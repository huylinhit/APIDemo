using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIDemo.Models;
using APIDemo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper, ILogger<CitiesController> logger)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
        {
            var citiesList = await _cityInfoRepository.GetCitiesAsync();


            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDTO>>(citiesList));
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetCityById(int cityId, bool includePointOfInterest = false)
        {
            var CityFromStore = await _cityInfoRepository.GetCityAsync(cityId, includePointOfInterest);

            if (!await _cityInfoRepository.isCityExist(cityId))
            {
                _logger.LogCritical($"The cityId = {cityId} isn't exist");
                return NotFound();
            }

            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDTO>(CityFromStore));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDTO>(CityFromStore));
        }
    }
}

