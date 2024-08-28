using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModels.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IMagazineRepository : IBaseRepository<Magazine>
    {
        MagazinePaging GetMagazinePaging(MagazinePaging request);
    }
}
