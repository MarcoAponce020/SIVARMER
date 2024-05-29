using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinSerAPIRiesgos
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };

            // If you don’t want the exe to run as console when double-clicked, then use:
            //   if (System.Diagnostics.Debugger.IsAttached)
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Service running, press  to stop.");
                ((Service1)ServicesToRun[0]).Start(null);
                Console.ReadLine();
                ((Service1)ServicesToRun[0]).Stop();
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
