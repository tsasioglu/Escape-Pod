namespace EscapePod.Logging
{
    public interface ILogger
    {
        void Error(string logText);
        void Error(string logText, params object[] formatArgs);
    }
}
