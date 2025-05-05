using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework1.Data;
using Homework1.Models;

namespace Homework1.Controllers
{
    public class RecordController : Controller
    {
        private readonly AppDbContext _context;

        public RecordController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Record
        public async Task<IActionResult> Index()
        {
            // 先取得資料後，再產生順序號碼
            var latestThreeRecords = await _context.AccountBook
                .OrderByDescending(r => r.Dateee)
                .Take(5)
                .ToListAsync();

            var latestThree = latestThreeRecords
                .Select((r, index) => new RecordViewModel
                {
                    // 產生從1開始的順序號碼
                    Id = index + 1,
                    Category = r.Categoryyy+1,
                    Amount = r.Amounttt,
                    Date = r.Dateee,
                    Note = r.Remarkkk
                })
                .ToList();

            var viewModel = new RecordPageViewModel
            {
                NewRecord = new RecordViewModel()
                {
                    Date = DateTime.Today // 設定預設日期為今天
                },
                LatestThreeRecords = latestThree
            };

            return View(viewModel);
        }

        // POST: /Record/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RecordPageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // 若表單驗證失敗，先取得資料，再產生順序號碼
                var latestThreeRecords = await _context.AccountBook
                    .OrderByDescending(r => r.Dateee)
                    .Take(5)
                    .ToListAsync();

                viewModel.LatestThreeRecords = latestThreeRecords
                    .Select((r, index) => new RecordViewModel
                    {
                        // 產生從1開始的順序號碼
                        Id = index + 1,
                        Category = r.Categoryyy+1,
                        Amount = r.Amounttt,
                        Date = r.Dateee,
                        Note = r.Remarkkk
                    })
                    .ToList();

                return View("Index", viewModel);
            }

            // 儲存新資料
            var newAccountBook = new AccountBook
            {
                Categoryyy = viewModel.NewRecord.Category-1, // Corrected property name
                Amounttt = viewModel.NewRecord.Amount,
                Dateee = viewModel.NewRecord.Date,
                Remarkkk = viewModel.NewRecord.Note
            };

            _context.AccountBook.Add(newAccountBook);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
