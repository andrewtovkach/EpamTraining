using Mediateka.Collections;
using Mediateka.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateka.Model
{
    class MediatekaClass : IEnumerable, IMediaCollection
    {
        private Folder root;

        public MediatekaClass()
        {
            root = new Folder();
        }

        public MediatekaClass(string name, DateTime creationDate, bool isReadOnly)
        {
            root = new Folder(name, creationDate, isReadOnly);
        }

        public MediaItem this[int index]
        {
            get { return root[index]; }
        }

        public MediaItem this[string name]
        {
            get { return root[name]; }
        }

        public void Add(MediaItem item)
        {
            root.Add(item);
        }

        public bool Remove(MediaItem item)
        {
            return root.Remove(item);
        }

        public int Count
        {
            get { return root.Count; }
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public string Print()
        {
            string str = new string('-', 100) + "\n";
            str += root.Print(-3);
            str += new string('-', 100) + "\n";
            return str;
        }

        public IEnumerator GetEnumerator()
        {
 	        return root.GetEnumerator();
        }

        public IEnumerable<MediaItem> GetNewMediaItems(int countItems)
        {
            return root.GetNewMediaItems(countItems);
        }

        public IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems)
        {
            return root.GetMostPopularMediaItems(countItems);
        }

        public double TotalSize
        {
            get { return root.TotalSize; }
        }

        public Popularity GetAvgPopularity
        {
            get { return root.GetAvgPopularity; }
        }
    }
}
