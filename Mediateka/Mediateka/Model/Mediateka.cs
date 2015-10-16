using Mediateka.Collections;
using Mediateka.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mediateka.Model
{
    class MediatekaClass : IEnumerable, IMediaCollection
    {
        private Folder _root;

        public MediatekaClass(string name, DateTime creationDate, bool isReadOnly)
        {
            _root = new Folder(name, creationDate, isReadOnly);
        }

        public MediaItem this[int index]
        {
            get { return _root[index]; }
        }

        public MediaItem this[string name]
        {
            get { return _root[name]; }
        }

        public void Add(MediaItem item)
        {
            _root.Add(item);
        }

        public bool Remove(MediaItem item)
        {
            return _root.Remove(item);
        }

        public int Count
        {
            get { return _root.Count; }
        }

        public override string ToString()
        {
            return _root.ToString();
        }

        public string Print()
        {
            string str = new string('-', 100) + "\n";
            str += _root.Print(-3);
            str += new string('-', 100) + "\n";
            return str;
        }

        public IEnumerator GetEnumerator()
        {
 	        return _root.GetEnumerator();
        }

        public IEnumerable<MediaItem> GetNewMediaItems(int countItems)
        {
            return _root.GetNewMediaItems(countItems);
        }

        public IEnumerable<MediaItem> GetMostPopularMediaItems(int countItems)
        {
            return _root.GetMostPopularMediaItems(countItems);
        }

        public double TotalSize
        {
            get { return _root.TotalSize; }
        }

        public Popularity GetAvgPopularity
        {
            get { return _root.GetAvgPopularity; }
        }
    }
}
