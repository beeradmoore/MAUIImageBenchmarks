using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenPng;

[MemoryDiagnoser(false)]
public class OpenPngBenchmarks : IBenchmarkInfo
{
    public static string Name { get; } = "Open png";

    readonly string _tempTestFile = Helpers.GetTempPng();
    
    readonly int _expectedImageWidth = 582;
    readonly int _expectedImageHeight = 453;
    
    [GlobalSetup]
    public async Task SetupAsync()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            await using (var fileStream = File.Create(_tempTestFile))
            {
                await stream.CopyToAsync(fileStream).ConfigureAwait(false);
            }
        }
    }
    
    [Benchmark]
    public async Task ImageSharp_FromResource()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            using (var image = await SixLabors.ImageSharp.Image.LoadAsync(stream).ConfigureAwait(false))
            {
                if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
                {
                    throw new Exception("Image width and height are different, image not loaded correctly.");
                }
            }
        }
    }
    
    [Benchmark]
    public async Task ImageSharp_FromFile()
    { 
        using (var image = await SixLabors.ImageSharp.Image.LoadAsync(_tempTestFile).ConfigureAwait(false))
        {
            if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
            {
                throw new Exception("Image width and height are different, image not loaded correctly.");
            }
        }
    }
    
    [Benchmark]
    public async Task SkiaSharp_FromResource()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            using (var image = SkiaSharp.SKImage.FromEncodedData(stream))
            {
                if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
                {
                    throw new Exception("Image width and height are different, image not loaded correctly.");
                }
            }
        }
    }
    
    [Benchmark]
    public void SkiaSharp_FromFile()
    {
        using (var fileStream = File.OpenRead(_tempTestFile))
        {
            using (var image = SkiaSharp.SKImage.FromEncodedData(fileStream))
            {
                if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
                {
                    throw new Exception("Image width and height are different, image not loaded correctly.");
                }
            }
        }
    }
    
    
#if __ANDROID__
    [Benchmark(Baseline = true)]
    public async Task Native_Android_FromResource()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.PngTestImage).ConfigureAwait(false))
        {
            using (var image = await Android.Graphics.BitmapFactory.DecodeStreamAsync(stream).ConfigureAwait(false))
            {
                if (image is null)
                {
                    throw new Exception("Failed to DecodeStreamAsync.");
                }
                if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
                {
                    throw new Exception("Image width and height are different, image not loaded correctly.");
                }
            }
        }
    }
    
    [Benchmark]
    public async Task Native_Android_FromFile()
    {
        using (var image = await Android.Graphics.BitmapFactory.DecodeFileAsync(_tempTestFile).ConfigureAwait(false))
        {
            if (image is null)
            {
                throw new Exception("Failed to DecodeStreamAsync.");
            }
            if (image.Width != _expectedImageWidth || image.Height != _expectedImageHeight)
            {
                throw new Exception("Image width and height are different, image not loaded correctly.");
            }
        }
    }
#endif
    
}