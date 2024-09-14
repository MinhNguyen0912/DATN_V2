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
        public int? Quantity { get; set; }
        public bool IsDefault {  get; set; } = false;
        public int MaximumQuantityPerOrder {  get; set; } 
        public int Weight {  get; set; } 
        //public bool isShow {  get; set; }

        // Navigation property
        public Product_EAV Product { get; set; }
        public List<VariantAttribute>? VariantAttributes { get; set; }
        public List<Specification>? Specifications { get; set; }
        public List<PendingCartVariant>? PendingCartVariants { get; set; }

    }
}
