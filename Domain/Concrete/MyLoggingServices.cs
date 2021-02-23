using TicketManager.Domain.Entities;
using System.Data.Entity;

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
                Log temp = new Log() { LogMessage = "This is a test", LogType = "Test", Priority = 1 };
                test.Logs.Add(temp);
                test.SaveChanges();
            }
        }

    }
}
