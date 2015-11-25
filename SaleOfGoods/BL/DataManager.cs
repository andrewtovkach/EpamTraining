using System;
using System.Configuration;
using DAL.Repositories;

namespace BL
{
    public class DataManager
    {
        public void OnStart()
        {
            var watcher = new Watcher(ConfigurationManager.AppSettings["FolderPath"],
                ConfigurationManager.AppSettings["FileExtension"]);
            watcher.CreatedFile += (sender, info) => { AddInformationToTheDb(info); };
            watcher.Run(() =>
            {
                var result = Console.Read() != 'q';
                return result;
            });
        }

        private static void AddInformationToTheDb(FileInformation information)
        {
            try
            {
                var readWriter = new ReadWriter(information.FullPath);
                var records = readWriter.Read();
                using (var fileInfoRepository = new FileInfoRepository())
                {
                    foreach (var record in records)
                    {
                        fileInfoRepository.Add(Parser.ParseFile(information.FullPath, record));
                        fileInfoRepository.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
