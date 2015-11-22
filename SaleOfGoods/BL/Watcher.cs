using System;
using System.Collections.Generic;
using System.IO;

namespace BL
{
    public class Watcher
    {
        public ICollection<FileInformation> Collection;

        public string Path { get; set; }
        public string Filter { get; set; }

        public Watcher(string path, string filter)
        {
            Collection = new List<FileInformation>();
            Path = path;
            Filter = filter;
        }

        public void Run()
        {
            var watcher = new FileSystemWatcher
            {
                Path = this.Path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = this.Filter
            };
            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;
            while (true) ;
        }

        public event EventHandler<FileInformation> CreatedFile;

        protected virtual void OnCreatedFile(FileInformation e)
        {
            if (CreatedFile != null)
                CreatedFile(this, e);
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            var fileState = new FileInformation { ChangeType = e.ChangeType, FullPath = e.FullPath };
            Collection.Add(fileState);
            OnCreatedFile(fileState);
        }
    }
}
