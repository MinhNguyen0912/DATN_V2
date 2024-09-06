using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class VoucherTemplate
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ActivationTime { get; set; }
        public bool IsVisible { get; set; } // Trạng thái hiển thị
    }
}
