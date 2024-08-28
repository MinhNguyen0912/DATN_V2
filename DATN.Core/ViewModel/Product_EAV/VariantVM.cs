using DATN.Core.Model.Product_EAV;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class VariantVM
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string VariantName { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int? Quantity { get; set; }

        // Navigation property
        public Product_EAV.ProductVM_EAV Product { get; set; }
        public ICollection<VariantAttributeVM> VariantAttributes { get; set; }
    }
}
