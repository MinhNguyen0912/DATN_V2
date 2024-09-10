using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.VoucherVM
{
    public class AssignVoucherUserRequest
    {
        public Guid UserId { get; set; }
        public int TotalVoucherAssign { get; set; }
    }
}
