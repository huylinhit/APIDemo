﻿using APIDemo.Entities;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace APIDemo.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointOfInterest);

        Task<bool> isCityExist(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestByIdAsync(int cityId, int pointofinterestId);

        Task AddPointOfInterestForAsync(int cityId, PointOfInterest pointofinterest);

        Task<bool> SaveChangeAsync();

        Task UpdatePointOfInterestForAsync(int cityId, PointOfInterest pointofinterest);


    }
}
