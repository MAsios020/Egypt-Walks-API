using Web.API.Models.Domain;
using Web.API.Models.Pagination;
using Web.API.Models.Dto.Region;

namespace Web.API.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<PagedResults<Region>> GetALL(string? filterName,string? filterValue, int pagenumber, int pagesize);
        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> AddAsync(RegionDto region);

        Task<Region> UpdateAsync(Guid id , UpdateRegionDto region);

        Task<bool> DeleteAsync(Guid regionID);
    }
}
