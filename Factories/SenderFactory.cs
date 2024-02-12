using BgWorker.Interfaces;
using BgWorker.Senders;

namespace BgWorker.Factories
{
    static class SenderFactory
    {
        public static ISender CreateSender(string type)
        {
            switch (type)
            {
                case "smtp":
                    return new SmtpSender();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
