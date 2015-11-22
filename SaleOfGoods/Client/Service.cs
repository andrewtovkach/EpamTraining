using System.Threading;
using BL;

namespace Client
{
    class Service
    {
        private readonly Thread _workerThread;

        public Service()
        {
            DataManager dataManager = new DataManager();
            _workerThread = new Thread(dataManager.OnStart);
            _workerThread.SetApartmentState(ApartmentState.STA);
        }

        public void OnStart()
        {
            _workerThread.Start();
        }

        public void OnStop()
        {
            _workerThread.Abort();
        }
    }
}
