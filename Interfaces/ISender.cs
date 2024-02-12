using BgWorker.Classes;

namespace BgWorker.Interfaces
{
    public interface ISender
    {
        public bool Send(PDF file);
    }
}
