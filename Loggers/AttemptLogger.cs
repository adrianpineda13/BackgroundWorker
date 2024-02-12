namespace BgWorker.Loggers
{
    public class AttemptLogger : FileLogger
    {
        public AttemptLogger()
        {
            FilePath = "Get Path from appsettings";
        }
        public override bool Log(string message)
        {
            Console.WriteLine("Log an attempt");
            return true;
        }
    }
}
