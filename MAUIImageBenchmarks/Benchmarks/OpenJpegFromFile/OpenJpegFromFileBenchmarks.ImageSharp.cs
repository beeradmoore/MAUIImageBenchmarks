using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpegFromFile;

public partial class OpenJpegFromFileBenchmarks
{
    [Benchmark]
    public async Task ImageSharp()
    { 
        using (var image = await SixLabors.ImageSharp.Image.LoadAsync(_tempTestFile).ConfigureAwait(false))
        {
            Console.WriteLine($"{image.Width}x{image.Height}");
        }
    }
}
