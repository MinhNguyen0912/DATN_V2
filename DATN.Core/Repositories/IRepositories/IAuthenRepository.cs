using DATN.Core.Models;
using DATN.Core.ViewModels;
using DATN.Core.ViewModels.AuthenViewModel;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IAuthenRepository
    {
        Task<ResponseViewModel> AccessByEmail(EmailLoginViewModel request);
        Task<ResponseViewModel> CreateAccountByGoogle(AppUser account, string Password);
        Task<bool> IsAccountExisted(string email);
    }
}
