using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repositories;

namespace BL
{
    public class DataManager
    {
        public void OnStart()
        {
            ProcessingUnverifiedFiles();
            string filePath = ConfigurationManager.AppSettings["FolderPath"];
            string fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var watcher = new Watcher(filePath, fileExtension);
            watcher.CreatedFile += (sender, info) =>
            {
                Task task = new Task(() => AddInformationToTheDb(info.FullPath));
                task.Start();
                task.ContinueWith((Task t) => { Console.WriteLine(info.FullPath + " обработан!"); });
            };
            Task.WaitAll();
            watcher.Run(() =>
            {
                var result = Console.Read() != 'q';
                return result;
            });
        }

        private static void ProcessingUnverifiedFiles()
        {
            using (var unverifiedFilesRepository = new UnverifiedFilesRepository())
            {
                if (unverifiedFilesRepository.Count() != 0)
                {
                    foreach (var item in unverifiedFilesRepository)
                    {
                        AddInformationToTheDb(item.FileName);
                    }
                }
            }
        }

        static readonly object Locker = new object();

        private static void AddInformationToTheDb(string fileName)
        {
            try
            {
                var unverifiedFiles = new UnverifiedFilesRepository { new UnverifiedFile(fileName) };
                unverifiedFiles.SaveChanges();
                var records = new ReadWriter(fileName).Read();
                using (var fileInfoRepository = new FileInfoRepository())
                {
                    foreach (var record in records)
                    {
                        lock (Locker)
                        {
                            fileInfoRepository.Add(Parser.ParseFile(fileName, record));
                            fileInfoRepository.SaveChanges();
                        }
                    }
                }
                var unverifiedFile = unverifiedFiles.Last();
                unverifiedFiles.Remove(unverifiedFile);
                unverifiedFiles.SaveChanges();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
