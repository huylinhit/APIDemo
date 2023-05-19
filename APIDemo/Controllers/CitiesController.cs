using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using APIDemo.Models;
using APIDemo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIDemo.Controllers
{
    [ApiController]
    //[Authorize("MustBeHaNoi")]
    [Route("api/v{version:apiVersion}/cities")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CitiesController> _logger;
        private readonly int maxTotalPage = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper, ILogger<CitiesController> logger)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxTotalPage)
            {
                pageSize = maxTotalPage;
            }
            
            var (citiesList, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            HttpContext.Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(paginationMetadata));


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

