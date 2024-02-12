using BgWorker.Classes;
using BgWorker.Interfaces;

namespace BgWorker.Senders
{
    public class SmtpSender : ISender
    {
        public bool Send(PDF file)
        {
            Console.WriteLine("Email sent.");
            return true;
        }
    }
}
