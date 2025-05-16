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
               .OrderByDescending(r => r.Date)
               .Select(r => new RecordViewModel
               {
                   Id = 0,//移除序號計算，預設為0 
                   Category = r.Category,
                   Amount = r.Amount,
                   Date = r.Date,
                   Remark = r.Remark
               })
               .ToPagedListAsync(pageNumber, pageSize);

            return records;
        }

        public async Task AddAccountBookAsync(AccountBook accountBook)
        {
            _context.AccountBook.Add(accountBook);
            await _context.SaveChangesAsync();
        }
    }
}
