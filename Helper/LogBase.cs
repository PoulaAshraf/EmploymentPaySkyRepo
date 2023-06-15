using System.Diagnostics;

namespace EmploymentApi.Helper
{
    public abstract class LogBase
    {
        protected readonly object _lock = new object();
        public abstract void Log(string message);
    }

    public class FileLogger:LogBase
    {
        string filePath = Environment.CurrentDirectory;
        //string filePath = Directory.GetCurrentDirectory();

        public override void Log(string message)
        {
            lock (_lock) 
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath))
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
            }
        }
    }

    public class DbLogger : LogBase
    {
        string connectionString = @"";
        public override void Log(string message)
        {
            lock (_lock)
            {
                
                //Logic
            }
        }
    }

    public class EventLogger : LogBase
    {
        public override void Log(string message)
        {
            lock (_lock) 
            {
                EventLog eventLog = new EventLog("");
                eventLog.Source = "IDGEventLog";
                eventLog.WriteEntry(message);
            }
        }
    }

}
