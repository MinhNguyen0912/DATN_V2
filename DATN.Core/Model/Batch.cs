using DATN.Core.Enum;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Batch : BaseEntity
    {
        public string? Name { get; set; } // Tên đợt phát hành/ tên voucher
        public string? Description { get; set; } // Mô tả
        public VoucherType? Type { get; set; } // Loại voucher (Ví dụ: giảm giá, miễn phí vận chuyển)
        public DiscountType DiscountType { get; set; } // Loại giảm giá (Ví dụ: %, tiền mặt)
        public decimal? DiscountAmount { get; set; } // Số tiền giảm giá
        public decimal? MinOrderAmount { get; set; } // Số tiền tối thiểu để sử dụng voucher
        public decimal? MaxDiscountAmount { get; set; } // Số tiền giảm giá tối đa (đối với voucher giảm giá %)
        public DateTime? StartDate { get; set; } // Ngày bắt đầu thời gian voucher có hiêu lực
        public DateTime? EndDate { get; set; } // Ngày kết thúc thời gian voucher có hiệu lực
        public int? ExpirationDate { get; set; } // Số ngày voucher có hiệu lực sau khi được phát cho người dùng
        public bool? IsActive { get; set; } = true; // Trạng thái hoạt động của voucher
        public List<Voucher>? Vouchers { get; set; }

    }
}
