using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class RecordViewModel
    {
        public int Id { get; set; } // 自動編號（顯示用）

        [Required]
        [Range(1, 2, ErrorMessage = "請選擇收入或支出")]
        public int Category { get; set; } // 1: 收入, 2: 支出

        public string CategoryDisplay => Category == 1 ? "收入" :
                                         Category == 2 ? "支出" : "未知";

        [Required]
        [Range(0, 100000000)] // 同樣允許 0
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string Note { get; set; }
    }

    public class RecordPageViewModel
    {
        // 上方表單使用的資料模型
        public RecordViewModel NewRecord { get; set; } = new RecordViewModel();

        // 下方顯示用的三筆資料
        public List<RecordViewModel> LatestThreeRecords { get; set; } = new List<RecordViewModel>();
    }
}
