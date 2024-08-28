using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model.Product_EAV
{
    public class Variant
    {
        [Key]
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string VariantName { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int Discount { get; set; }
        public int? Quantity { get; set; }
        public bool IsDefault {  get; set; } = false;

        // Navigation property
        public Product_EAV Product { get; set; }
        public ICollection<VariantAttribute>? VariantAttributes { get; set; }
    }
}
