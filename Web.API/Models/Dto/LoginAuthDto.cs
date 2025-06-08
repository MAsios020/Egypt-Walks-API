using System.ComponentModel.DataAnnotations;

namespace Web.API.Models.Dto
{
    public class LoginAuthDto
    {

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
