using System.Security.AccessControl;
using DATN.Core.ViewModels.UserViewModel;

namespace DATN.Core.ViewModel.SaleProductVM;

public class SaleProuductVM
{
    public int TabIndex { get; set; }
    public int ProductCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? DicountAmount { get; set; }
    public decimal? MustPay { get; set; }
    public decimal? ChangeMoney { get; set; }
    public decimal? CustomerGivenMoney { get; set; }
    public string? Note { get; set; }
    public string? BacthName { get; set; }
    public int? BatchId { get; set; } 
    public int? VoucherId { get; set; } 
     public QuickCreateUserVM QuickCreateUserVM { get; set; }
    public string? TabName { get; set; }
    public List<ShoppingTab> ShoppingTabs { get; set; }
}

public class ShoppingTab
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Variant { get; set; }
    public int VariantId { get; set; }
    public decimal? Price { get; set; }
    public int Quantity { get; set; }
}