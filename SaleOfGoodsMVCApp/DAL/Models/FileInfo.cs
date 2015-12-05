using System;

namespace DAL.Models
{
    public class FileInfo : BaseClass, IEquatable<FileInfo>
    {
        public Manager Manager { get; set; }
        public DateTime Date { get; set; }

        public FileInfo(Manager manager, DateTime date, int id = 0)
        {
            Manager = manager;
            Date = date;
            Id = id;
        }

        public FileInfo()
        {
            Manager = new Manager();
        }

        public bool Equals(FileInfo other)
        {
            return other != null && Manager.Equals(other.Manager) && Date == other.Date;
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
            return string.Format("Manager: {0} {1}", Manager, Date.ToShortDateString());
        }
    }
}
