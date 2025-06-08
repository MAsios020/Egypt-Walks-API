using AutoMapper;
using Web.API.Models.Domain;
using Web.API.Models.Dto;
using Web.API.Models.Dto.Region;
using Web.API.Models.Dto.WalkDTO;
using Web.API.Models.Pagination;

namespace Web.API.Mapping
{
    public class AutoMapperProfile :Profile
    {

        public AutoMapperProfile() 
        {
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap(typeof(PagedResults<>), typeof(PagedResultsDto<>)).ReverseMap();

        }
    }
}
