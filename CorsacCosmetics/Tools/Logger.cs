namespace CorsacCosmetics.Tools;

public static class Logger
{
    public static void Debug(string message)
    {
        CorsacCosmeticsPlugin.Instance.Log.LogDebug(message);
    }

    public static void Message(string message)
    {
        CorsacCosmeticsPlugin.Instance.Log.LogMessage(message);
    }

    public static void Info(string message)
    {
        if (CorsacCosmeticsPlugin.LogCosmetics.Value)
        {
            CorsacCosmeticsPlugin.Instance.Log.LogInfo(message);
        }
    }

    public static void Warning(string message)
    {
        CorsacCosmeticsPlugin.Instance.Log.LogWarning(message);
    }

    public static void Error(string message)
    {
        CorsacCosmeticsPlugin.Instance.Log.LogError(message);
    }
}