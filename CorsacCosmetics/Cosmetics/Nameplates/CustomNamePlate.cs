using UnityEngine;

namespace CorsacCosmetics.Cosmetics.Nameplates;

public record CustomNamePlate(
    string Id,
    NamePlateData NamePlateData,
    NamePlateViewData NamePlateViewData,
    PreviewViewData PreviewData
);