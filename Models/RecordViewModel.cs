using Homework1.Models.Validations;
using System.ComponentModel.DataAnnotations;

using X.PagedList;

namespace Homework1.Models
{
    public class RecordViewModel
    {
        public int Id { get; set; } // 自動編號（顯示用）

        [Required]
        [Range(0, 1, ErrorMessage = "請選擇收入或支出")]
        public int Category { get; set; } // 0: 收入, 1: 支出

        public string CategoryDisplay => Category == 0 ? "收入" :
                                         Category == 1 ? "支出" : "未知";

        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="{0}超出範圍!")]
        [PositiveInteger(ErrorMessage = "{0}需要為正整數!")]
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [NotFutureDate(ErrorMessage = "{0}不能大於今天")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "請輸入{0}")]
        [StringLength(100, ErrorMessage = "{0}不能超過100字")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; } = string.Empty; // Initialize with a default non-null value
    }

    public class RecordPageViewModel
    {
        // 上方表單使用的資料模型
        public RecordViewModel NewRecord { get; set; } = new RecordViewModel();
        public IPagedList<RecordViewModel>? LatestThreeRecords { get; set; }
        // 下方顯示用的三筆資料
        //public List<RecordViewModel> LatestThreeRecords { get; set; } = new List<RecordViewModel>();
    }
}
