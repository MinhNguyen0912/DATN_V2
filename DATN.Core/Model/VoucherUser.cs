using DATN.Core.Models;

namespace DATN.Core.Model
{
    public class VoucherUser : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public int VoucherId { get; set; }
        public Guid AppUserId { get; set; }
        public int UsageCount { get; set; }
        public Voucher Voucher { get; set; }
        public AppUser AppUser { get; set; }

    }
}
