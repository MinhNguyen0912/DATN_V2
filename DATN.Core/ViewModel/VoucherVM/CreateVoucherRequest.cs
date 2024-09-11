using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class CreateVoucherRequest
    {
        public int BatchId { get; set; }    
        public List<string>? VoucherCodes { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime ActivationTime { get; set; }
    }
}
