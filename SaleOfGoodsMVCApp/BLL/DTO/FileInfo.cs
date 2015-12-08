using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class FileInfo
    {
        public int Id { get; set; }
        public Manager Manager { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
