using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class SearchVoucherRequest
    {
        public VoucherStatus? Status { get; set; }
        public string? UserName { get; set; }
        public int BatchId { get; set; }    
    }
}
