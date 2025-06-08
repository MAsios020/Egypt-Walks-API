using System.ComponentModel.DataAnnotations;

namespace Web.API.Models.Dto.Region
{
    public class RegionDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please  Provide The Code")]
        [MinLength(3, ErrorMessage = "Please Enter  3 chacters minimum")]
        [MaxLength(3, ErrorMessage = "Please Enter  3 chacters Maximum")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please  Provide The Name")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
