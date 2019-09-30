using System;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Ch01
{
    class _5BackgroundWorker
    {

        /// <summary>
        /// Main method that creates background worker
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork += SimulateServiceCall;
            backgroundWorker.ProgressChanged += ProgressChanged;
            backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();

            Console.WriteLine("To Cancel Worker Thread Press C.");

            while (backgroundWorker.IsBusy)
            {
                if (Console.ReadKey(true).KeyChar == 'C')
                {
                    backgroundWorker.CancelAsync();
                }
            }
        }

        /// <summary>
        /// This method executes when the background worker finishes execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine(e.Error.Message);
            }
            else
                Console.WriteLine("Result from service call is " + e.Result);
        }
        /// <summary>
        /// This method is called when background worker want to report progress to caller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}% completed",e.ProgressPercentage);
        }

        /// <summary>
        /// Service call we are tryign to simulate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SimulateServiceCall(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            StringBuilder data = new StringBuilder();
            //Simulate a streaming service call which gets data and store it to return back to caller
            for (int i = 1; i <= 100; i++)
            {
                //worker.CancellationPending will be true if user press C
                if (!worker.CancellationPending)
                {
                    data.Append(i);
                    worker.ReportProgress(i);
                    Thread.Sleep(100);
                    //Try to uncomment and throw error
                    //throw new Exception("Some Error has occured");
                }
               else
                {
                    //Cancels the execution of worker
                    worker.CancelAsync();
                }
            }
            e.Result = data;
        }
    }
}
