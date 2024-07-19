using System.ComponentModel;
using System.Configuration.Install;

namespace Riesgos.Simefin.WindowsService.PortfolioLoad
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}
