using System;
using System.IO;
using BL.Interfaces;

namespace BL.Model
{
    public class Watcher : IWatcher
    {
        public string Path { get; set; }
        public string Filter { get; set; }

        public Watcher(string path, string filter)
        {
            Path = path;
            Filter = filter;
        }

        public void Run(Func<bool> func)
        {
            var watcher = new FileSystemWatcher
            {
                Path = this.Path,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName
                                | NotifyFilters.DirectoryName,
                Filter = this.Filter
            };
            watcher.Created += (sender, args) =>
            {
                OnCreatedFile(new FileInformation
                {
                    ChangeType = args.ChangeType,
                    FullPath = args.FullPath
                });
            };
            watcher.EnableRaisingEvents = true;
            while (func.Invoke());
        }

        public event EventHandler<FileInformation> CreatedFile;

        protected virtual void OnCreatedFile(FileInformation e)
        {
            if (CreatedFile != null)
                CreatedFile(this, e);
        }
    }
}
