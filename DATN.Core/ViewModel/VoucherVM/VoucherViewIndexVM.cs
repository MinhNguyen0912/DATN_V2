using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class VoucherViewIndexVM
    {
        public List<voucherVM.VoucherVM> Vouchers { get; set; } = new List<voucherVM.VoucherVM>();
        public string? Message { get; set; }
    }
}
