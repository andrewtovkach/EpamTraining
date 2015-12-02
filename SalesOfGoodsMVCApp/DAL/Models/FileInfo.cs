using System;

namespace DAL.Models
{
    public class FileInfo
    {
        public int Id { get; set; }
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }
    }
}
