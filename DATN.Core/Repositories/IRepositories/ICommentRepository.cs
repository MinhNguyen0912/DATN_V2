using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductCommentVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        CommentPaging GetCommentPaging(CommentPaging request);
        Task<CommentOverviewVM> GetCommentPagingByProductId(CommentPaging request);
    }
}
