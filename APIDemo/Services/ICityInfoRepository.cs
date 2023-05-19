using APIDemo.Entities;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace APIDemo.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>,PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<City?> GetCityAsync(int cityId, bool includePointOfInterest);

        Task<bool> isCityExist(int cityId);

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestByIdAsync(int cityId, int pointofinterestId);

        Task AddPointOfInterestForAsync(int cityId, PointOfInterest pointofinterest);

        Task<bool> SaveChangeAsync();

        void DeletePointOfInterestForAsync(PointOfInterest pointOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestAsync(int cityId, string? name, string? searchQuery);

        Task<bool> CityNameMatchCityId(string name, int id);
    }
}
