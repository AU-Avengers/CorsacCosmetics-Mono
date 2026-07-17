using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CorsacCosmetics.Tools;
using CorsacCosmetics.Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CorsacCosmetics.Cosmetics.Visors;

public class VisorLoader : IBaseLoader
{
    public Dictionary<string, CustomVisor> CustomVisors { get; } = [];

    public void InstallCosmetics(ReferenceData refData)
    {
        foreach (var (id, customVisor) in CustomVisors)
        {
            try
            {
                refData.visors.Add(customVisor.VisorData);
                Info($"Added visor {id} to HatManager");
            }
            catch (Exception e)
            {
                Error($"Failed to load visor {id} with exception:\n{e.ToString()}");
            }
        }
    }

    public void LoadCosmetics(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Info($"Created visors directory at {directory}");
            return;
        }

        var visorFiles = Directory.GetFiles(directory, "*.png");

        foreach (var visorFile in visorFiles)
        {
            try
            {
                if (LoadVisor(visorFile))
                {
                    Info($"Loaded visor from {visorFile}");
                }
                else
                {
                    Error($"Failed to load visor from {visorFile}");
                }
            }
            catch (Exception e)
            {
                Error($"Exception while loading visor from {visorFile}: {e.Message}");
            }
        }
    }

    public bool LocateCosmetic(string id, string type, out Type il2CPPType)
    {
        il2CPPType = null!;
        if (!CustomVisors.ContainsKey(id))
        {
            return false;
        }

        il2CPPType = type == ReferenceType.VisorViewData ? typeof(VisorViewData) : null!;
        return il2CPPType != null;
    }

    public bool ProvideCosmetic(ProvideHandle handle, string id, string type)
    {
        if (!CustomVisors.TryGetValue(id, out var visor))
        {
            return false;
        }

        switch (type)
        {
            case ReferenceType.Preview:
                Debug($"Found visor preview for {id}");
                handle.Complete(visor.PreviewData, true, null);
                return true;
            case ReferenceType.VisorViewData:
                Debug($"Found visor view data for {id}");
                handle.Complete(visor.VisorViewData, true, null);
                return true;
            default:
                Error("Unknown visor type");
                return false;
        }
    }

    private bool LoadVisor(string filePath)
    {
        var name = Path.GetFileNameWithoutExtension(filePath);
        var metadataFile = Path.ChangeExtension(filePath, ".json");
        var metadata = new VisorMetadata
        {
            Name = name
        };
        try
        {
            if (File.Exists(metadataFile))
            {
                var metadataJson = File.ReadAllText(metadataFile);
                metadata = JsonConvert.DeserializeObject<VisorMetadata>(metadataJson);
            }
            else
            {
                Warning($"No metadata file found for visor {name}, using defaults.");
            }
        }
        catch (Exception e)
        {
            Error($"Failed to load metadata for visor {name}: {e.Message}");
            return false;
        }

        var fullId = Names.Normalize(name, "nameplate");

        var visorSprite = SpriteTools.LoadSpriteFromFile(filePath);
        if (visorSprite == null)
        {
            Error($"Error loading visor sprite {name}");
            return false;
        }
        
        visorSprite.DontUnload().DontDestroy();
        var visorViewData = ScriptableObject.CreateInstance<VisorViewData>();
        visorViewData.name = metadata.Name;
        visorViewData.MatchPlayerColor = metadata.MatchPlayerColor;
        visorViewData.ClimbFrame = SpriteTools.EmptySprite;
        visorViewData.IdleFrame
            = visorViewData.LeftIdleFrame
                    = visorViewData.FloorFrame
                        = visorSprite;

        var previewData = ScriptableObject.CreateInstance<PreviewViewData>();
        previewData.name = metadata.Name;
        previewData.PreviewSprite = visorSprite;

        var visorData = ScriptableObject.CreateInstance<VisorData>();
        visorData.name = metadata.Name;
        visorData.Free = true;
        visorData.ProductId = fullId;
        visorData.behindHats = metadata.BehindHats;
        visorData.PreviewCrewmateColor = metadata.MatchPlayerColor;
        visorData.ViewDataRef = new AssetReference(HatLocator.GetGuid(fullId, ReferenceType.VisorViewData));
        visorData.PreviewData = new AssetReference(HatLocator.GetGuid(fullId, ReferenceType.Preview));

        var customVisor = new CustomVisor(fullId, visorData, visorViewData, previewData);
        CustomVisors.Add(fullId, customVisor);
        
        visorData.ViewDataRef.LoadAsset<VisorViewData>();
        visorData.PreviewData.LoadAsset<PreviewViewData>();

        return true;
    }
}