namespace BgWorker.Loggers
{
    public class ExceptionLogger : FileLogger
    {
        public ExceptionLogger()
        {
            FilePath = "Get Path from appsettings";
        }
        public override bool Log(string message)
        {
            Console.WriteLine("Log an exception");
            return true;
        }
    }
}
