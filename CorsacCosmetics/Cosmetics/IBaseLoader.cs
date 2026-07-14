using System;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CorsacCosmetics.Cosmetics;

public interface IBaseLoader
{
    public void InstallCosmetics(ReferenceData refData);

    public void LoadCosmetics(string directory);

    public bool LocateCosmetic(string id, string type, out Type il2CPPType);

    public bool ProvideCosmetic(ProvideHandle handle, string id, string type);
}