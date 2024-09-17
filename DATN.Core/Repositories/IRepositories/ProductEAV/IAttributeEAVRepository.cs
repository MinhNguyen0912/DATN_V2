using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using Attribute_EAV = DATN.Core.Model.Product_EAV.Attribute_EAV;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IAttributeEAVRepository : IBaseRepository<Attribute_EAV>
    {
        public List<Attribute_EAV> GetAllAttributeValue();
        AttributesPaging GetAttributePaging(AttributesPaging request);
    }
}

    
