using DATN.Core.Enum;
using DATN.Core.Model;

namespace DATN.Core.ViewModel.PaymentInfoVM
{
    public class PaymentInfoVM
    {
        public int PaymentInfoId { get; set; }
        public int? InvoiceId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
