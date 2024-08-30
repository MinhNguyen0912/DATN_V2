using DATN.Core.Enum;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class CreateVoucherRequest
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int? QuantityUsed { get; set; }
        public int? UsageLimit { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public List<int>? ListCateId { get; set; }
        public List<int>? ListProductId { get; set; }
        public ICollection<VoucherUser>? VoucherUsers { get; set; }
    }
}
