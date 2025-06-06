﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework1.Data;
using Homework1.Models;
using Homework1.Services;
using Microsoft.Extensions.Options;
using Homework1.Helper;

namespace Homework1.Controllers
{
    public class RecordController : Controller
    {
        private readonly IAccountBookService _accountBookService;
        private readonly int _pageSize;
        private readonly ILogger<RecordController> _logger;

        public RecordController(IAccountBookService accountBookService, IOptions<PageSettings> pageSettings, ILogger<RecordController> logger)
        {
            _accountBookService = accountBookService;
            _pageSize = pageSettings.Value.PageSize;
            _logger = logger;
        }

        // GET: /Record
        public async Task<IActionResult> Index(int? page)

        {
            //int pageNumber = page.ToPageIndex(); 
            //int pageSize = _pageSize;

            var records = await _accountBookService.GetLatestRecordViewModelsAsync(page.ToPageIndex(), _pageSize);


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
                        // 將錯誤訊息輸出
                        _logger.LogError(error.ErrorMessage);
                    }
                }
                // 如果驗證失敗，重新載入最新的三筆資料
                //int pageNumber = page.ToPageIndex();
                //int pageSize = _pageSize;
                viewModel.LatestThreeRecords = await _accountBookService.GetLatestRecordViewModelsAsync(page.ToPageIndex(), _pageSize);
                return View("Index", viewModel);
            }

            // 儲存新資料
            var newAccountBook = new AccountBook
            {
                Id = Guid.NewGuid(),
                Category = viewModel.NewRecord.Category, // Corrected property name
                Amount = viewModel.NewRecord.Amount,
                Date = viewModel.NewRecord.Date,
                Remark = viewModel.NewRecord.Remark
            };

            await _accountBookService.AddAccountBookAsync(newAccountBook);
            // 重導向時保留頁碼
            return RedirectToAction("Index");
        }
    }
}
