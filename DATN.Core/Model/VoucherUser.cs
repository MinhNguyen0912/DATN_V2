using DATN.Core.Models;

namespace DATN.Core.Model
{
    public class VoucherUser : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public int VoucherId { get; set; }
        public Guid AppUserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Voucher Voucher { get; set; }
        public AppUser AppUser { get; set; }

    }
}
