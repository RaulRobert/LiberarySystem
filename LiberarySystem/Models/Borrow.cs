using library_system.Models;
using Login.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberarySystem.Models
{
    public class Borrow
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        [ForeignKey("Authentication_Id")]
        [Required]
        public int AuthenticationId { get; set; }
        public Authentication Authentication { get; set; }

        [ForeignKey("Book_Id")]
        [Required]
        public int BookId { get; set; }
        public Book book { get; set; }

        [NotMapped]
        public bool IsBorrowed { get; set; }


    }
}
