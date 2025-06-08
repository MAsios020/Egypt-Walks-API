using Web.API.Models.Domain;
using Web.API.Models.Dto.Region;

namespace Web.API.Models.Dto.WalkDTO
{
    public class InsertWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        // Navigation properties
        //public Difficulty Difficulty { get; set; }
        //public RegionDto Region { get; set; }
    }
}
