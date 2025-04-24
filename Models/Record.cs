using System.ComponentModel.DataAnnotations;

namespace Homework1.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 2)]
        public int Category { get; set; }

        [Required]
        [Range(0, 100000000)]
        public int Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Note { get; set; }
    }
}
