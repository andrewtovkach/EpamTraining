using System.Threading;
using BL;

namespace Client
{
    public class Service
    {
        private readonly Thread _workerThread;

        public Service()
        {
            DataManager dataManager = new DataManager();
            _workerThread = new Thread(dataManager.OnStart);
            _workerThread.SetApartmentState(ApartmentState.STA);
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
