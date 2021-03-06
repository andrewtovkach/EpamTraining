﻿namespace DAL.Models
{
    public class UnverifiedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public UnverifiedFile(string fileName, int id = 0)
        {
            FileName = fileName;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, FileName);
        }
    }
}
