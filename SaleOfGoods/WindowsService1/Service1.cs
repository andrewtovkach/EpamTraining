using System.ServiceProcess;
using System.Threading;
using BL;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private readonly Thread workerThread;

        public Service1()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager();
            workerThread = new Thread(dataManager.OnStart);
            workerThread.SetApartmentState(ApartmentState.STA);
        }

        protected override void OnStart(string[] args)
        {
            workerThread.Start();
        }

        protected override void OnStop()
        {
            workerThread.Abort();
        }
    }
}
