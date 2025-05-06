using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

public partial class OpenJpegBenchmarks
{
    [Benchmark]
    public async Task ImageSharp()
    {
        try
        {
            await using (var stream = await FileSystem.OpenAppPackageFileAsync(TestFile).ConfigureAwait(false))
            {
                // We complete the LoadAsync
                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
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