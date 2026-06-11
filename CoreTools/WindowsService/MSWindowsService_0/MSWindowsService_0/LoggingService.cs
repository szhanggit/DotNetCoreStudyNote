using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;

namespace MSWindowsService_0
{
    public class LoggingService : ServiceBase
    {
        private const string _logFileLocation = @"D:\E\CoreTools\WindowsService\MSWindowsService_0\log\servicelog.txt";
        public void Log(string logMessage)
        {
            if (File.Exists(_logFileLocation))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logFileLocation));
                File.AppendAllText(_logFileLocation, DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
            }
        }
        public LoggingService()
        {
            if (!EventLog.SourceExists("TestMSWindowsService"))
            {
                // An event log source should not be created and immediately used.
                // There is a latency time to enable the source, it should be created
                // prior to executing the application that uses the source.
                // Execute this sample a second time to use the new source.
                EventLog.CreateEventSource("TestMSWindowsService", "MyMSService");
            }
        }
        public void OnStartPublic(string[] args)
        {
            Log("Starting");
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                EventLog.WriteEntry("TestMSWindowsService", "MS Test Windows Service Start", EventLogEntryType.Information);
                Log("Starting");
                base.OnStart(args);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
                EventLog.WriteEntry(ex.StackTrace, EventLogEntryType.Error);
            }
        }
        protected override void OnStop()
        {
            Log("Stopping");
            base.OnStop();
        }
        protected override void OnPause()
        {
            Log("Pausing");
            base.OnPause();
        }
    }
}
