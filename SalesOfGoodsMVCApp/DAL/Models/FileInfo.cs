using System;

namespace DAL.Models
{
    public class FileInfo : IEquatable<FileInfo>
    {
        public int Id { get; set; }
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }

        public FileInfo(Manager manager, DateTime date)
        {
            Manager = manager;
            Date = date;
        }

        public FileInfo()
        {
            
        }

        public bool Equals(FileInfo other)
        {
            return Manager != null && Manager.FirstName == other.Manager.FirstName && 
                Manager.SecondName == other.Manager.SecondName && Date == other.Date;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as FileInfo);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} - Manager: {1} {2}", Id, Manager, Date.ToShortDateString());
        }
    }
}
