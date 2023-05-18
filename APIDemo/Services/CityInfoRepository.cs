using APIDemo.DbContexts;
using APIDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;
        private readonly ILogger<CityInfoRepository> _logger;
        const int maxPageSize = 20;
        public CityInfoRepository(CityInfoContext context, ILogger<CityInfoRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name.Equals(name));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(c => c.Name.ToLower().Contains(searchQuery.ToLower())
                || (c.Description != null && c.Description.ToLower().Contains(searchQuery.ToLower())));
            }

            var totalItemCount = collection.Count();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionReturn = await collection
                .OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionReturn, paginationMetadata);
        }


        public async Task<City?> GetCityAsync(int cityId, bool includePointOfInterest)
        {
            if (includePointOfInterest)
            {
                var CityFromStore = await _context.Cities
                    .Include(item => item.PointOfInterests)
                    .Where(item => item.Id == cityId)
                    .FirstOrDefaultAsync();
                return CityFromStore;
            }

            return await _context.Cities.Where(item => item.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestAsync(int cityId)
        {
            var CityFromStore = await _context.PointOfInterests
                .Where(item => item.CityId == cityId)
                .ToListAsync();
            return CityFromStore;
        }

        public async Task<PointOfInterest?> GetPointOfInterestByIdAsync(int cityId, int pointofinterestId)
        {
            var PointOfInterestFromStore = await _context.PointOfInterests
                .Where(item => item.CityId == cityId && item.Id == pointofinterestId)
                .FirstOrDefaultAsync();

            return PointOfInterestFromStore;

        }

        public async Task<bool> isCityExist(int cityId)
        {
            return await _context.Cities.AnyAsync(item => item.Id == cityId);
        }

        public async Task AddPointOfInterestForAsync(int cityId
            , PointOfInterest pointofinterest)
        {
            var CityFromStore = await GetCityAsync(cityId, false);
            if (CityFromStore != null)
            {
                CityFromStore.PointOfInterests.Add(pointofinterest);
            }
        }

        public void DeletePointOfInterestForAsync(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


        public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestAsync(int cityId, string? name, string? searchQuery)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(searchQuery))
            {
                return await GetPointOfInterestAsync(cityId);
            }

            var collection = _context.PointOfInterests.Where(item => item.CityId == cityId);

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(item => item.Name.Equals(name));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(item => item.Name.ToLower().Contains(searchQuery.ToLower())
                ||(item.Description != null && item.Description.Contains(searchQuery)));
            }

            return await collection
                .OrderBy(item =>item.Name)
                .ToListAsync();
        }

    }
}
