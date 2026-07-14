using System;
using System.Linq;
using BepInEx.Bootstrap;
using HarmonyLib;

namespace CorsacCosmetics.Tools;

public static class ReactorCompat
{
    public const string ReactorID = "gg.reactor.api";
    private static Func<int, bool> ShowCredits { get; } = location => location == 0;

    public static void RegisterCredits()
    {
        if (!Chainloader.PluginInfos.TryGetValue(ReactorID, out var value))
        {
            return;
        }

        var reactorPlugin = value.Instance!;
        var reactorAssembly = reactorPlugin.GetType().Assembly;
        var reactorTypes = AccessTools.GetTypesFromAssembly(reactorAssembly);

        var reactorCreds = reactorTypes.First(t => t.Name == "ReactorCredits");

        var registerMethod = AccessTools
            .GetDeclaredMethods(reactorCreds)
            .Single(m => m.Name == "Register" && m.IsGenericMethodDefinition)
            ?.MakeGenericMethod(typeof(CorsacCosmeticsPlugin));
        var showCreditsType = registerMethod!.GetParameters().First().ParameterType;
        var showCreditsDelegate = Delegate.CreateDelegate(showCreditsType, ShowCredits.Target, ShowCredits.Method);
        registerMethod.Invoke(null, [showCreditsDelegate]);
        Message("Reactor was detected and Corsac was registered successfully.");
    }
}