using Homework1.Models;
using X.PagedList;

namespace Homework1.Services
{
    public interface IAccountBookService
    {
        Task<IPagedList<RecordViewModel>> GetLatestRecordViewModelsAsync(int pageNumber, int pageSize);
        Task AddAccountBookAsync(AccountBook accountBook);
    }
}
