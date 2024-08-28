namespace DATN.Core.ViewModel.ShippingOrderVM
{
    public class CreateShippingOrderRepuest
    {
        public string ShippingOrderCode { get; set; }
        public double ShippingFee { get; set; }
        public Guid CustomerId { get; set; }
        public int InvoiceId { get; set; }
    }
}
