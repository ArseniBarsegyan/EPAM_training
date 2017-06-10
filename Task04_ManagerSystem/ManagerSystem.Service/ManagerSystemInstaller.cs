using System.ComponentModel;
using System.ServiceProcess;

namespace ManagerSystem.Service
{
    [RunInstaller(true)]
    public partial class ManagerSystemInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public ManagerSystemInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ManagerService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
