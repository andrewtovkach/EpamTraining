using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class Country
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        public string Name { get; set; }
    }
}
