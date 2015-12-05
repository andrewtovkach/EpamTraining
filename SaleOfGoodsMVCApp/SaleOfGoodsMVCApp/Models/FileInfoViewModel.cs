using System.Collections.Generic;
using DAL.Models;
using System.Web.Mvc;

namespace BLL
{
    public class FileInfoViewModel
    {
        public IEnumerable<FileInfo> FileInfos { get; set; }
        public SelectList Managers { get; set; }
    }
}