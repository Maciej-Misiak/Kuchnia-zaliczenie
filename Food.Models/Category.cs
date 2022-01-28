using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Food.Models
{
    public class Category
    {
        [Key]     
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nazwa kategori powinna zawierać od 1 do 50 znaków!", MinimumLength = 2)]
        [DisplayName("Nazwa kategorii")]
        public string Name { get; set; }
        
    } 
}