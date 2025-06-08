using Web.API.Models.Domain;
using Web.API.Models.Dto.WalkDTO;
using Web.API.Models.Pagination;

namespace Web.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<PagedResults<WalkDto>> GetALL(string? filterName, string? filterValue, int pagenumber, int pagesize);
        Task<Walk?> GetByIdAsync(Guid id);

         Task<(Walk? walk, string? Message)> AddAsync(InsertWalkDto walk ,string? Message);

        Task<(Walk? walk , string? Message)> UpdateAsync(Guid id, InsertWalkDto walk, string? Message);

        Task<bool> DeleteAsync(Guid walkID);


    }
}
