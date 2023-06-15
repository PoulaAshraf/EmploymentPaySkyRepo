namespace EmploymentApi.Helper
{
    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(LogTarget target, string message)
        {
            switch (target)
            {
                case LogTarget.File:
                    logger = new FileLogger();
                    break;
                case LogTarget.Database:
                    logger = new DbLogger();
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    break;
                default:
                    return;
            }
            logger.Log(message);
        }
    }
    public enum LogTarget
    {
        File, Database, EventLog
    }
}
