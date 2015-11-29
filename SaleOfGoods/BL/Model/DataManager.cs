using System;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace BL.Model
{
    public class DataManager : IDataManager
    {
        public IWatcher Watcher { get; private set; }
        
        public DataManager(IWatcher watcher)
        {
            Watcher = watcher;
        }

        public void OnStart()
        {
            ProcessingUnverifiedFiles();
            Watcher.CreatedFile += (sender, info) => { CreateTask(info); };
            Task.WaitAll();
            Watcher.Run(() =>
            {
                var result = Console.Read() != 'q';
                return result;
            });
        }

        private void ProcessingUnverifiedFiles()
        {
            using (var unverifiedFilesRepository = new UnverifiedFilesRepository())
            {
                foreach (var item in unverifiedFilesRepository.Where(item => AddInformationToTheDb(item.FileName)))
                {
                    unverifiedFilesRepository.Remove(item);
                }
            }
        }

        private void CreateTask(FileInformation info)
        {
            var task = new Task(() => AddInformationToTheDb(info.FullPath));
            task.Start();
            task.ContinueWith((Task t) =>
            {
                if (t.Exception == null)
                {
                    Console.WriteLine("File " + info.FullPath + " is recorded!");
                    return;
                }
                Console.WriteLine("In the process of writing a file error occurred: \n");
                foreach (var exception in t.Exception.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            });
        }

        static readonly object Locker = new object();

        public bool AddInformationToTheDb(string fileName)
        {
            using (var fileInfoRepository = new FileInfoRepository())
            {
                lock (Locker)
                {
                    try
                    {
                        var fileInfo = new Parser(new ReadWriter(fileName)).ParseFile();
                        fileInfoRepository.Add(fileInfo);
                        fileInfoRepository.SaveChanges();
                        return true;
                    }
                    catch
                    {
                        var unverifiedFiles = new UnverifiedFilesRepository { new UnverifiedFile(fileName)                         };
                        unverifiedFiles.SaveChanges();
                        return false;
                    }
                }
            }
        }
    }
}
