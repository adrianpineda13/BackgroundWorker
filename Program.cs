using BgWorker.Classes;
using System.ComponentModel;

namespace Main
{
    internal class Program
    {
        static BackgroundWorker worker;
        static void Main(string[] args)
        {
            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            worker.DoWork += Work;
            worker.ProgressChanged += Progress;
            worker.RunWorkerCompleted += Finalized;

            worker.RunWorkerAsync();

            Console.WriteLine("Press any key to cancel.");
            Console.ReadLine();
            if(worker.IsBusy)
            {
                worker.CancelAsync();
                Console.ReadLine();
            }
        }

        static void Work(object sender, DoWorkEventArgs e)
        {
            List<PDF> files = GetFilesToBeSent();
            int progress = 0;


            foreach (PDF pdf in files)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                pdf.Send("smtp"); // GET from Appsettings
                pdf.Log();
                pdf.Move();

                Thread.Sleep(100);

                worker.ReportProgress(progress);
            }

            e.Result = files.Count;
        }

        static void Progress(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 5 == 0)
            {
                Console.WriteLine("{0} % completed", e.ProgressPercentage);
            }
        }

        static void Finalized(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                Console.WriteLine("Work cancelled.");
            }
            else if(e.Error != null)
            {
                Console.WriteLine($"Error: {e.Error}");
            }
            else 
            {
                Console.WriteLine("Work completed.");
                Console.WriteLine(e.Result);
                Console.WriteLine("Press any key to continue.");
            }
        }

        private static List<PDF> GetFilesToBeSent()
        {
            // Read Files from Folder
            // Flag to know if the file is from the current scheduled proccess or if was added after the task init
            // Attemps from AttempsLog
            // Pdf.Attemps = Attemps where name = name;
            //return lst where pdf.TryAgain == true || attemps == 0
            return new List<PDF>
            {
                new PDF { Name = "Test" }
            };
        }
    }
}
 