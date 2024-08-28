using DATN.Core.Infrastructures;
using DATN.Core.Model;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ICategoryTimeRangeRepository : IBaseRepository<CategoryTimeRange>
    {
        public new List<CategoryTimeRange> GetAll();

        public List<CategoryTimeRange> GetTimeRanebyCateId(int id);
        public List<CategoryTimeRange> GetCateTimeByTimeRangeId(int id);
    }
}
