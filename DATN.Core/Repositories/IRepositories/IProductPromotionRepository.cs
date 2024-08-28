using DATN.Core.Infrastructures;
using DATN.Core.Model;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IProductPromotionRepository : IBaseRepository<ProductPromotion>
    {
        List<ProductPromotion> GetAllCustom();
        List<ProductPromotion> GetAllByProduct();
    }
}
