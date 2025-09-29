using Login.Models;
using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Tipology
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(300)] // nvarchar(100)
        public string Description { get; set; }

        // Log dates
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } // To handle soft delete later

        // log user
        public int? CreatedBy { get; set; }
        
        public User? User { get; set; } // The creator reference

        public ICollection<Book> Books { get; set; }
    }
}
