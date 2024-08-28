using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModels.Paging;

namespace DATN.Core.ViewModel.Paging
{
    public class CommentPaging : PagingRequestBase<CommentVM>
    {

        public int? ProductId { get; set; }
        public int? StarRating { get; set; }
    }
}
