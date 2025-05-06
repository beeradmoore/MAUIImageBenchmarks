using BenchmarkDotNet.Attributes;
using SkiaSharp;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

public partial class OpenJpegBenchmarks
{
    [Benchmark]
    public async Task SkiaSharp()
    {
        try
        {
            await using (var stream = await FileSystem.OpenAppPackageFileAsync(TestFile).ConfigureAwait(false))
            {
                using (var image = SKBitmap.Decode(stream))
                {
                    Console.WriteLine($"{image.Width}x{image.Height}");
                }
            }
        }
        catch (Exception err)
        {
            Console.WriteLine(err.Message);
        }
    }
}