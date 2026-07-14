namespace CorsacCosmetics.Cosmetics.Visors;

public record CustomVisor(
    string Id,
    VisorData VisorData,
    VisorViewData VisorViewData,
    PreviewViewData PreviewData
);