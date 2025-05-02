using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class AccountBook
    {
        public Guid Id { get; set; }

        [Required]
        [Range(1, 2)]
        public int Categoryyy { get; set; }

        [Required]
        [Range(1, 100000000)]
        public int Amounttt { get; set; }

        [Required]
        public DateTime Dateee { get; set; }

        [MaxLength(500)]
        public string Remarkkk { get; set; } = string.Empty; // Initialize with a default non-null value
    }
}
