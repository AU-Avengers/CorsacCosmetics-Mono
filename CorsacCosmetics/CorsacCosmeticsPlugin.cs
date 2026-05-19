global using static CorsacCosmetics.Tools.Logger;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using CorsacCosmetics.Components;
using CorsacCosmetics.Cosmetics;
using CorsacCosmetics.Tools;
using CorsacCosmetics.Unity;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CorsacCosmetics;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorCompat.ReactorID, BepInDependency.DependencyFlags.SoftDependency)]
public partial class CorsacCosmeticsPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);

    public static CorsacCosmeticsPlugin Instance { get; private set; } = null!;

    public CorsacCosmeticsPlugin()
    {
        Instance = this;
    }

    public override void Load()
    {
        Message("Loading Corsac Cosmetics Plugin...");
        
        Assets.Initialize();

        IL2CPPChainloader.Instance.Finished +=
            ReactorCompat
                .RegisterCredits;

        ClassInjector.RegisterTypeInIl2Cpp<InventoryTabPaginationBehaviour>();

        ClassInjector.RegisterTypeInIl2Cpp<HatLocator>(new RegisterTypeOptions
        {
            Interfaces = new Il2CppInterfaceCollection([typeof(IResourceLocator)])
        });

        ClassInjector.RegisterTypeInIl2Cpp<HatProvider>(new RegisterTypeOptions
        {
            Interfaces = new Il2CppInterfaceCollection([typeof(IResourceProvider)])
        });

        Info("Initializing HatProvider...");
        HatProvider.Initialize();
        Info("HatProvider initialized!");
        
        Info("Initializing HatLocator...");
        HatLocator.Initialize();
        Info("HatLocator initialized!");

        Info("Loading Harmony patches...");
        Harmony.PatchAll();
        Info("Harmony patches loaded!");

        Info("Creating necessary directories...");
        CosmeticPaths.EnsureDirectoriesExist();
        Info("Necessary directories created!");
        
        Message("Loaded Corsac Cosmetics Plugin!");
    }
}