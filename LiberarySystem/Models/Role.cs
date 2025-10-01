using System.ComponentModel.DataAnnotations;

namespace LiberarySystem.Models
{
    public class Role
    {
           
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // limit role name length
        public string Name { get; set; } 
    }
}
    

