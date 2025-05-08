using System.Diagnostics;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUIImageBenchmarks;

public partial class RunTestPageModel : ObservableObject
{
    readonly WeakReference<RunTestPage> _weakPage;

    [ObservableProperty]
    public partial bool IsRunning { get; set; } = false;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShareResultsCommand))]
    public partial string Results { get; set; } = string.Empty;

    [ObservableProperty]
    public partial int Countdown { get; set; } = 5;

    bool _timerEnabled = true;
    
    public BenchmarkMenuItem BenchmarkMenuItem { get; private set; }

    public RunTestPageModel(RunTestPage page, BenchmarkMenuItem benchmarkMenuItem)
    {
        _weakPage = new WeakReference<RunTestPage>(page);
        BenchmarkMenuItem = benchmarkMenuItem;
        
        page.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (_timerEnabled == false)
            {
                return false;
            }
            
            --Countdown;

            if (Countdown == 0)
            {
                _ = StartBenchmarksAsync();
                return false;
            }

            return true;
        });
    }

    async Task StartBenchmarksAsync()
    {
        if (_weakPage.TryGetTarget(out var pagePre))
        {
            pagePre.DisableBack();
        }
        IsRunning = true;

        
        var artifactsPath = Path.Combine(Path.GetTempPath(), "BenchmarkBindings", "Artifacts");
        if (Directory.Exists(artifactsPath) == false)
        {
            Directory.CreateDirectory(artifactsPath);
        }

        var logger = new AccumulationLogger();

        try
        {
            
            /*
            var config = ManualConfig.CreateMinimumViable()
                .AddJob(Job.Default.WithToolchain(new InProcessEmitToolchain(TimeSpan.FromMinutes(10), logOutput: true)))
                .AddDiagnoser(MemoryDiagnoser.Default)
                .AddLogger(ConsoleLogger.Default)
                .WithArtifactsPath(artifactsPath);
            config.UnionRule = ConfigUnionRule.AlwaysUseGlobal; // Overriding the default


            //.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Alphabetical));

         
            */

            await Task.Run(() =>
            {
#if RELEASE
                var config = ManualConfig.CreateMinimumViable();
#else
                var config = new DebugInProcessConfig();
#endif
                config.AddLogger(logger);
                config.WithArtifactsPath(artifactsPath);

                var summaries = BenchmarkRunner.Run(BenchmarkMenuItem.Benchmarks, config);
                foreach (var summary in summaries)
                {
                    MarkdownExporter.Console.ExportToLog(summary, logger);
                    ConclusionHelper.Print(logger,
                        summary.BenchmarksCases
                            .SelectMany(benchmark => benchmark.Config.GetCompositeAnalyser().Analyse(summary))
                            .Distinct()
                            .ToList());
                }
            });
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error: {err.Message}");
        }

        Results = logger.GetLog();

        IsRunning = false;
        if (_weakPage.TryGetTarget(out var pagePost))
        {
            pagePost.EnableBack();
        }
    }

    [RelayCommand(CanExecute = nameof(CanShareResults))]
    async Task ShareResultsAsync()
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = Results,
            Title = "Share results"
        });
    }

    bool CanShareResults()
    {
        return string.IsNullOrWhiteSpace(Results) == false;
    }

    internal void CancelCountdown()
    {
        _timerEnabled = false;
    }
}