using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IBatchRepository:IBaseRepository<Batch>
    {
        BatchPaging batchPaging(BatchPaging request);
        Batch GetByIdCustom(int id);
        Task<bool> AnyAsync(Expression<Func<Batch, bool>> predicate);
    }
}
