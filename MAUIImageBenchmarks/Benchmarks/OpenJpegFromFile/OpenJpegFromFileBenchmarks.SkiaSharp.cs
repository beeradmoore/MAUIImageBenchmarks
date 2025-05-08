using BenchmarkDotNet.Attributes;
using SkiaSharp;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpegFromFile;

public partial class OpenJpegFromFileBenchmarks
{
    [Benchmark]
    public void SkiaSharp()
    {
        using (var fileStream = File.OpenRead(_tempTestFile))
        {
            using (var image = SKImage.FromEncodedData(fileStream))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }
    }
}