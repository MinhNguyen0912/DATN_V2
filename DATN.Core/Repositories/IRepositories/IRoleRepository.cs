using DATN.Core.Infrastructures;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Identity;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IRoleRepository : IBaseRepository<IdentityRole<Guid>>
    {
        List<string> getRolesName();
        RolePaging GetRolePaging(RolePaging request);
    }
}
