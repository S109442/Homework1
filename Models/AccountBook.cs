using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class AccountBook
    {
        public Guid Id { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Category 只能選擇收入(0)或支出(1)")]
        public int Categoryyy { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Amount要大於0")]
        public int Amounttt { get; set; }

        [Required]
        public DateTime Dateee { get; set; }

        [MaxLength(500,ErrorMessage ="Note不能超過500字")]
        public string Remarkkk { get; set; } = string.Empty; // Initialize with a default non-null value
    }
}
