using Homework1.Data;
using Homework1.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework1.Services
{
    public interface IAccountBookService
    {
        Task<List<RecordViewModel>> GetLatestRecordViewModelsAsync(int count);
    }

    public class AccountBookService : IAccountBookService
    {
        private readonly AppDbContext _context;

        public AccountBookService(AppDbContext context)
        {
            _context = context;
        }

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
    }
}
