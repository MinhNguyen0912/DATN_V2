using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProdutEAVVM
{
    public class CreateProductEAVVM
    {
        public string ProductName { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public List<CategoryProduct>? CategoryProducts { get; set; }
        public string cateIds { get; set; }
        public Brand? Brand { get; set; }
        public Origin? Origin { get; set; }
        public List<IFormFile>? Images { get; set; }
        public IFormFile? ImagesDefault { get; set; }
        public List<CreateVariantsVM> Variants { get; set; } = new List<CreateVariantsVM>();

    }
}
