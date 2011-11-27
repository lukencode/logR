using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using logR.Model;
using SignalR.Hubs;
using SignalR.Client.Hubs;

namespace logR
{
    public partial class LogR 
    {
        public static void Init()
        {
            Settings.InitRouting();
        }

        public static void Log(string className, Level level, string message)
        {
            var entry = new LogEntry
            {
                Message = message,
                Class = className,
                Level = level,
                Date = DateTime.Now
            };

            var clients = Hub.GetClients<logRHub>();
            clients.addLog(entry);
        }

        private static readonly LogR instance = new LogR();

        static LogR()
        {
        }
    }
}
