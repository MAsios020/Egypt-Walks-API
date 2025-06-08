using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.API.Models.Dto
{
    public class RegisterAuthDto
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string?[] Roles { get; set; } // "Reader" or "Writer"
    }
}
