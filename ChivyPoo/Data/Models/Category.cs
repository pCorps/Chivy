using System.ComponentModel.DataAnnotations;

namespace ChivyPoo.Data.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Category Name.")]
        public string Name { get; set; }
    }
}
