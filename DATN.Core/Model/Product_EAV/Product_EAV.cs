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
        public ICollection<Variant> Variants { get; set; }
        public ICollection<CategoryProduct>? CategoryProducts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<ProductPromotion>? PromotionProducts { get; set; }
    }
}
