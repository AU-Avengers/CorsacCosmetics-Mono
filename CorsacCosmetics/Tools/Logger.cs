using BepInEx.Logging;

namespace CorsacCosmetics.Tools;

public static class Logger
{
    internal static ManualLogSource LogSource { get; } = BepInEx.Logging.Logger.CreateLogSource("CorsacCosmetics");
    public static void Debug(string message)
    {
        LogSource.LogDebug(message);
    }

    public static void Message(string message)
    {
        LogSource.LogMessage(message);
    }

    public static void Info(string message)
    {
        LogSource.LogInfo(message);
    }

    public static void Warning(string message)
    {
        LogSource.LogWarning(message);
    }

    public static void Error(string message)
    {
        LogSource.LogError(message);
    }
}