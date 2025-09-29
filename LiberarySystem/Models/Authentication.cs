using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login.Models
{
    public class Authentication
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }
        public string Role { get; set; } = "User"; // default is User
    

    }
}
