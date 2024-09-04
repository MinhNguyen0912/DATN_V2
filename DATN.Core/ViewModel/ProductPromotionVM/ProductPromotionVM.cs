using DATN.Core.Model;

namespace DATN.Core.ViewModel.ProductPromotionVM
{
    public class ProductPromotionVM
    {
        public int ProductPromotionId { get; set; }
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public PromotionVM.PromotionVM? Promotion { get; set; }
        public Product_EAV.ProductVM_EAV? Product { get; set; }
    }
}
