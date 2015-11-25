﻿using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using BL;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private readonly Thread _workerThread;

        public Service1()
        {
            InitializeComponent();
            DataManager dataManager = new DataManager();
            _workerThread = new Thread(dataManager.OnStart);
            _workerThread.SetApartmentState(ApartmentState.STA);
        }

        public void Start()
        {
            AddLog("start");
            _workerThread.Start();
        }

        public new void Stop()
        {
            AddLog("stop");
            _workerThread.Abort();
        }

        protected override void OnStart(string[] args)
        {
            Start();   
        }

        protected override void OnStop()
        {
            Stop();
        }

        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("Service1"))
                {
                    EventLog.CreateEventSource("Service1", "Service1");
                }
                eventLog1.Source = "Service1";
                eventLog1.WriteEntry(log);
            }
            catch { }
        }
    }
}
