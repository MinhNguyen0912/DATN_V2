using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModels.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public List<Category> GetLevel1AndChild(int categoryId);
        CategoryPaging CategoryPaging(CategoryPaging request);
        CategoryPagingLv1 CategoryPagingLv1(CategoryPagingLv1 request);
        CategoryPagingLv2 CategoryPagingLv2(CategoryPagingLv2 request);

        public Category GetCateByIdCustom(int id);

    }
}
