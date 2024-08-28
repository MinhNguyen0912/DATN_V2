using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = DATN.Core.Model.Product_EAV.Attribute;

namespace DATN.Core.Repositories.IRepositories.ProductEAV
{
    public interface IAttributeRepository : IBaseRepository<Attribute>
    {
    }
}
