using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        public string Name { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }
    }
}
