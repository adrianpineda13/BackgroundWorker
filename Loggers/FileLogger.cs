using BgWorker.Interfaces;

namespace BgWorker.Loggers
{
    public abstract class FileLogger : ILogger
    {
        protected string FilePath;
        public abstract bool Log(string message);
    }
}
