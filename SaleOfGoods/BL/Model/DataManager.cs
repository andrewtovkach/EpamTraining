using System;
using System.Collections.Generic;
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
            var listTasks = new List<Task>();
            Watcher.CreatedFile += (sender, info) =>
            {
                var t = CreateTask(info);
                listTasks.Add(t);
            };
            Task.WaitAll(listTasks.ToArray());
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

        private Task CreateTask(FileInformation info)
        {
            var task = new Task(() =>
            {
                if (AddInformationToTheDb(info.FullPath))
                {
                    Console.WriteLine("File " + info.FullPath + " is recorded!");
                }
                else
                {
                    Console.WriteLine("In the process of writing a file error occurred!");
                }
            });
            task.Start();
            return task;
        }

        public bool AddInformationToTheDb(string fileName)
        {
            using (var fileInfoRepository = new FileInfoRepository())
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
                    var unverifiedFiles = new UnverifiedFilesRepository { new UnverifiedFile(fileName) };
                    unverifiedFiles.SaveChanges();
                    return false;
                }
            }
        }
    }
}
