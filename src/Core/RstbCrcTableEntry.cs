using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace RstbLibrary.Core;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public struct RstbCrcTableEntry
{
    public uint hash;
    public uint size;

    public static void Write(uint hash, uint size, Span<byte> data, int offset, Endianness endian)
    {
        Span<byte> sub = data[offset..];
        if (endian == Endianness.Big) {
            BinaryPrimitives.WriteUInt32BigEndian(sub[0..4], hash);
            BinaryPrimitives.WriteUInt32BigEndian(sub[4..8], size);
        }
        else {
            BinaryPrimitives.WriteUInt32LittleEndian(sub[0..4], hash);
            BinaryPrimitives.WriteUInt32LittleEndian(sub[4..8], size);
        }
    }

    public RstbCrcTableEntry(ReadOnlySpan<byte> data, int offset, Endianness endian)
    {
        ReadOnlySpan<byte> sub = data[offset..];
        if (endian == Endianness.Big) {
            hash = BinaryPrimitives.ReadUInt32BigEndian(sub[0..4]);
            size = BinaryPrimitives.ReadUInt32BigEndian(sub[4..8]);
        }
        else {
            hash = BinaryPrimitives.ReadUInt32LittleEndian(sub[0..4]);
            size = BinaryPrimitives.ReadUInt32LittleEndian(sub[4..8]);
        }
    }
}
