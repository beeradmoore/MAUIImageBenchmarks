using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

[MemoryDiagnoser(false)]
public partial class OpenJpegBenchmarks : IBenchmarkInfo
{
    public static string Name { get; } = "Open jpeg from resource";

    public override string ToString()
    {
        return Name;
    }
}