using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Domain;
using Web.API.Models.Dto.Region;
using Web.API.Models.Dto.WalkDTO;
using Web.API.Models.Pagination;
using Web.API.Repositories.Interfaces;

namespace Web.API.Repositories.Classes
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WebDbContext webDbContext;

        public WalkRepository(WebDbContext webDbContext)
        {

            this.webDbContext = webDbContext;
        }

        public async Task<PagedResults<WalkDto>> GetALL(string? filterName, string? filterValue, int pageNumber, int pageSize)
        {

            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            IQueryable<Walk> query = webDbContext.Walks;

            if (!string.IsNullOrWhiteSpace(filterName) && !string.IsNullOrWhiteSpace(filterValue))
            {
                switch (filterName.ToLowerInvariant())
                {
                    case "name":
                        query = query.Where(x => x.Name.Contains(filterValue));
                        break;
                    case "km":
                        query = query.Where(x => x.LengthInKm <= int.Parse(filterValue));
                        break;
                    default:
                        query = query;
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var skipResults = (pageNumber - 1) * pageSize;


            var walks = await query
           .Select(w => new WalkDto
           {
               Id = w.Id,
               Name = w.Name,
               Description = w.Description,
               LengthInKm = w.LengthInKm,
               WalkImageUrl = w.WalkImageUrl,
               Difficulty = new Difficulty
               {
                   Id = w.Difficulty.Id,
                   Name = w.Difficulty.Name
               },
               Region = new RegionDto
               {
                   Id = w.Region.Id,
                   Code = w.Region.Code,
                   Name = w.Region.Name,
                   RegionImageUrl = w.Region.RegionImageUrl
               }
           })
           .OrderBy(x => x.Name)
           .Skip(skipResults)
           .Take(pageSize)
           .ToListAsync();

            return new PagedResults<WalkDto>
            {
                Data = walks,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };


        }


        public async Task<(Walk? walk, string? Message)> AddAsync(InsertWalkDto InsertWalkDto, string? Message)
        {


            var difficultyExists = await webDbContext.Difficulties
                .AnyAsync(d => d.Id == InsertWalkDto.DifficultyId);

            var regionExists = await webDbContext.Regions
                .AnyAsync(r => r.Id == InsertWalkDto.RegionId);

            if (!difficultyExists)
                return (new Walk(), "Invalid difficulty ID provided");

            if (!regionExists)
                return (null, "Invalid region ID provided");


            Walk walk = new Walk
            {
                Id = Guid.NewGuid(),
                Description = InsertWalkDto.Description,
                LengthInKm = InsertWalkDto.LengthInKm,
                Name = InsertWalkDto.Name,
                WalkImageUrl = InsertWalkDto.WalkImageUrl,
                DifficultyId = InsertWalkDto.DifficultyId,
                RegionId = InsertWalkDto.RegionId,
            };

            var Walk = await webDbContext.Walks.AddAsync(walk);

            await webDbContext.SaveChangesAsync();

            var completeWalk = await webDbContext.Walks
            .Include(w => w.Difficulty)
            .Include(w => w.Region)
            .FirstOrDefaultAsync(w => w.Id == walk.Id);

      




            return (completeWalk, null);

        }

        public async Task<bool> DeleteAsync(Guid walkID)
        {
            Walk walk = await webDbContext.Walks.FirstOrDefaultAsync(x => x.Id == walkID);

            if (walk == null) return false;

            webDbContext.Walks.Remove(walk);
            await webDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<Walk?> GetByIdAsync(Guid id)
        {

            if (id == Guid.Empty) return null;


            Walk? walk = await webDbContext.Walks
                   .Include(w => w.Region)
                   .Include(w => w.Difficulty)
                   .FirstOrDefaultAsync(w => w.Id == id);


            if (walk == null) return null;

            return walk;
        }

        public async Task<(Walk? walk,string? Message)> UpdateAsync(Guid id, InsertWalkDto walkDto,string? message)
        {

            var difficultyExists = await webDbContext.Difficulties
              .AnyAsync(d => d.Id == walkDto.DifficultyId);

            var regionExists = await webDbContext.Regions
                .AnyAsync(r => r.Id == walkDto.RegionId);

            if (!difficultyExists)
                return (new Walk(), "Invalid difficulty ID provided");

            if (!regionExists)
                return (null, "Invalid region ID provided");



            var walk = await webDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);


            if (walk == null) return (null,"Walk Not found");

            walk.Name = walkDto.Name;
            walk.Description = walkDto.Description;
            walk.LengthInKm = walkDto.LengthInKm;
            walk.WalkImageUrl = walkDto.WalkImageUrl;
            walk.DifficultyId = walkDto.DifficultyId;
            walk.RegionId = walkDto.RegionId;
          

       

            webDbContext.Walks.Update(walk);
            await webDbContext.SaveChangesAsync();

            var completeWalk = await webDbContext.Walks
          .Include(w => w.Difficulty)
          .Include(w => w.Region)
          .FirstOrDefaultAsync(w => w.Id == walk.Id);

            return (completeWalk, "success");
        }
    }
}
