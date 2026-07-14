using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.InteropServices;

namespace CorsacCosmetics.Cosmetics.Bundle.V2;

[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
public readonly record struct BundleHeaderV2
{
    public const uint ExpectedMagic = 0x434f5253; // 'CORS' in ASCII
    public const ushort CurrentVersion = 2;

    public uint Magic { get; init; }
    public ushort Version { get; init; }
    public ushort Flags { get; init; }
    public uint ManifestLength { get; init; }
    public uint DataLength { get; init; }
    
    public bool IsValid => Magic == ExpectedMagic;
    public bool IsSupportedVersion => Version is CurrentVersion;

    public static BundleHeaderV2 Read(Stream stream)
    {
        var buffer = new byte[16];
        if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
            throw new EndOfStreamException("Could not read the full bundle header.");
        
        return new BundleHeaderV2
        {
            Magic = BinaryPrimitives.ReadUInt32LittleEndian(buffer.AsSpan<byte>(0, 4)),
            Version = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan<byte>(4, 2)),
            Flags = BinaryPrimitives.ReadUInt16LittleEndian(buffer.AsSpan<byte>(6, 2)),
            ManifestLength = BinaryPrimitives.ReadUInt32LittleEndian(buffer.AsSpan<byte>(8, 4)),
            DataLength = BinaryPrimitives.ReadUInt32LittleEndian(buffer.AsSpan<byte>(12, 4))
        };
    }

    public static void Write(Stream stream, BundleHeaderV2 header)
    {
        var buffer = new byte[16];
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.AsSpan<byte>(0, 4), header.Magic);
        BinaryPrimitives.WriteUInt16LittleEndian(buffer.AsSpan<byte>(4, 2), header.Version);
        BinaryPrimitives.WriteUInt16LittleEndian(buffer.AsSpan<byte>(6, 2), header.Flags);
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.AsSpan<byte>(8, 4), header.ManifestLength);
        BinaryPrimitives.WriteUInt32LittleEndian(buffer.AsSpan<byte>(12, 4), header.DataLength);
        
        stream.Write(buffer, 0, buffer.Length);
    }
}