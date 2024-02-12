using BgWorker.Interfaces;
using BgWorker.Loggers;

namespace BgWorker.Factories
{
    static class LoggerFactory
    {
        public static ILogger CreateLogger(string type)
        {
            switch (type)
            {
                case "exception":
                    return new ExceptionLogger();
                case "attempt":
                    return new AttemptLogger();
                case "db":
                    return new DatabaseLogger();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
