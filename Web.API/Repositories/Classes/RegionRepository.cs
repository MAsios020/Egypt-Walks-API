using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Domain;
using Web.API.Models.Dto.Region;
using Web.API.Models.Pagination;
using Web.API.Repositories.Interfaces;

namespace Web.API.Repositories.Classes
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WebDbContext webDbContext;

        public RegionRepository(WebDbContext webDbContext)
        {
            this.webDbContext = webDbContext;
        }

        public async Task<PagedResults<Region>> GetALL(string? filterName, string? filterValue, int pageNumber, int pageSize)
        {

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            IQueryable<Region> query = webDbContext.Regions;

            if (!string.IsNullOrWhiteSpace(filterName) && !string.IsNullOrWhiteSpace(filterValue))
            {
                switch (filterName.ToLowerInvariant())
                {
                    case "name":
                        query = query.Where(x => x.Name.Contains(filterValue));
                        break;
                    case "code":
                        query = query.Where(x => x.Code.Contains(filterValue));
                        break;
                    default:
                        query = query;
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var skipResults = (pageNumber - 1) * pageSize;
            var regions = await query
                .OrderBy(x => x.Name) 
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResults<Region>
            {
                Data = regions,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };

        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return null;

            Region region = await webDbContext.Regions.FindAsync(id);

            if (region == null) return null;

            return region;


        }

        public async Task<Region> AddAsync(RegionDto _region)
        {
            Region region = new Region
            {
                Id = Guid.NewGuid(),
                Code = _region.Code,
                Name = _region.Name,
                RegionImageUrl = _region.RegionImageUrl
            };

            if (region == null) return null;

            webDbContext.Regions.AddAsync(region);
            webDbContext.SaveChangesAsync();

            return region;
        }
        public async Task<Region> UpdateAsync(Guid id , UpdateRegionDto _regionDto)
        {
            Region Region = await webDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);


            if (Region == null) return null;

            Region.Code = _regionDto.Code;
            Region.Name = _regionDto.Name;
            Region.RegionImageUrl = _regionDto.RegionImageUrl;


            webDbContext.Regions.Update(Region);
            await webDbContext.SaveChangesAsync();

            return Region;
        }

        public async Task<bool> DeleteAsync(Guid id )
        {
            Region Region = await webDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (Region == null) return false;

            webDbContext.Regions.Remove(Region);
            await webDbContext.SaveChangesAsync();

            return true;
        }

    
    }
}
