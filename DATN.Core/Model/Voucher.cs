using DATN.Core.Enum;
using DATN.Core.Models;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? QuantityUsed { get; set; }
        public int? UsageLimit { get; set; }

        //Condition, ActivationTime
        public string? Condition { get; set; }
        public DateTime? ActivationTime { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }        
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<VoucherCate>? VoucherCates { get; set; }
        public List<VoucherProduct>? VoucherProducts { get; set; }
        public ICollection<VoucherUser>? VoucherUsers { get; set; }
    }
}
