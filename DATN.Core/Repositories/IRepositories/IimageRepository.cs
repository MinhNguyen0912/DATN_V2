using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IimageRepository : IBaseRepository<DATN.Core.Model.Image>
    {
        ImagePaging GetImagePaging(ImagePaging request);
    }
}
