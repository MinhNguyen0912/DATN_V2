using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IVariantRepository : IBaseRepository<Variant>
    {
        public Variant GetByIdCustom(int variantId);

    }
}
