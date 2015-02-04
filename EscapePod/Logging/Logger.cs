using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace EscapePod.Logging
{
    public class Logger : ILogger
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Error(string logText)
        {
            Log.Error(logText);
        }

        public void Error(string logText, params object[] formatArgs)
        {
            Error(String.Format(logText, formatArgs));
        }
    }
}
