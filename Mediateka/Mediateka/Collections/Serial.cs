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
    class Serial : MediaItem, ICollection, IDurationCollection
    {
        public string TVChannel { get; set; }
        public string Country { get; set; }
        private TimeSpan Duration { get; set; }

        private SortedDictionary<int, List<MediaItem>> dictionaryItems;

        public Serial()
        {
            dictionaryItems = new SortedDictionary<int, List<MediaItem>>();
        }

        public Serial(string name, DateTime creationDate, string TVChannel, string country)
        {
            dictionaryItems = new SortedDictionary<int, List<MediaItem>>();
            this.Name = name;
            this.TVChannel = TVChannel;
            this.Country = country;
            this.CreationDate = creationDate;
            this.SizeFile = 0;
            this.Popularity = Popularity.One;
        }

        readonly object syncRoot = new object();

        public IEnumerable<MediaItem> this[int key]
        {
            get { return dictionaryItems[key]; }
        }

        public void Add(int season, MediaItem item)
        {
            if (!dictionaryItems.ContainsKey(season))
            {
                dictionaryItems.Add(season, new List<MediaItem>());
                dictionaryItems[season].Add(item);
            }
            else dictionaryItems[season].Add(item);
            this.SizeFile = this.TotalSize;
            this.Popularity = this.GetAvgPopularity;
            this.Duration = TotalDuration;
        }

        public override void Add(MediaItem item)
        {
            this.Add(1, item);
        }

        public void Clear()
        {
            dictionaryItems.Clear();
        }

        public bool Contains(MediaItem item)
        {
            foreach (var i in dictionaryItems)
            {
                if (i.Value.Contains(item))
                    return true;
            }
            return false;
        }

        public override bool Remove(MediaItem item)
        {
            foreach (var i in dictionaryItems)
            {
                if (i.Value.Remove(item))
                {
                    this.SizeFile = this.TotalSize;
                    this.Popularity = this.GetAvgPopularity;
                    this.Duration = TotalDuration;
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            var listItems = GetListVideo();
            int j = index;
            for (int i = 0; i < listItems.Count; i++)
            {
                array.SetValue(listItems[i], j);
                j++;
            }
        }

        public object SyncRoot
        {
            get { return syncRoot; }
        }

        public int Count
        {
            get { return dictionaryItems.Keys.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in dictionaryItems.Values)
            {
                foreach (var i in item)
                    yield return i;
            }
        }

        public override string ToString()
        {
            return String.Format(new string((char)16, 3) + " \"{0}\" - {1} ({2}) Дата выхода: {3} " +
            "Общ. размер: {4} MB Общ. продолж.: {5}, {6}", Name, TVChannel, Country, CreationDate.ToShortDateString(),
            TotalSize, TotalDuration, new string((char)3, (int)Popularity + 1));
        }

        private List<MediaItem> GetListVideo()
        {
            List<MediaItem> listVideo = new List<MediaItem>();
            foreach (var item in dictionaryItems.Values)
                listVideo.AddRange(item);
            return listVideo;
        }

        public IEnumerable<MediaItem> GetNewMediaItems(int countItems)
        {
            var listVideo = GetListVideo();
            listVideo.Sort(new ComparerByCreationDate());
            return listVideo.Take(Math.Min(listVideo.Count, countItems));
        }

        public IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems)
        {
            var listVideo = GetListVideo();
            listVideo.Sort(new ComparerByPopularity());
            return listVideo.Take(Math.Min(listVideo.Count, countItems));
        }

        public override double TotalSize
        {
            get
            {
                var listItems = GetListVideo();
                return listItems.Sum(x => x.TotalSize);
            }
        }

        public override string Print(int indent)
        {
            indent += 3;
            StringBuilder sb = new StringBuilder();
            sb.Append(new string(' ', indent));
            sb.Append(this.ToString() + "\n");
            foreach (var item in dictionaryItems)
            {
                sb.Append(new string(' ', indent + 6));
                sb.Append(" - " + item.Key + " cезон\n");
                item.Value.Sort();
                foreach (var i in item.Value)
                {
                    sb.Append(new string(' ', indent));
                    sb.Append(i.Print(indent) + "\n");
                }
            }
            return sb.ToString();
        }

        public override int AvgPopularity
        {
            get
            {
                var listItems = GetListVideo();
                if (listItems.Count != 0)
                    return (int) Math.Round(listItems.Average(x => x.AvgPopularity));
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
            var listItems = GetListVideo();
            foreach (var item in listItems)
            {
                var element = item as VideoItem;
                if(element != null)
                    result = result.Add(element.Duration);
            }
            return result;
        }

        public TimeSpan TotalDuration
        {
            get { return GetTotalDuration(); }
        }
    }
}
