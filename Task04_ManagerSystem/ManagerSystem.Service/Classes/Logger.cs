using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ManagerSystem.Service.Classes
{
    public class Logger
    {
        private readonly FileSystemWatcher _watcher;
        private readonly object _obj = new object();
        private bool _enabled = true;
        private CsvDbRecorder _dbRecorder;

        public Logger()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["ServerPath"]);
            _dbRecorder = new CsvDbRecorder("DefaultConnection");

            _watcher.Created += WatcherOnCreated;
            _watcher.Changed += WatcherOnChanged;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
            while (_enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _enabled = false;
        }

        private void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("created ", fileSystemEventArgs.FullPath);
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            RecordEntry("changed ", fileSystemEventArgs.FullPath);

            var task = new Task(() =>
            {
                _dbRecorder.WriteDataToDataBaseFromFile(fileSystemEventArgs.FullPath);
                //Delete file after recording to database, write to log about success
                File.Delete(fileSystemEventArgs.FullPath);
                RecordEntry("successfully proceeded ", fileSystemEventArgs.FullPath);
            });
            task.Start();
            task.Wait();
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (_obj)
            {
                using (var writer = new StreamWriter(ConfigurationManager.AppSettings["LogPath"], true))
                {
                    writer.WriteLine($"{filePath} was {fileEvent}");
                    writer.Flush();
                }
            }
        }
    }
}
