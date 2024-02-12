using BgWorker.Factories;
using BgWorker.Helpers;
using BgWorker.Interfaces;

namespace BgWorker.Classes
{
    public class PDF
    {
        const int AllowedAttemps = 5; // GET from AppSettings

        public ILogger? _logger;
        public ISender? _sender;

        public required string Name { get; set; }
        public int Attempts { get; set; }
        private bool Sent { get; set; }

        private bool _retry;

        public bool TryAgain
        {
            get => _retry;
            set => _retry = Attempts > 0 && Attempts <= AllowedAttemps;
        }

        #region Send
        public void Send(string type)
        {
            _sender = SenderFactory.CreateSender(type);
            Sent = _sender.Send(this);
        }
        #endregion Send

        #region Log
        public void Log()
        {
            LogBasedOnPdfStatus();
        }

        private bool LogBasedOnPdfStatus()
        {
            string message = string.Empty;
            
            if (!Sent || TryAgain)
            {
                if (!Sent)
                {
                    return LogException(message);
                }

                if (TryAgain)
                {
                    return LogIfTryAgain(message);
                }
            }

            return LogByType(message, "db");
        }

        private bool LogException(string message)
        {
            return LogByType(message, "exception");
        }

        private bool LogIfTryAgain(string message)
        {
            if (LogByType(message, "attempt"))
            {
                Attempts++;
                return true;
            }
            return false;
        }

        private bool LogByType(string message, string type)
        {
            _logger = LoggerFactory.CreateLogger(type);
            return _logger.Log(message);
        }
        #endregion Log

        #region Move
        public void Move()
        {
            if (Sent)
            {
                // Move to another folder
                if (!FileHelper.Move("", "")) // GET from AppSettings
                {
                    LogException("file cannot be moved");
                    LogIfTryAgain("");
                }
            }
        }
        #endregion Move
    }
}
