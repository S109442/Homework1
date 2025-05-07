using Homework1.Data;
using Homework1.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework1.Services
{
    public class AccountBookService(AppDbContext _context) : IAccountBookService
    {
        public async Task<List<RecordViewModel>> GetLatestRecordViewModelsAsync(int count)
        {
            var latestRecords = await _context.AccountBook
                .OrderByDescending(r => r.Dateee)
                .Take(count)
                .ToListAsync();

            var viewModels = latestRecords
                .Select((r, index) => new RecordViewModel
                {
                    // 產生從1開始的順序號碼
                    Id = index + 1,
                    Category = r.Categoryyy + 1,
                    Amount = r.Amounttt,
                    Date = r.Dateee,
                    Note = r.Remarkkk
                })
                .ToList();

            return viewModels;
        }
        public async Task AddAccountBookAsync(AccountBook accountBook)
        {
            _context.AccountBook.Add(accountBook);
            await _context.SaveChangesAsync();
        }
    }
}
