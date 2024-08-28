using DATN.Core.Infrastructures;
using DATN.Core.Model;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IShippingOrderRepository : IBaseRepository<ShippingOrder>
    {
        List<ShippingOrder> getshippingcustom();
    }
}
