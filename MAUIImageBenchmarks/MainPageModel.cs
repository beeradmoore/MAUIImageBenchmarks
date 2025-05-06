using System.Diagnostics;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIImageBenchmarks.Benchmarks.ImageSharp;

namespace MAUIImageBenchmarks;

public partial class MainPageModel : ObservableObject
{
    [RelayCommand]
    async Task RunBenchmarks()
    {
        var test = new Benchmarks.OpenJpeg.OpenJpegBenchmarks();
        await test.ImageSharp();

        return;
        var artifactsPath = Path.Combine(Path.GetTempPath(), "BenchmarkBindings", "Artifacts");
        if (Directory.Exists(artifactsPath) == false)
        {
            Directory.CreateDirectory(artifactsPath);
        }
		
        var config = ManualConfig.CreateMinimumViable()
            .AddJob(Job.Default.WithToolchain(new InProcessEmitToolchain(TimeSpan.FromMinutes(10), logOutput: true)))
            .AddDiagnoser(MemoryDiagnoser.Default)
            .WithArtifactsPath(artifactsPath)
            .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical));
        config.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default

        var benchmarks = new[]
        {
            //typeof(Test),
            typeof(Benchmarks.OpenJpeg.OpenJpegBenchmarks),
        };


        try
        {
            // var summary = BenchmarkRunner.Run(benchmarks[0], config);
            var summaries = await Task.Run<Summary[]>(() =>
            {
                return BenchmarkRunner.Run(benchmarks, config.WithOptions(ConfigOptions.DisableLogFile));
            });
            
            /*
            foreach (var summary in summaries)
            {
                MarkdownExporter.Console.ExportToLog(summary, logger);
                ConclusionHelper.Print(logger,
                    summary.BenchmarksCases
                        .SelectMany(benchmark => benchmark.Config.GetCompositeAnalyser().Analyse(summary))
                        .Distinct()
                        .ToList());
            }
            */
        }
        catch (Exception err)
        {
            Debugger.Break();
        }
    }
}