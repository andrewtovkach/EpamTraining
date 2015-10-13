using Mediateka.Model;
using Mediateka.Comparers;
using Mediateka.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateka.Collections
{
    class Folder : MediaItem, ICollection, IMediaCollection
    {
        public bool IsReadOnly { get; set; }

        private List<MediaItem> listItems;

        public Folder()
        {
            listItems = new List<MediaItem>();
        }

        public Folder(string name, DateTime creationDate, bool isReadOnly)
        {
            listItems = new List<MediaItem>();
            this.Name = name;
            this.CreationDate = creationDate;
            this.IsReadOnly = isReadOnly;
            this.SizeFile = 0;
            this.Popularity = Popularity.One;
        }

        readonly object syncRoot = new object();

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
            this.SizeFile = this.TotalSize;
            this.Popularity = this.GetAvgPopularity;
        }

        public override bool Remove(MediaItem item)
        {
            bool ok = listItems.Remove(item);
            this.SizeFile = this.TotalSize;
            this.Popularity = this.GetAvgPopularity;
            return ok;
        }

        public void CopyTo(Array array, int index)
        {
            int j = index;
            for (int i = 0; i < listItems.Count; i++)
            {
                array.SetValue(listItems[i], j);
                j++;
            }
        }

        public int Count
        {
            get { return listItems.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { return syncRoot; }
        }

        public IEnumerator GetEnumerator()
        {
            return listItems.GetEnumerator();
        }

        public override string ToString()
        {
            return String.Format(new string('>', 3) + " \"{0}\" Дата созд.: {1} ReadOnly: {2} " +
            "Общ. размер: {3} MB, {4}", Name, CreationDate.ToShortDateString(), IsReadOnly,
            TotalSize, new string((char)3, (int)Popularity + 1));
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
            sb.Append(this.ToString() + "\n");
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
                    return (int) Math.Round(listItems.Average(x => x.AvgPopularity));
                return 1;
            }
        }

        public Popularity GetAvgPopularity
        {
            get { return (Popularity)AvgPopularity; }
        }
    }
}
