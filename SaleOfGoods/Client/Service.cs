using System.Configuration;
using System.Threading;
using BL.Model;

namespace Client
{
    public class Service
    {
        private readonly Thread _workerThread;

        public Service()
        {
            var filePath = ConfigurationManager.AppSettings["FolderPath"];
            var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var dataManager = new DataManager(new Watcher(filePath, fileExtension));
            _workerThread = new Thread(dataManager.OnStart);
        }

        public void Start()
        {
            _workerThread.Start();
        }

        public void Stop()
        {
            _workerThread.Abort();
        }
    }
}
