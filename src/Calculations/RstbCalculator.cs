namespace RstbLibrary.Calculations;

public static class RstbCalculator
{
    /// <summary>
    /// Infallibly calculate an <see cref="RSTB"/> value from a file on disk, returning <see langword="null"/> if<br/>
    /// the type is not supported.
    /// </summary>
    public static uint? CalcFromFile(string file, Endianness endian)
    {
        return CalcFromBytesAndName(
            File.ReadAllBytes(file), Path.GetFileName(file), endian);
    }

    /// <summary>
    /// Infallibly calculate an <see cref="RSTB"/> value from a span of bytes and filename,<br/>
    /// returning <see langword="null"/> if the type is not supported.
    /// </summary>
    public static uint? CalcFromBytesAndName(ReadOnlySpan<byte> data, string name, Endianness endian)
    {
        if (data.Length < 8) {
            return null;
        }

        return CalcOrEstimateFromBytesAndName(data, name, endian, estimate: false);
    }

    /// <summary>
    /// Infallibly calculate an <see cref="RSTB"/> value from an uncompressed file size and<br/>
    /// filename, returning <see langword="null"/> if the type is not supported.
    /// </summary>
    public static uint? CalcFromSizeAndName(int size, string name, Endianness endian)
    {
        return CalcOrEstimateFromSizeAndName(size, name, endian, estimate: false);
    }

    /// <summary>
    /// Infallibly calculate <i>or</i> estimate an <see cref="RSTB"/> value from a file on disk,<br/>
    /// returning <see langword="null"/> if the type is not supported.
    /// </summary>
    public static uint? EstimateFromFile(string file, Endianness endian)
    {
        return EstimateFromBytesAndName(
            File.ReadAllBytes(file), Path.GetFileName(file), endian);
    }

    /// <summary>
    /// Infallibly calculate <i>or</i> estimate an <see cref="RSTB"/> value from a span of bytes and<br/>
    /// filename, returning <see langword="null"/> if the type is not supported.
    /// </summary>
    public static uint? EstimateFromBytesAndName(ReadOnlySpan<byte> data, string name, Endianness endian)
    {
        if (data.Length < 8) {
            return null;
        }

        return CalcOrEstimateFromBytesAndName(data, name, endian, estimate: true);
    }

    /// <summary>
    /// Infallibly calculate <i>or</i> estimate an <see cref="RSTB"/> value from an uncompressed file size and<br/>
    /// filename, returning <see langword="null"/> if the type is not supported.
    /// </summary>
    public static uint? EstimateFromSizeAndName(int size, string name, Endianness endian)
    {
        return CalcOrEstimateFromSizeAndName(size, name, endian, estimate: true);
    }

    internal static uint? CalcOrEstimateFromSizeAndName(int size, string name, Endianness endian, bool estimate)
    {
        throw new NotImplementedException();
    }

    internal static uint? CalcOrEstimateFromBytesAndName(ReadOnlySpan<byte> data, string name, Endianness endian, bool estimate)
    {
        throw new NotImplementedException();
    }
}
