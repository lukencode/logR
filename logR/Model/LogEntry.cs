using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logR.Model
{
    internal class LogEntry
    {
        public string Message { get; set; }
        public string Class { get; set; }
        public DateTime Date { get; set; }
        public Level Level { get; set; }

        public string DateDisplay
        {
            get { return Date.ToString(); }
        }

        public string LevelDisplay
        {
            get { return Level.ToString().ToLower(); }
        }
    }
}
