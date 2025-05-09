using Homework1.Data;
using Homework1.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.EF;

namespace Homework1.Services
{
    public class AccountBookService(AppDbContext _context) : IAccountBookService
    {
        public async Task<IPagedList<RecordViewModel>> GetLatestRecordViewModelsAsync(int pageNumber, int pageSize)
        {
            var records = await _context.AccountBook
               .OrderByDescending(r => r.Dateee)
               .Select(r => new RecordViewModel
               {
                   Id = 0,//移除序號計算，預設為0 
                   Category = r.Categoryyy + 1,
                   Amount = r.Amounttt,
                   Date = r.Dateee,
                   Note = r.Remarkkk
               })
               .ToPagedListAsync(pageNumber, pageSize);

            return (IPagedList<RecordViewModel>)records;
        }

        public async Task AddAccountBookAsync(AccountBook accountBook)
        {
            _context.AccountBook.Add(accountBook);
            await _context.SaveChangesAsync();
        }
    }
}
