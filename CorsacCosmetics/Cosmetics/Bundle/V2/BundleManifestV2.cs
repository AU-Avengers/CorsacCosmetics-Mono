using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace CorsacCosmetics.Cosmetics.Bundle.V2;

public struct BundleManifestV2()
{
    public const uint CurrentVersion = 2;

    [JsonIgnore]
    public bool IsValid => Version is > 0 and <= CurrentVersion;

    public uint Version { get; set; } = 0;

    public GroupManifest[] Groups { get; set; } = [];

    public override string ToString()
    {
        return $"BundleManifest (Version {Version}, Groups: {Groups.Length})";
    }
}

public struct GroupManifest()
{
    public string Name { get; set; } = "Custom Cosmetics";
    [Preserve]
    public HatManifest[] Hats { get; set; } = [];
    [Preserve]
    public VisorManifest[] Visors { get; set; } = [];
    [Preserve]
    public NameplateManifest[] Nameplates { get; set; } = [];
}

