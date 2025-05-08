using BenchmarkDotNet.Attributes;

namespace MAUIImageBenchmarks.Benchmarks.OpenJpegFromFile;

public partial class OpenJpegFromFileBenchmarks : IBenchmarkInfo
{
    public static string Name { get; } = "Open jpeg from file";
    readonly string _tempTestFile = Helpers.GetTempJpg();
    
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

    public override string ToString()
    {
        return Name;
    }
}