using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Data;
using Web.API.Models.Domain;
using Web.API.Models.Dto.Region;
using Web.API.Models.Pagination;
using Web.API.Repositories.Classes;
using Web.API.Repositories.Interfaces;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    // [Authorize(Roles = "Admin,User")] // Uncomment if you want to restrict access to specific roles
    public class RegionsController : ControllerBase
    {
        private IRegionRepository RegionRepository;
        private readonly IMapper mapper;

        public RegionsController(WebDbContext webDbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            RegionRepository = regionRepository;
            this.mapper = mapper;
        }




        // GET: api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions([FromQuery] string?filterName, [FromQuery] string? filterValue, [FromQuery] int pagenumber, [FromQuery] int pagesize)
        {
            // Domain models are mapped to DTOs in the service layer, so we don't need to map them here.
            // var regions = webDbContext.Regions.ToList();

         
            var RegionsDomain = await RegionRepository.GetALL(filterName, filterValue, pagenumber,pagesize);
            var PagedRegionDto = mapper.Map<PagedResultsDto<Region>>(RegionsDomain);



            return Ok(RegionsDomain);
        }

        [HttpGet("{Id:guid}")]
        public async Task< IActionResult >GetRegionByID(Guid Id)
        {
            Region region = await RegionRepository.GetByIdAsync(Id);

            if(region==null) return NotFound($"The Region Not found ");
          
            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddRegion([FromBody] RegionDto regionDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("Error", "please enter right info format");
            //    return BadRequest(ModelState);

            //}

            if(regionDto == null )
            {
                return BadRequest("Invalid region data provided.");
            }

           RegionRepository.AddAsync(regionDto);



            return Ok(regionDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id ,[FromBody] UpdateRegionDto regionDto)
        {
       

            if (regionDto == null)
            {
                return BadRequest("Invalid region data provided.");
            }

            var updatedRegion = await RegionRepository.UpdateAsync(id,regionDto);

            if (updatedRegion == null)
            {
                return NotFound($"Region with ID provided  not found.");
            }


            var updatedDto = mapper.Map<RegionDto>(updatedRegion);


            return Ok(updatedDto);


        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            if (id == Guid.Empty ) return NotFound("Please Set the id");

            var IsRegionDeleted = await RegionRepository.DeleteAsync(id);

            if (!IsRegionDeleted)
            {
                return NotFound($"Region with ID {id} not found.");
            }

            return Ok($"Region with ID {id} has been deleted successfully.");


        }


    }
}
