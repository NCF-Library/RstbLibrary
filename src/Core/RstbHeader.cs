using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace RstbLibrary.Core;

[StructLayout(LayoutKind.Sequential, Size = 12)]
internal readonly ref struct RstbHeader
{
    private readonly ReadOnlySpan<byte> _magic;
    private readonly int _crcMapCount;
    private readonly int _nameMapCount;

    public int CrcMapCount => _crcMapCount;
    public int NameMapCount => _nameMapCount;

    public int GetBufferSize()
    {
        int headerSize = 12; // Marshal.SizeOf<RstbHeader>();
        int crcEntrySize = 8; // Marshal.SizeOf<RstbCrcTableEntry>();
        int nameEntrySize = 132; // Marshal.SizeOf<RstbNameTableEntry>();

        return headerSize + (_crcMapCount * crcEntrySize) + (_nameMapCount * nameEntrySize);
    }

    public unsafe void Write(Span<byte> data, int offset, Endianness endian)
    {
        Span<byte> sub = data[offset..];
        // 0x52535442 == "RSTB"u8
        BinaryPrimitives.WriteUInt32BigEndian(sub[0..4], 0x52535442);

        if (endian == Endianness.Big) {
            BinaryPrimitives.WriteInt32BigEndian(sub[4..8], _crcMapCount);
            BinaryPrimitives.WriteInt32BigEndian(sub[8..12], _nameMapCount);
        }
        else {
            BinaryPrimitives.WriteInt32LittleEndian(sub[4..8], _crcMapCount);
            BinaryPrimitives.WriteInt32LittleEndian(sub[8..12], _nameMapCount);
        }
    }

    public RstbHeader(ReadOnlySpan<byte> data, Endianness endian)
    {
        _magic = data[0..4];

        if (!_magic.SequenceEqual("RSTB"u8)) {
            throw new InvalidDataException("Invalid RSTB magic");
        }

        if (endian == Endianness.Big) {
            _crcMapCount = BinaryPrimitives.ReadInt32BigEndian(data[4..8]);
            _nameMapCount = BinaryPrimitives.ReadInt32BigEndian(data[8..12]);
        }
        else {
            _crcMapCount = BinaryPrimitives.ReadInt32LittleEndian(data[4..8]);
            _nameMapCount = BinaryPrimitives.ReadInt32LittleEndian(data[8..12]);
        }
    }

    public RstbHeader(int crcMapCount, int nameMapCount)
    {
        _magic = "RSTB"u8;
        _crcMapCount = crcMapCount;
        _nameMapCount = nameMapCount;
    }
}
