﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPP.Application.DTOs.ProductCategories
{
    public class ProductCategoryCreateDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
    }
}
