using log4net.Layout;
using System;

namespace Riesgos.Simefin.WindowsService.PortfolioLoad
{

    /// <summary>
    /// Clase que formatea el encabezado y pié del inicio y terminación del servicio.
    /// </summary>
    public class TimestampedPatternLayout : PatternLayout
    {
        private readonly string _serviceName = "Portfolio Excel Load";
        private readonly string _separator = "===================================================================";

        public override string Header
        {
            get
            {
                string result = "\r\n{0}\r\n {1} Service Started ({2})\r\n{0}\r\n";
                result = string.Format(result, _separator, _serviceName, DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"));
                return result;
            }
            set { }
        }

        public override string Footer
        {
            get
            {
                string result = "{0}\r\n {1} Service Ended   ({2})\r\n{0}\r\n";
                result = string.Format(result, _separator, _serviceName, DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"));
                return result;
            }
            set { }
        }

    }

}
