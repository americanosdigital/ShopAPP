using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopAPP.Application.DTOs.Account
{
    public class UpdateUserDto
    {
        public string FullName { get; set; } = default!;
        public string Document { get; set; } = default!;
        public string Email { get; set; } = default!;

        public IFormFile? ImageFile { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
