using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpeg;

[MemoryDiagnoser(false)]
public class OpenJpegBenchmarks : IBenchmarkInfo
{
    public static string Name { get; } = "Open jpeg";

    readonly string _tempTestFile = Helpers.GetTempJpg();
    
    readonly int _expectedImageWidth = 1999;
    readonly int _expectedImageHeight = 2998;
    
    [GlobalSetup]
    public async Task SetupAsync()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
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
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
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
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
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
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
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
#elif __MACCATALYST__ 
    [Benchmark(Baseline = true)]
    public async Task Native_MacCatalyst_FromResource()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var data = Foundation.NSData.FromStream(stream))
            {
                if (data is null)
                {
                    throw new Exception("Failed to NSData.FromStream.");
                }
                using (var image = UIKit.UIImage.LoadFromData(data))
                {
                    if (image is null)
                    {
                        throw new Exception("Failed to UIImage.LoadFromData.");
                    }
                    if (image.Size.Width != _expectedImageWidth || image.Size.Height != _expectedImageHeight)
                    {
                        throw new Exception("Image width and height are different, image not loaded correctly.");
                    }
                }
            }
        }
    }
    
    [Benchmark]
    public void Native_MacCatalyst_FromFile()
    {
        using (var image = UIKit.UIImage.FromFile(_tempTestFile))
        {
            if (image is null)
            {
                throw new Exception("Failed to UIImage.LoadFromData.");
            }
            if (image.Size.Width != _expectedImageWidth || image.Size.Height != _expectedImageHeight)
            {
                throw new Exception("Image width and height are different, image not loaded correctly.");
            }
        }
    }
#elif __IOS__ 
    [Benchmark(Baseline = true)]
    public async Task Native_iOS_FromResource()
    {
        await using (var stream = await FileSystem.OpenAppPackageFileAsync(Helpers.JpgTestImage).ConfigureAwait(false))
        {
            using (var data = Foundation.NSData.FromStream(stream))
            {
                if (data is null)
                {
                    throw new Exception("Failed to NSData.FromStream.");
                }
                using (var image = UIKit.UIImage.LoadFromData(data))
                {
                    if (image is null)
                    {
                        throw new Exception("Failed to UIImage.LoadFromData.");
                    }
                    if (image.Size.Width != _expectedImageWidth || image.Size.Height != _expectedImageHeight)
                    {
                        throw new Exception("Image width and height are different, image not loaded correctly.");
                    }
                }
            }
        }
    }
    
    [Benchmark]
    public void Native_iOS_FromFile()
    {
        using (var image = UIKit.UIImage.FromFile(_tempTestFile))
        {
            if (image is null)
            {
                throw new Exception("Failed to UIImage.LoadFromData.");
            }
            if (image.Size.Width != _expectedImageWidth || image.Size.Height != _expectedImageHeight)
            {
                throw new Exception("Image width and height are different, image not loaded correctly.");
            }
        }
    }
#endif
    
}