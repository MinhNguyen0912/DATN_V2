using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Models;

namespace DATN.Core.ViewModel.InvoiceVM
{
    public class InvoiceVM
    {
        public int InvoiceId { get; set; }
        public DateTime CreateDate { get; set; }
        //public decimal? TotalAmount { get; set; }
        //public decimal? Discount { get; set; }
        //public decimal? FinalAmount { get; set; }
        public InvoiceStatus Status { get; set; }
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public ICollection<InvoiceDetailVM.InvoiceDetailVM>? InvoiceDetails { get; set; }
        public string? Note { get; set; }
        public int? VoucherUserId { get; set; }
        public ShippingOrder? ShippingOrder { get; set; }

    }
}
