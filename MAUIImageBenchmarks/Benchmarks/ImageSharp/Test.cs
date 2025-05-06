using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.ImageSharp;

[MemoryDiagnoser(false)]
public class Test
{
    [Benchmark]
    public async Task DoTestAsync()
    {
        await Task.Delay(100);
    }
}