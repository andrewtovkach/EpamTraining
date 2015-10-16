using System;

namespace Mediateka.Model
{
    abstract class MediaItem : IComparable<MediaItem>
    {
        public string Name { get; set; }
        public double SizeFile { get; set; }
        public DateTime CreationDate { get; set; }
        public Popularity Popularity { get; set; }

        protected MediaItem() { }

        protected MediaItem(string name, double sizeFile, DateTime creationDate, 
            Popularity popularity)
        {
            this.Name = name;
            this.SizeFile = sizeFile;
            this.CreationDate = creationDate;
            this.Popularity = popularity;
        }

        public int CompareTo(MediaItem obj)
        {
            return String.Compare(this.Name, obj.Name, StringComparison.Ordinal);
        }

        public abstract string Print(int indent);
        public abstract double TotalSize { get;  }
        public abstract int AvgPopularity { get; }
        public abstract void Add(MediaItem item);
        public abstract bool Remove(MediaItem item);

        public override string ToString()
        {
            return String.Format("\"{0}\" Размер: {1} MB Дата созд.: {2}, {3}", Name, SizeFile,
                CreationDate.ToShortDateString(), new string((char)3, (int)Popularity + 1));
        }
    }
}
