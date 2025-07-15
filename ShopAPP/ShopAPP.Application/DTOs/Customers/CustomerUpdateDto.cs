using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ShopAPP.Application.DTOs.Customers
{
    public class CustomerUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
