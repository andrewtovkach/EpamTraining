using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;

namespace SalesOfGoodsMVCApp.Models
{
    public class FileInfoViewModel
    {
        public IEnumerable<FileInfo> FileInfos { get; set; }
        public SelectList Managers { get; set; }
    }
}