using System;
using System.Linq;
using System.Reflection;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Version = SemanticVersioning.Version;

namespace CorsacCosmetics.Tools;

public static class ReactorCompat
{
    public const string ReactorID = "gg.reactor.api";
    private static Func<int, bool> ShowCredits { get; } = location => location == 0;
    public static Version ReactorVersion { get; private set; }
    public static bool ReactorLoaded { get; private set; }
    public static BasePlugin ReactorPlugin { get; private set; }
    public static Assembly ReactorAssembly { get; private set; }
    public static Type[] ReactorTypes { get; private set; }

    public static void Initialize()
    {
        if (!IL2CPPChainloader.Instance.Plugins.TryGetValue(ReactorID, out var value))
        {
            return;
        }

        ReactorPlugin = (value.Instance as BasePlugin)!;
        ReactorAssembly = ReactorPlugin.GetType().Assembly;
        ReactorVersion = value.Metadata.Version;
        ReactorTypes = AccessTools.GetTypesFromAssembly(ReactorAssembly);

        var reactorCreds = ReactorTypes.First(t => t.Name == "ReactorCredits");

        var registerMethod = AccessTools
            .GetDeclaredMethods(reactorCreds)
            .Single(m => m.Name == "Register" && m.IsGenericMethodDefinition)
            ?.MakeGenericMethod(typeof(CorsacCosmeticsPlugin));
        var showCreditsType = registerMethod!.GetParameters().First().ParameterType;
        var showCreditsDelegate = Delegate.CreateDelegate(showCreditsType, ShowCredits.Target, ShowCredits.Method);
        registerMethod.Invoke(null, [showCreditsDelegate]);

        ReactorLoaded = true;
        Message("Reactor was detected and Corsac was registered successfully.");
    }

    public static void RegisterCredits()
    {
        /*try
        {
            var creditsType = AccessTools.TypeByName("Reactor.Utilities.ReactorCredits");
            var registerMethod = AccessTools
                .GetDeclaredMethods(creditsType)
                .Single(m => m.Name == "Register" && m.IsGenericMethodDefinition)
                ?.MakeGenericMethod(typeof(CorsacCosmeticsPlugin));

            if (registerMethod == null)
            {
                Error("Could not register credits with Reactor! The method was not found.");
                return;
            }

            var showCreditsType = registerMethod.GetParameters().First().ParameterType;
            var showCreditsDelegate = Delegate.CreateDelegate(showCreditsType, ShowCredits.Target, ShowCredits.Method);

            registerMethod.Invoke(null, [showCreditsDelegate]);
            Info("Registered credits with Reactor!");
        }
        catch (Exception e)
        {
            Error($"Could not register credits with Reactor! An exception was thrown:\n{e}");
        }*/
    }
}