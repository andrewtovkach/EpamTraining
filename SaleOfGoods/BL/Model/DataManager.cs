using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace BL.Model
{
    public class DataManager : IDataManager
    {
        public void OnStart()
        {
            var filePath = ConfigurationManager.AppSettings["FolderPath"];
            var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var watcher = new Watcher(filePath, fileExtension);
            watcher.CreatedFile += (sender, info) => { CreateTask(info); };
            Task.WaitAll();
            watcher.Run(() =>
            {
                var result = Console.Read() != 'q';
                return result;
            });
        }

        public void CreateTask(FileInformation info)
        {
            var task = new Task(() => VerificationTable(info.FullPath));
            task.Start();
            task.ContinueWith((Task t) =>
            {
                if (t.Exception == null)
                {
                    Console.WriteLine("File " + info.FullPath + " is recorded!");
                    return;
                }
                foreach (var exception in t.Exception.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            });
        }

        static readonly object Locker = new object();

        private void VerificationTable(string fileName)
        {
            var writtenRecords = new List<FileInfo>();
            try
            {
                writtenRecords = AddInformationToTheDb(fileName);
            }
            catch
            {
                var unverifiedFiles = new UnverifiedFilesRepository { new UnverifiedFile(fileName) };
                unverifiedFiles.SaveChanges();
                var fileInfoRepository = new FileInfoRepository();
                foreach (var record in writtenRecords)
                {
                    fileInfoRepository.Remove(record);
                }
                fileInfoRepository.SaveChanges();
                throw new Exception("Error writing file to the database");
            }
        }

        public List<FileInfo> AddInformationToTheDb(string fileName)
        {
            var writtenRecords = new List<FileInfo>();
            var records = new ReadWriter(fileName).Read();
            using (var fileInfoRepository = new FileInfoRepository())
            {
                foreach (var record in records)
                {
                    lock (Locker)
                    {
                        var fileInfo = new Parser().ParseFile(fileName, record);
                        fileInfoRepository.Add(fileInfo);
                        writtenRecords.Add(fileInfo);
                        fileInfoRepository.SaveChanges();
                    }
                }
            }
            return writtenRecords;
        }
    }
}
