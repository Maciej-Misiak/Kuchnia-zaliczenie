using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Food.Models
{
    public class LevelType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Poziom trudności")]
        [Required]
        [MaxLength(50)]

        public string Name { get; set; }
    }
}
