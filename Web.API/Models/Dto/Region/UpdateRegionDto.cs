using System.ComponentModel.DataAnnotations;

namespace Web.API.Models.Dto.Region
{
    public class UpdateRegionDto
    {

        [Required(ErrorMessage = "Please  Provide The Code")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Code must be exactly 3 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please  Provide The Name")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
