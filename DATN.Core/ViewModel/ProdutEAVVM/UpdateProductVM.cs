﻿using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.ViewModel.ImagePath;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProdutEAVVM
{
    public class UpdateProductVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public List<CategoryProduct>? CategoryProducts { get; set; }
        public string cateIds { get; set; }
        public Brand? Brand { get; set; }
        public Origin? Origin { get; set; }
        public int ImagedefaultId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<ImageVM> ImageGetDB { get; set; }
        public IFormFile? ImagesDefault { get; set; }
        public List<UpdateVariantVM>? Variants { get; set; }



    }
}
