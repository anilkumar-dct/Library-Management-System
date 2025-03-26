using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystemUsingMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [DisplayName("Book Title")]
       
        public string BookTitle { get; set; }

        [Required(ErrorMessage = "Author name is required.")]
        [RegularExpression(@"^[a-zA-Z\s.,'’\-]+$", ErrorMessage = "Author name must contain only letters, spaces, and valid symbols (.,-'’).")]

        public string Author { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Genre must contain only letters and spaces.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Year field can't be empty.")]
        [Range(1900, 2025, ErrorMessage = "Year must be between 1900 and 2025.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please select the status.")]
        public bool? Status { get; set; } // Nullable bool for validation

    }
}
