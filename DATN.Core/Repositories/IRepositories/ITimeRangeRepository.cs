using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ITimeRangeRepository : IBaseRepository<TimeRange>
    {
        TimeRangePaging GetPartnerPaging(TimeRangePaging request);
    }
}
