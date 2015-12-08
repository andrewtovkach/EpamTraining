using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class SaleInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInfo FileInfo { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [CostValidation(ErrorMessage = "Incorrect cost")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(3, ErrorMessage = "Incorrect currency code")]
        public string Currency { get; set; }
    }
}
