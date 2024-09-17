using DATN.Core.Enum;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProductCommentVM;

namespace DATN.Core.ViewModel.ProdutEAVVM;

public class Product_EAV_VM
{
    public ProductVM_EAV Product { get; set; }
    public CommentOverviewVM CommentOverviewVm { get; set; }
}