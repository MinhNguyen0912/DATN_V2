using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        public int Quantity { get; set; } = 1;
        // Foreign Key
        public int? InvoiceId { get; set; }
        public int VariantId { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public decimal PuscharPrice { get; set; }
        public Comment? Comment { get; set; }

        // Navigation property
        public Invoice? Invoice { get; set; }
        public Product_EAV.Variant Variant { get; set; }
    }
}
