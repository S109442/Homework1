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
            var latestThree = await _context.Records
                .OrderByDescending(r => r.Id)
                .Take(3)
                .Select(r => new RecordViewModel
                {
                    Id = r.Id,
                    Category = r.Category,
                    Amount = r.Amount,
                    Date = r.Date,
                    Note = r.Note
                })
                .ToListAsync();

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
                // 若表單驗證失敗，重新載入新三筆資料顯示
                viewModel.LatestThreeRecords = await _context.Records
                    .OrderByDescending(r => r.Id)
                    .Take(3)
                    .Select(r => new RecordViewModel
                    {
                        Id = r.Id,
                        Category = r.Category,
                        Amount = r.Amount,
                        Date = r.Date,
                        Note = r.Note
                    })
                    .ToListAsync();

                return View("Index", viewModel);
            }

            // 儲存新資料
            var newRecord = new Record
            {
                Category = viewModel.NewRecord.Category,
                Amount = viewModel.NewRecord.Amount,
                Date = viewModel.NewRecord.Date,
                Note = viewModel.NewRecord.Note
            };

            _context.Records.Add(newRecord);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
