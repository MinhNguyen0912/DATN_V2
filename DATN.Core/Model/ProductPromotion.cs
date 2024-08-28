namespace DATN.Core.Model
{
    public class ProductPromotion
    {

        public int ProductPromotionId { get; set; }
        public int PromotionId { get; set; }
        public Promotion? Promotion { get; set; }
        public Product_EAV.Product_EAV? Product { get; set; }
        public int ProductId { get; set; }
    }
}
