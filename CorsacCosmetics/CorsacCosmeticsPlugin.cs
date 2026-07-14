global using static CorsacCosmetics.Tools.Logger;
using BepInEx;
using CorsacCosmetics.Cosmetics;
using CorsacCosmetics.Tools;
using CorsacCosmetics.Unity;
using HarmonyLib;

namespace CorsacCosmetics;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorCompat.ReactorID, BepInDependency.DependencyFlags.SoftDependency)]
public partial class CorsacCosmeticsPlugin : BaseUnityPlugin
{
    public Harmony Harmony { get; } = new(Id);

    public static CorsacCosmeticsPlugin Instance { get; private set; } = null!;

    private void Awake()
    {
        Instance = this;
        Message("Loading Corsac Cosmetics Plugin...");
        
        Assets.Initialize();

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

    private void Start()
    {
        ReactorCompat.RegisterCredits();
    }
}