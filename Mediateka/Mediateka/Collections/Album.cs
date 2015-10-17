using Mediateka.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mediateka.Comparers;
using Mediateka.Model;
using System.Collections;

namespace Mediateka.Collections
{
    class Album : MediaItem, ICollection, IDurationCollection
    {
        public string Artist { get; set; }
        private TimeSpan Duration { get; set; }

        private readonly List<MediaItem> listItems;

        public Album(string name, DateTime creationDate, string artist) 
        {
            listItems = new List<MediaItem>();
            this.Name = name;
            this.CreationDate = creationDate;
            this.Artist = artist;
            this.SizeFile = 0;
            this.Popularity = Popularity.One;
        }

        readonly object _syncRoot = new object();

        public MediaItem this[int index]
        {
            get { return listItems[index]; }
        }

        public MediaItem this[string name]
        {
            get { return listItems.FirstOrDefault(x => x.Name.Contains(name)); }
        }

        public override void Add(MediaItem item)
        {
            listItems.Add(item);
            SizeFile = TotalSize;
            Popularity = GetAvgPopularity;
            Duration = TotalDuration;
        }

        public void Clear()
        {
            listItems.Clear();
        }

        public bool Contains(MediaItem item)
        {
            return listItems.Contains(item);
        }

        public override bool Remove(MediaItem item)
        {
            bool ok = listItems.Remove(item);
            SizeFile = TotalSize;
            Popularity = GetAvgPopularity;
            Duration = TotalDuration;
            return ok;
        }

        public void CopyTo(Array array, int index)
        {
            int j = index;
            foreach (MediaItem t in listItems)
            {
                array.SetValue(t, j);
                j++;
            }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        public int Count
        {
            get { return listItems.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public IEnumerator GetEnumerator()
        {
            return listItems.GetEnumerator();
        }

        public override string ToString()
        {
            return String.Format(new string((char)14, 3) + " \"{0}\" - {1} Дата созд.: {2} " +
            "Общ. размер: {3} MB Общ. продолж.: {4}, {5}", Name, Artist, CreationDate.ToShortDateString(),
            TotalSize, TotalDuration, new string((char)3, (int)Popularity + 1));
        }

        public IEnumerable<MediaItem> GetNewMediaItems(int countItems)
        {
            listItems.Sort(new ComparerByCreationDate());
            return listItems.Take(Math.Min(listItems.Count, countItems));
        }

        public IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems)
        {
            listItems.Sort(new ComparerByPopularity());
            return listItems.Take(Math.Min(listItems.Count, countItems));
        }

        public override double TotalSize
        {
            get { return listItems.Sum(x => x.TotalSize); }
        }

        public override string Print(int indent)
        {
            indent += 3;
            StringBuilder sb = new StringBuilder();
            sb.Append(new string(' ', indent));
            sb.Append(this + "\n");
            listItems.Sort();
            foreach (var item in listItems)
            {
                sb.Append(new string(' ', indent));
                sb.Append(item.Print(indent) + "\n");
            }
            return sb.ToString();
        }

        public override int AvgPopularity
        {
            get
            {
                if (listItems.Count != 0)
                    return (int)Math.Round(listItems.Average(x => x.AvgPopularity));
                return 1;
            }
        }

        public Popularity GetAvgPopularity
        {
            get { return (Popularity)AvgPopularity; }
        }

        private TimeSpan GetTotalDuration()
        {
            TimeSpan result = new TimeSpan();
            return listItems.OfType<AudioItem>().Aggregate(result, (current, element) => current.Add(element.Duration));
        }

        public TimeSpan TotalDuration
        {
            get { return GetTotalDuration(); }
        }
    }
}
