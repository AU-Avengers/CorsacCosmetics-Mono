using CorsacCosmetics.Cosmetics;
using HarmonyLib;

namespace CorsacCosmetics.Patches;

[HarmonyPatch(typeof(ReferenceDataManager), nameof(ReferenceDataManager.Initialize))]
public static class InstallCosmeticsPatch
{
    private static bool _didRun = false;

    public static void Postfix(ReferenceDataManager __instance)
    {
        if (_didRun)
        {
            // only run after the original method has fully completed
            return;
        }

        Info("Loading cosmetics...");
        CosmeticsLoader.Instance.LoadCosmetics();
        Info("Cosmetics loaded");

        Info("Patching HatManager to include custom cosmetics");
        CosmeticsLoader.Instance.InstallCosmetics(__instance.Refdata);
        Info("Loaded custom cosmetics into HatManager");

        // second guard to prevent double execution
        _didRun = true;
    }
}