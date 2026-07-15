using System.Text.Json.Serialization;

namespace CorsacCosmetics.Cosmetics.Bundle;

public struct BundleManifest()
{
    public const uint CurrentVersion = 1;

    public uint Version { get; set; } = 0;
    public HatManifest[] Hats { get; set; } = [];
    public VisorManifest[] Visors { get; set; } = [];
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

    public SpriteData PreviewSprite { get; set; } = new();
    public SpriteData MainSprite { get; set; } = new();
    public SpriteData BackSprite { get; set; } = new();
    public SpriteData ClimbSprite { get; set; } = new();
    public SpriteData FloorSprite { get; set; } = new();
    public SpriteData LeftMainSprite { get; set; } = new();
    public SpriteData LeftBackSprite { get; set; } = new();
    public SpriteData LeftClimbSprite { get; set; } = new();
    public SpriteData LeftFloorSprite { get; set; } = new();
}

public struct VisorManifest()
{
    public string Name { get; set; } = "Custom Visor";
    public bool MatchPlayerColor { get; set; } = false;
    public bool BehindHats { get; set; } = false;

    public SpriteData PreviewSprite { get; set; } = new();
    public SpriteData IdleSprite { get; set; } = new();
    public SpriteData LeftIdleSprite { get; set; } = new();
    public SpriteData FloorSprite { get; set; } = new();
    public SpriteData ClimbSprite { get; set; } = new();
}

public struct NameplateManifest()
{
    public string Name { get; set; } = "Custom Nameplate";

    public SpriteData PreviewSprite { get; set; } = new();
    public SpriteData NameplateSprite { get; set; } = new();
}

public struct SpriteData()
{
    public uint Size { get; set; } = 0;
    public uint Offset { get; set; } = 0;
}
