using library_system.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    public class User
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        public int? CreatedBy { get; set; }

        public User? Creator { get; set; }

        public List<Author>? Authors { get; set; }

        public List<Book>? Books { get; set; }


        public List<User>? Users { get; set; }

        public List<Pubblisher>? Publishers { get; set; }

        public List<Tipology>? Tipologies { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}



