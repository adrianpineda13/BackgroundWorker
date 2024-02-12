using BgWorker.Interfaces;

namespace BgWorker.Loggers
{
    public class DatabaseLogger : ILogger
    {
        public bool Log(string message)
        {
            Console.WriteLine("Log in the database");
            return true;
        }
    }
}
