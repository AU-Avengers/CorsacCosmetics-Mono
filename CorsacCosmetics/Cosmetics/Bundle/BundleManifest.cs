using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace CorsacCosmetics.Cosmetics.Bundle;

public struct BundleManifest()
{
    public const uint CurrentVersion = 1;

    [JsonIgnore]
    public bool IsValid => Version is > 0 and <= CurrentVersion;
    public uint Version { get; set; } = 0;
    [Preserve]
    public HatManifest[] Hats { get; set; } = [];
    [Preserve]
    public VisorManifest[] Visors { get; set; } = [];
    [Preserve]
    public NameplateManifest[] Nameplates { get; set; } = [];

    public override string ToString()
    {
        return $"BundleManifest (Version {Version}, Hats: {Hats.Length}, Visors: {Visors.Length}, Nameplates: {Nameplates.Length})";
    }
}

public struct HatManifest()
{
    public string Name { get; set; } = "Custom Hat";
    public bool MatchPlayerColor { get; set; }
    public bool BlocksVisors { get; set; }
    public bool InFront { get; set; } = true;
    public bool NoBounce { get; set; } = true;

    [Preserve]
    public SpriteData PreviewSprite { get; set; } = new();
    [Preserve]
    public SpriteData MainSprite { get; set; } = new();
    [Preserve]
    public SpriteData BackSprite { get; set; } = new();
    [Preserve]
    public SpriteData ClimbSprite { get; set; } = new();
    [Preserve]
    public SpriteData FloorSprite { get; set; } = new();
    [Preserve]
    public SpriteData LeftMainSprite { get; set; } = new();
    [Preserve]
    public SpriteData LeftBackSprite { get; set; } = new();
    [Preserve]
    public SpriteData LeftClimbSprite { get; set; } = new();
    [Preserve]
    public SpriteData LeftFloorSprite { get; set; } = new();
}

public struct VisorManifest()
{
    public string Name { get; set; } = "Custom Visor";
    public bool MatchPlayerColor { get; set; } = false;
    public bool BehindHats { get; set; } = false;

    [Preserve]
    public SpriteData PreviewSprite { get; set; } = new();
    [Preserve]
    public SpriteData IdleSprite { get; set; } = new();
    [Preserve]
    public SpriteData LeftIdleSprite { get; set; } = new();
    [Preserve]
    public SpriteData FloorSprite { get; set; } = new();
    [Preserve]
    public SpriteData ClimbSprite { get; set; } = new();
}

public struct NameplateManifest()
{
    public string Name { get; set; } = "Custom Nameplate";

    [Preserve]
    public SpriteData PreviewSprite { get; set; } = new();
    [Preserve]
    public SpriteData NameplateSprite { get; set; } = new();
}

public struct SpriteData()
{
    public uint Size { get; set; } = 0;
    public uint Offset { get; set; } = 0;

    [JsonIgnore]
    public bool HasData => Size > 0;
}
