using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class AssignVoucherRequest
    {
        public int BatchId { get; set; }
        public List<AssignVoucherUserRequest> ListAssign { get; set; }
    }
}
