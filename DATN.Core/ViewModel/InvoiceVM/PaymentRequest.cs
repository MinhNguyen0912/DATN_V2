using DATN.Core.Enum;
using DATN.Core.ViewModel.GHNVM;

namespace DATN.Core.ViewModel.InvoiceVM
{
    public class PaymentRequest
    {
        //public decimal? TotalAmount { get; set; }
        //public decimal? Discount { get; set; }
        //public decimal? FinalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public int pendingCartId { get; set; }
        public int VoucherId { get; set; } = 0;
        public string FirstName {  get; set; }
        public string LastName {  get; set; }
        public string Email {  get; set; }
        public string PhoneNumber {  get; set; }
        public string to_ward_code {  get; set; }
        public string to_district_id {  get; set; }
        public string to_address {  get; set; }
        public decimal ShippingFee {  get; set; }
        //public CreateGHNOrderAdmin? PendingShippingOrder { get; set; }

    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public double GiaNhap { get; set; }
    }
}
