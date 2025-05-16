using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework1.Models
{
    public class AccountBook
    {
        public Guid Id { get; set; }

        [Required]
        [Column("Categoryyy")]
        [Range(0, 1, ErrorMessage = "Category 只能選擇收入(0)或支出(1)")]
        public int Category { get; set; }

        [Required]
        [Column("Amounttt")]
        [Range(1,int.MaxValue,ErrorMessage = "{0}要大於0")]
        public int Amount { get; set; }

        [Required]
        [Column("Dateee")]
        public DateTime Date { get; set; }

        [Column("Remarkkk")]
        [MaxLength(100,ErrorMessage ="{0}不能超過100字")]
        public string Remark { get; set; } = string.Empty; // Initialize with a default non-null value
    }
}
