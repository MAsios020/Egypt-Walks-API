using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Domain;
using Web.API.Models.Dto.WalkDTO;
using Web.API.Models.Pagination;
using Web.API.Repositories.Interfaces;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository iwalkRepository;
        private readonly IMapper _mapper;
        public IRegionRepository RegionRepository { get; }

        public WalksController(IWalkRepository iwalkRepository, IMapper mapper, IRegionRepository regionRepository)
        {
            this.iwalkRepository = iwalkRepository;
            this._mapper = mapper;
            RegionRepository = regionRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterName, [FromQuery] string? filterValue, [FromQuery] int pagenumber, [FromQuery] int pagesize)
        {

            var walks = await iwalkRepository.GetALL(filterName, filterValue, pagenumber, pagesize);

            var walksDto = _mapper.Map<PagedResultsDto<WalkDto>>(walks);


            return Ok(walksDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetWalkByID(Guid id)
        {
            Walk walk = await iwalkRepository.GetByIdAsync(id);

            if (walk == null) return NotFound($"The walk Not found ");

            return Ok(_mapper.Map<WalkDto>(walk));

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewWalk([FromBody] InsertWalkDto InsertWalkDto)
        {

            if (InsertWalkDto == null) return BadRequest("Invalid Walk data provided.");

            var Addedwalk = await iwalkRepository.AddAsync(InsertWalkDto, "");

            if (Addedwalk.Message != null) return BadRequest(Addedwalk.Message);

            var WalkDto = _mapper.Map<WalkDto>(Addedwalk.walk);

            return Ok(WalkDto);

        }

        [HttpPut("{Id:guid}")]
        public async Task<IActionResult> UpdateWalk(Guid Id , [FromBody] InsertWalkDto walkDto)
        { 
            if (Id == Guid.Empty) return NotFound("Please Set the id");

            if (walkDto == null) return BadRequest("Invalid Walk data provided.");

            var updatedWalk = await iwalkRepository.UpdateAsync(Id, walkDto, "");

            if (updatedWalk.Message == "Walk Not found") return NotFound($"Walk with ID {Id} not found.");
            if (updatedWalk.Message == "Invalid difficulty ID provided") return NotFound(updatedWalk.Message);
            if (updatedWalk.Message == "Invalid region ID provided") return NotFound(updatedWalk.Message);

            var WalkDto = _mapper.Map<WalkDto>(updatedWalk.walk);

            return Ok(WalkDto);
        
        
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            if (id == Guid.Empty) return NotFound("Please Set the id");

            var IsWalkDeleted = await iwalkRepository.DeleteAsync(id);

            if (!IsWalkDeleted)
            {
                return NotFound($"Walk with ID {id} not found.");
            }

            return Ok($"Walk with ID {id} has been deleted successfully.");

        }
    }
}
