using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

[MemoryDiagnoser(false)]
public partial class OpenJpegBenchmarks
{
    public readonly string TestFile = "forest_bridge.jpg";
}