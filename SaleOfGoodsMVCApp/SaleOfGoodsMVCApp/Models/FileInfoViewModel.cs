using System.Collections.Generic;
using System.Web.Mvc;
using BLL.DTO;

namespace SaleOfGoodsMVCApp.Models
{
    public class FileInfoViewModel
    {
        public IEnumerable<FileInfo> FileInfos { get; set; }
        public SelectList Managers { get; set; }
    }
}