using RstbLibrary;
using System.Diagnostics;

const string Name = "ResourceSizeTable.product.rsizetable";

Stopwatch watch = Stopwatch.StartNew();

string path = Path.Combine("D:", "Bin", "RSTB", args[0], Name);
Span<byte> data = File.ReadAllBytes(path);

watch.Stop();
Console.WriteLine($"[Alloc] -> {watch.ElapsedMilliseconds}ms, {watch.ElapsedTicks}t");
watch.Restart();

Endianness endian = args[0] == "wiiu" ? Endianness.Big : Endianness.Little;
RSTB rstb = RSTB.FromBinary(data, endian);

watch.Stop();
Console.WriteLine($"[Deserialize (Binary)] -> {watch.ElapsedMilliseconds}ms, {watch.ElapsedTicks}t");
watch.Restart();

string json = path + ".json";
File.WriteAllText(json, rstb.ToText());

watch.Stop();
Console.WriteLine($"[Serialize (Json)] -> {watch.ElapsedMilliseconds}ms, {watch.ElapsedTicks}t");
watch.Restart();

string binary = Path.Combine(Path.GetDirectoryName(path)!, $"Out-{Name}");
using FileStream fs = File.Create(binary);
fs.Write(rstb.ToBinary(endian));

watch.Stop();
Console.WriteLine($"[Serialize (Binary)] -> {watch.ElapsedMilliseconds}ms, {watch.ElapsedTicks}t");
watch.Restart();