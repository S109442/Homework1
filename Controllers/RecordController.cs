using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework1.Data;
using Homework1.Models;
using Homework1.Services;
using Microsoft.Extensions.Options;

namespace Homework1.Controllers
{
    public class RecordController : Controller
    {
        private readonly IAccountBookService _accountBookService;
        private readonly int _pageSize;

        public RecordController(IAccountBookService accountBookService, IOptions<PageSettings> pageSettings)
        {
            _accountBookService = accountBookService;
            _pageSize = pageSettings.Value.PageSize;
        }

        // GET: /Record
        public async Task<IActionResult> Index(int? page)

        {
            int pageNumber = (page.HasValue && page.Value > 0) ? page.Value : 1; 
            int pageSize = _pageSize;

            var records = await _accountBookService.GetLatestRecordViewModelsAsync(pageNumber, pageSize);


            var viewModel = new RecordPageViewModel
            {
                NewRecord = new RecordViewModel()
                {
                    Date = DateTime.Today // 設定預設日期為今天
                },
                LatestThreeRecords = records
            };

            return View(viewModel);
        }

        // POST: /Record/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RecordPageViewModel viewModel, int? page)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // 將錯誤訊息記錄到開發環境的輸出中
                        System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                    }
                }
                // 如果驗證失敗，重新載入最新的三筆資料
                int pageNumber = (page.HasValue && page.Value > 0) ? page.Value : 1;
                int pageSize = _pageSize;
                viewModel.LatestThreeRecords = await _accountBookService.GetLatestRecordViewModelsAsync(pageNumber, pageSize);
                return View("Index", viewModel);
            }

            // 儲存新資料
            var newAccountBook = new AccountBook
            {
                Id = Guid.NewGuid(),
                Categoryyy = viewModel.NewRecord.Category-1, // Corrected property name
                Amounttt = viewModel.NewRecord.Amount,
                Dateee = viewModel.NewRecord.Date,
                Remarkkk = viewModel.NewRecord.Note
            };

            await _accountBookService.AddAccountBookAsync(newAccountBook);
            // 重導向時保留頁碼
            return RedirectToAction("Index", new { page });
        }
    }
}
