using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute_EAV = DATN.Core.Model.Product_EAV.Attribute_EAV;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IAttributeEAVRepository : IBaseRepository<Attribute_EAV>
    {
    }
}
