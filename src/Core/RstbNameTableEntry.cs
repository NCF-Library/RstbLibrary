using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace RstbLibrary.Core;

[StructLayout(LayoutKind.Sequential, Size = 132)]
public ref struct RstbNameTableEntry
{
    public ReadOnlySpan<byte> name;
    public uint size;

    public static unsafe void Write(string name, uint size, Span<byte> data, int offset, Endianness endian)
    {
        Span<byte> sub = data[offset..];
        ReadOnlySpan<byte> nameData = Encoding.UTF8.GetBytes(name);
        for (int i = 0; i < (nameData.Length <= 128 ? nameData.Length : 128); i++) {
            sub[i] = nameData[i];
        }

        if (endian == Endianness.Big) {
            BinaryPrimitives.WriteUInt32BigEndian(sub[128..132], size);
        }
        else {
            BinaryPrimitives.WriteUInt32LittleEndian(sub[128..132], size);
        }
    }

    public unsafe string? GetManagedName()
    {
        fixed (byte* ptr = name) {
            return Utf8StringMarshaller.ConvertToManaged(ptr);
        }
    }

    public RstbNameTableEntry(Span<byte> data, int offset, Endianness endian)
    {
        Span<byte> sub = data[offset..];
        name = sub[0..128];
        size = endian == Endianness.Big
            ? BinaryPrimitives.ReadUInt32BigEndian(sub[128..132])
            : BinaryPrimitives.ReadUInt32LittleEndian(sub[128..132]);
    }
}
