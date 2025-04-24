using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public interface EntryViewModels
    {
        // 雖然是隱藏欄位，還是建議保留以便編輯時使用
        public int Id { get; set; }

        [Required(ErrorMessage = "請選擇類別")]
        [Range(1, 2, ErrorMessage = "類別代號只能是 1（收入）或 2（支出）")]
        [Display(Name = "類別")]
        public byte Category { get; set; }

        [Required(ErrorMessage = "請輸入金額")]
        [Range(0.01, double.MaxValue, ErrorMessage = "金額需大於 0")]
        [Display(Name = "金額")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "請選擇日期")]
        [DataType(DataType.Date)]
        [Display(Name = "日期")]
        public DateTime Date { get; set; }

        [StringLength(255, ErrorMessage = "備註最多 255 字")]
        [Display(Name = "備註")]
        public string Note { get; set; }

    }
}
