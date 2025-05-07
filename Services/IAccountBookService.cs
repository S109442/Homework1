using Homework1.Models;

namespace Homework1.Services
{
    public interface IAccountBookService
    {
        Task<List<RecordViewModel>> GetLatestRecordViewModelsAsync(int count);
        Task AddAccountBookAsync(AccountBook accountBook);
    }
}
