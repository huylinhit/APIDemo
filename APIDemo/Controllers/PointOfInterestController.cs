using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIDemo.Model;
using APIDemo.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{ 
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        public readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointOfInterestController(ILogger<PointOfInterestController> logger, IMailService mailService, CitiesDataStore citiesDataStore )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }      


        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDTO>> GetPointOfInterests(int cityId)
        {
            try {
                var list = _citiesDataStore.Cities.SingleOrDefault(item => item.Id == cityId);

                if (list == null)
                {
                    _logger.LogCritical($"The city with id {cityId} wasn't found when accessing points of interest");
                    return NotFound();
                }

                return Ok(list.PointOfInterest);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"The city with id {cityId} wasn't found when accessing points of interest",
                    ex);

                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterestById")]
        public ActionResult<PointOfInterestDTO> GetPointOfInterestsById(int cityId, int pointofinterestId)
        {
            var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

            if (CityList == null) return NotFound();

            var PointOfInterestList = CityList.PointOfInterest.FirstOrDefault(item => item.Id == pointofinterestId);

            if (PointOfInterestList == null) return NotFound();

            return Ok(PointOfInterestList);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDTO> CreatePointOfInterest(
            int cityId, PointOfInterestCreationDTO pointOfInterest)
        {
            var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

            if (CityList == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = _citiesDataStore.Cities
                .SelectMany(item => item.PointOfInterest)
                .Max(item => item.Id);

            PointOfInterestDTO finalPointOfInterest = new PointOfInterestDTO()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            CityList.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterestById",
                new
                {
                    cityId = cityId,
                    pointofinterestId = finalPointOfInterest.Id
                },
                finalPointOfInterest);
        }

        [HttpPut("{pointofinterestId}")]
        public ActionResult<PointOfInterestDTO> UpdatePointOfInterest(
            int cityId, int pointofinterestId,
            PointOfIntrestUpdateDTO pointOfIntrest)
        {
            var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

            if (CityList == null)
            {
                return NotFound();
            }

            var finalPointOfIntrest = CityList.PointOfInterest.FirstOrDefault(item => item.Id == pointofinterestId);

            if (finalPointOfIntrest == null)
            {
                return NotFound();
            }

            finalPointOfIntrest.Name = pointOfIntrest.Name;
            finalPointOfIntrest.Description = pointOfIntrest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofinterestId}")]
        public ActionResult<PointOfInterestDTO> PartiallyUpdatePointOfInterest(
            int cityId, int pointofinterestId,
            JsonPatchDocument<PointOfIntrestUpdateDTO> patchDocument)
        {
            var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

            if (CityList == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = CityList.PointOfInterest.FirstOrDefault(item => item.Id == pointofinterestId);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            PointOfIntrestUpdateDTO pointOfInterestToPatch = new PointOfIntrestUpdateDTO()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }


            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestId}")]
        public ActionResult<PointOfInterestDTO> DeletePointOfInterest(int cityId, int pointofinterestId)
        {
            var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

            if (CityList == null)
            {
                return NotFound();
            }

            var deletePointOfInterest = CityList.PointOfInterest.FirstOrDefault(item => item.Id == pointofinterestId);

            if (deletePointOfInterest == null)
            {
                return NotFound();
            }

            CityList.PointOfInterest.Remove(deletePointOfInterest);

            _mailService.Send(
                $"Delete Point Of Interest",
                $"The Point Of Interest With Id {pointofinterestId} of City with id {cityId} was deleted");

            return NoContent();
        }
    }
}

