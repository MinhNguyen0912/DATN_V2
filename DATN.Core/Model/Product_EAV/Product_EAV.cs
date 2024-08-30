using DATN.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model.Product_EAV
{
    public class Product_EAV
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Origin? Origin { get; set; }

        // Navigation property
        public List<Variant> Variants { get; set; }
        public List<CategoryProduct>? CategoryProducts { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Image>? Images { get; set; }
        public List<ProductPromotion>? PromotionProducts { get; set; }
        public List<VoucherProduct>? VoucherProducts { get; set; }
    }
}
