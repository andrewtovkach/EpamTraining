using System;
using System.Collections.Generic;
using System.IO;

namespace BL
{
    public class Watcher
    {
        public ICollection<FileState> Collection;

        public string Path { get; set; }
        public string Filter { get; set; }

        public Watcher(string path, string filter)
        {
            Collection = new List<FileState>();
            Path = path;
            Filter = filter;
        }

        public void Run(Func<bool> action)
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = this.Path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = this.Filter
            };
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.EnableRaisingEvents = true;
            while (action.Invoke());
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Collection.Add(new FileState { ChangeType = e.ChangeType, FullPath = e.FullPath });
        }
    }
}
