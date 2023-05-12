using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using APIDemo.Entities;
using APIDemo.Models;
using APIDemo.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace APIDemo.Controllers
{ 
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        public readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointOfInterestController(
            ILogger<PointOfInterestController> logger, 
            IMailService mailService, 
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDTO>>> GetPointOfInterests(int cityId)
        {
            try {

                var list = await _cityInfoRepository.GetPointOfInterestAsync(cityId);

                if (!await _cityInfoRepository.isCityExist(cityId))
                {
                    _logger.LogCritical($"The city with id {cityId} wasn't found when accessing points of interest");
                    return NotFound();
                }


                if (list == null)
                {
                    _logger.LogCritical($"The Point Of Interest doesnt exist");
                    return NotFound();
                }

                return Ok(_mapper.Map<IEnumerable<PointOfInterestDTO>>(list));
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
        public async Task<ActionResult<PointOfInterestDTO>> GetPointOfInterestsById(int cityId, int pointofinterestId)
        {

            if (!await _cityInfoRepository.isCityExist(cityId))
            {
                _logger.LogCritical($"The city with id {cityId} wasn't found when accessing points of interest");
                return NotFound();
            }

            var PointOfInterestFromStore = await _cityInfoRepository.GetPointOfInterestByIdAsync(cityId, pointofinterestId);

            if (PointOfInterestFromStore == null) {
                _logger.LogCritical($"The Point Of Interest Id = {pointofinterestId} isn't exist in this context");
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDTO>(PointOfInterestFromStore));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDTO>> CreatePointOfInterest(
            int cityId, PointOfInterestCreationDTO pointOfInterest)
        {

            if (!await _cityInfoRepository.isCityExist(cityId)){
                //_logger.LogCritical($"The city with id {cityId} is existed when accessing points of interest");
                return NotFound();
            }

            var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);


            await _cityInfoRepository.AddPointOfInterestForAsync(cityId, finalPointOfInterest);

            await _cityInfoRepository.SaveChangeAsync();

            var returnPointOfInterest = _mapper.Map<PointOfInterestDTO>(finalPointOfInterest);



            return CreatedAtRoute("GetPointOfInterestById",
                new
                {
                    cityId = cityId,
                    pointofinterestId = returnPointOfInterest.Id
                },
                returnPointOfInterest);
        }

        [HttpPut("{pointofinterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId, int pointofinterestId,
            PointOfInterestUpdateDTO pointOfInterest)
        {

            if (!await _cityInfoRepository.isCityExist(cityId)) 
            { 
                return NotFound();
            }

            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestByIdAsync(cityId, pointofinterestId);


            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, pointOfInterestEntity);

            await _cityInfoRepository.SaveChangeAsync();

            return NoContent();
        }

        //[HttpPatch("{pointofinterestId}")]
        //public ActionResult<PointOfInterestDTO> PartiallyUpdatePointOfInterest(
        //    int cityId, int pointofinterestId,
        //    JsonPatchDocument<PointOfInterestUpdateDTO> patchDocument)
        //{
        //    var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

        //    if (CityList == null)
        //    {
        //        return NotFound();
        //    }

        //    var PointOfInterest = CityList.PointOfInterests.FirstOrDefault(item => item.Id == pointofinterestId);

        //    if (PointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    PointOfInterestUpdateDTO pointofinterestpath = new PointOfInterestUpdateDTO()
        //    {
        //        Name = PointOfInterest.Name,
        //        Description = PointOfInterest.Description,
        //    };

        //    patchDocument.ApplyTo(pointofinterestpath,
        //                          ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!TryValidateModel(pointofinterestpath)){
        //        return BadRequest(ModelState);
        //    }

        //    PointOfInterest.Name = pointofinterestpath.Name;
        //    PointOfInterest.Description = pointofinterestpath?.Description;

        //    return NoContent();

        //}

        //[HttpDelete("{pointofinterestId}")]
        //public ActionResult<PointOfInterestDTO> DeletePointOfInterest(int cityId, int pointofinterestId)
        //{
        //    var CityList = _citiesDataStore.Cities.FirstOrDefault(item => item.Id == cityId);

        //    if (CityList == null)
        //    {
        //        return NotFound();
        //    }

        //    var deletePointOfInterest = CityList.PointOfInterests.FirstOrDefault(item => item.Id == pointofinterestId);

        //    if (deletePointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    CityList.PointOfInterests.Remove(deletePointOfInterest);

        //    _mailService.Send(
        //        $"Delete Point Of Interest",
        //        $"The Point Of Interest With Id {pointofinterestId} of City with id {cityId} was deleted");

        //    return NoContent();
        //}
    }
}

