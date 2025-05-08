using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

public partial class OpenJpegBenchmarks
{
    [Benchmark]
    public async Task ImageSharp()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                Console.WriteLine($"{image.Width}x{image.Height}");
            }
        }
    }
}