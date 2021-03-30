using System;

namespace TicketManager.Domain.Entities
{
    public class Log
    {
        public int LogID { get; set; }
        //TODO Make Enum?
        public string LogType { get; set; }
        public string LogMessage { get; set; }
        public int Priority { get; set; }
        public DateTime TimeMade { get; set; }
        public string Object { get; set; }

        public Log()
        {
            this.LogMessage ="No Message.";
            this.LogType = "Default";
            this.Priority = 1;
            this.TimeMade = DateTime.UtcNow;
            this.Object = "NULL";
        }

        public Log(string logType = null, string logMessage = null, int? priority = null,
            DateTime? timeMade = null, string logObject = null)
        {
            this.LogMessage = logType ?? "No Message.";
            this.LogType = logMessage ?? "Default";
            this.Priority = priority ?? 1;
            this.TimeMade = timeMade ?? DateTime.UtcNow;
            this.Object = logObject ?? "NULL";
        }
    }
}
