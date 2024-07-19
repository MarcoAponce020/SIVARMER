using System.ServiceProcess;

namespace Riesgos.Simefin.WindowsService.ExcelLoad
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
                new ServiceExcelLoad()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
