using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvc1.Models
{
    public class Category
    {
        [Key] // id is primary key and identity
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Must be between 1 and 100")]
        public string DisplayOrder { get; set; }
        
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
