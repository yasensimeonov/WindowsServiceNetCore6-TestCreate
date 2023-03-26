using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Pipes;

namespace WindowsServiceCreation
{
    public class FileWriteService : ServiceBase
    {

        public Thread Worker = null;

        public FileWriteService()
        {
            ServiceName = "MyCoreService";
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart start = new ThreadStart(Working);
            Worker = new Thread(start);
            Worker.Start();
        }

        public void Working()
        {
            int nsleep = 1; // Declare interval as 1 minute.

            try
            {
                while (true)
                {
                    string FileName = @"E:\TestService\MyCoreService.txt";
                    using (StreamWriter writer = new StreamWriter(FileName, true))
                    {
                        writer.WriteLine(string.Format("Windows Service called on " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt") + ""));
                        writer.Close();
                    }
                    Thread.Sleep(nsleep * 60 * 1000);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (Worker != null & Worker.IsAlive)
                {
                    string FileName = @"E:\TestService\MyCoreService.txt";
                    using (StreamWriter writer = new StreamWriter(FileName, true))
                    {
                        writer.WriteLine(string.Format("Windows Service stopped on " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt") + ""));
                        writer.Close();
                    }
                    Worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OnDebug()
        {
            OnStart(null);
        }

    }
}
