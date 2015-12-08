using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class Manager
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+\d{3}\(\d{2,4}\)\d{1,3}-\d{2}-\d{2}$", ErrorMessage = "Incorrect telephone number")]
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect e-mail")]
        public string Email { get; set; }
    }
}
