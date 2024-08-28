using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product_EAV
{
    public class Variant
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string VariantName { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int? Quantity { get; set; }

        // Navigation property
        public Product Product { get; set; }
        public ICollection<VariantAttribute> VariantAttributes { get; set; }
    }
}
