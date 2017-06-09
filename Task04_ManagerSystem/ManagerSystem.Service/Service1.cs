using System.ServiceProcess;
using System.Threading.Tasks;
using ManagerSystem.Service.Classes;
using System.Threading;

namespace ManagerSystem.Service
{
    public partial class Service1 : ServiceBase
    {
        private Logger _logger;

        public Service1()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            _logger = new Logger();
            var loggerTask = new Task(_logger.Start);
            loggerTask.Start();
        }

        protected override void OnStop()
        {
            _logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
