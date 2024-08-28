using DATN.Core.Models;

namespace DATN.Core.Model
{
    public class Origin : BaseEntity
    {
        public ICollection<Product_EAV.Product_EAV>? Products { get; set; }

    }
}
