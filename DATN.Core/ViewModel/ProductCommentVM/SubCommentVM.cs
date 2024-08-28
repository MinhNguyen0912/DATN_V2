using DATN.Core.Enum;

namespace DATN.Core.ViewModel.ProductCommentVM
{
    public class SubCommentVM
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } // Assuming you want to display user name
        public CommentType Type { get; set; }
    }
}
