using BenchmarkDotNet.Attributes;
using SkiaSharp;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

public partial class OpenJpegBenchmarks
{
    [Benchmark]
    public async Task SkiaSharp()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = SKImage.FromEncodedData(stream))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }
    }
}