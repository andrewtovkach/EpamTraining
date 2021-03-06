﻿using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using BL.Model;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private readonly Thread _workerThread;

        public Service1()
        {
            InitializeComponent();
            var filePath = ConfigurationManager.AppSettings["FolderPath"];
            var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var dataManager = new DataManager(new Watcher(filePath, fileExtension));
            _workerThread = new Thread(dataManager.OnStart);
        }

        public void Start()
        {
            _workerThread.Start();
        }

        public new void Stop()
        {
            _workerThread.Abort();
        }

        protected override void OnStart(string[] args)
        {
            AddLog("start");
            Start();   
        }

        protected override void OnStop()
        {
            AddLog("stop");
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
            catch(Exception exception)
            {
                AddLog(exception.Message);
            }
        }
    }
}
