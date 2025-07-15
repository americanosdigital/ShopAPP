using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.DTOs.Account
{
    public class RegisterUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Document { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Customer";

        public IFormFile? ImageFile { get; set; }
        public string? ProfileImageUrl { get; set; }
    }

}
