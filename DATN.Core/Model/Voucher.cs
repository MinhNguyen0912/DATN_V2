using DATN.Core.Models;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string Name { get; set; }
        public int? DiscountByPercent { get; set; }
        public decimal? DiscountByPrice { get; set; }
        public ICollection<VoucherUser>? VoucherUsers { get; set; }
    }
}
