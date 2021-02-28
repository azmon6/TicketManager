using TicketManager.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace TicketManager.Logging
{
    public class MyLoggingServices : DbContext
    {

        //TODO Make it Async ?
        public DbSet<Log> Logs { get; set; }

        public MyLoggingServices() : base("name=MyLoggingServices")
        {
            Database.SetInitializer<MyLoggingServices>(new DropCreateDatabaseIfModelChanges<MyLoggingServices>());
        }

        public static void TestLog()
        {
            using (var test = new MyLoggingServices())
            {
                Log temp = new Log();
                test.Logs.Add(temp);
                test.SaveChanges();
            }
        }

        public static void AddLog(string logType = null, string logMessage = null , int? priority = null, 
            string timeMade = null, string logObject = null)
        {
            using (var test = new MyLoggingServices())
            {
                Log temp = new Log(logType,logMessage,priority,timeMade,logObject);
                test.Logs.Add(temp);
                test.SaveChanges();
            }
        }

        public static List<Log> GetLogs(int startFrom = 0, int howMany = 1)
        {
            using (var test = new MyLoggingServices())
            {
                return test.Logs.OrderBy(p => p.TimeMade).Skip(startFrom).Take(howMany).ToList();
            }
        }

        public static List<Log> GetAllLogs()
        {
            using (var test = new MyLoggingServices())
            {
                return test.Logs.OrderBy(p => p.TimeMade).ToList();
            }
        }

    }
}
