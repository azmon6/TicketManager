using TicketManager.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

//For Testing Slow responces for Debug
using System.Threading;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure Column
            modelBuilder.Entity<Log>()
                        .Property(p => p.TimeMade)
                        .HasColumnName("Time Created")
                        .HasColumnType("datetime2");
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
            DateTime? timeMade = null, string logObject = null)
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
            //TODO DO NOT FORGET TO REMOVE
            Thread.Sleep(2000);
            using (var test = new MyLoggingServices())
            {
                return test.Logs.OrderByDescending(p => p.TimeMade.Year).ThenByDescending(p => p.TimeMade.Month).
                    ThenByDescending(p => p.TimeMade.Day).ThenByDescending(p => p.TimeMade.Hour).ThenByDescending(p => p.TimeMade.Minute).
                    ThenByDescending(p => p.TimeMade.Second).Skip(startFrom).Take(howMany).ToList();
            }
        }

        public static List<Log> GetAllLogs()
        {
            using (var test = new MyLoggingServices())
            {
                return test.Logs.OrderByDescending(p => p.TimeMade.Year).ThenByDescending(p => p.TimeMade.Month).
                    ThenByDescending(p => p.TimeMade.Day).ThenByDescending(p => p.TimeMade.Hour).ThenByDescending(p => p.TimeMade.Minute).
                    ThenByDescending(p => p.TimeMade.Second).ToList();
            }
        }

    }
}
