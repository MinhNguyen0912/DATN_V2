using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IProductEAVRepository : IBaseRepository<Product_EAV>
    {
        public Product_EAV GetByIdCustom(int id);
        public Product_EAV GetByIdWithPromotion(int id);

    }
}
