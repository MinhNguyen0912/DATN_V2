using DATN.Core.Model;
using DATN.Core.ViewModel.Product_EAV;

namespace DATN.Core.ViewModel.InvoiceDetailVM;

public class InvoiceDetailForCommentVM
{
    public int InvoiceDetailId { get; set; }
    public int Quantity { get; set; } = 1;
    // Foreign Key
    public int? InvoiceId { get; set; }
    public int? ProductId { get; set; }
    public bool? IsShowComment { get; set; }

    public int ProductAttributeId { get; set; }
    public double OldPrice { get; set; }
    public double NewPrice { get; set; }
    public double PuscharPrice { get; set; }
    public int VariantId { get; set; }  
    public VariantVM? Variant { get; set; }
    public Comment? Comment { get; set; }
    public string? ImagePath { get; set; }
    // Navigation property
    public Invoice? Invoice { get; set; }
}