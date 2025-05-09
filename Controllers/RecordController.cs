using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework1.Data;
using Homework1.Models;
using Homework1.Services;

namespace Homework1.Controllers
{
    public class RecordController : Controller
    {
        private readonly IAccountBookService _accountBookService;

        public RecordController(IAccountBookService accountBookService)
        {
            _accountBookService = accountBookService;
        }

        // GET: /Record
        public async Task<IActionResult> Index(int? page)

        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var records = await _accountBookService.GetLatestRecordViewModelsAsync(pageNumber, pageSize);


            var viewModel = new RecordPageViewModel
            {
                NewRecord = new RecordViewModel()
                {
                    Date = DateTime.Today // 設定預設日期為今天
                },
                LatestThreeRecords = (X.PagedList.IPagedList<RecordViewModel>)records
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
                int pageNumber = page ?? 1;
                int pageSize = 5;
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
