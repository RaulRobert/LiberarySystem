using Login.Models;
using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Author
    {
        [Key]
        [Required]// Explicit primary key
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string FirstName { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime? DeadDate { get; set; }

        // Log dates
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } 

        // log user
        public int? CreatedBy { get; set; }
        
        public User? Creator { get; set; } // The creator reference

        public ICollection<Book> Books { get; set; }
    }
}
