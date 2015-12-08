using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
    }
}
