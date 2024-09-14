using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IProductEAVRepository : IBaseRepository<Product_EAV>
    {
        public Product_EAV GetByIdCustom(int id);
        public Task<Product_EAV> GetByIdAsync(int id);
        public Task<bool> UpdateQuantiyVariant(int ProductId, int VariantId, int Quantity);
        public Task<List<Product_EAV>> GetByName(string name);
        public Product_EAV GetByIdWithPromotion(int id);

    }
}
