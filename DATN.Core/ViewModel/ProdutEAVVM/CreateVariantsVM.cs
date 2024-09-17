using DATN.Core.ViewModel.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProdutEAVVM
{
    public class CreateVariantsVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal SalelPrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int MaximumQuantityPerOrder { get; set; }
        public int Weight { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; } = true;
        public string attributeValueIds { get; set; }
        // List of specifications for each variant
        public List<CreateSpecificationsVM> Specifications { get; set; } = new List<CreateSpecificationsVM>();
    }
}
