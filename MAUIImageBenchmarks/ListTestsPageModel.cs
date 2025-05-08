using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIImageBenchmarks;

public partial class ListTestsPageModel : ObservableObject
{
    readonly WeakReference<ListTestsPage> _weakPage;

    public List<BenchmarkMenuItem> BenchmarksList { get; } = new List<BenchmarkMenuItem>();

    [ObservableProperty]
    public partial BenchmarkMenuItem? SelectedItem { get; set; }
    
    public ListTestsPageModel(ListTestsPage page)
    {
        _weakPage = new WeakReference<ListTestsPage>(page);
        
        BenchmarksList.Add(new BenchmarkMenuItem(Benchmarks.OpenJpeg.OpenJpegBenchmarks.Name, typeof(Benchmarks.OpenJpeg.OpenJpegBenchmarks)));
        BenchmarksList.Add(new BenchmarkMenuItem(Benchmarks.OpenPng.OpenPngBenchmarks.Name, typeof(Benchmarks.OpenPng.OpenPngBenchmarks)));

        // Add all benchmarks to the list
        var allBenchmarks = new List<Type>();
        foreach (var item in BenchmarksList)
        {
            foreach (var benchmark in item.Benchmarks)
            {
                if (allBenchmarks.Contains(benchmark) == false)
                {
                    allBenchmarks.Add(benchmark);
                }
            }
        }
        BenchmarksList.Insert(0, new BenchmarkMenuItem("Run All", allBenchmarks.ToArray()));
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(SelectedItem))
        {
            if (SelectedItem is null)
            {
                return;
            }

            var selectedItem = SelectedItem;
            SelectedItem = null;

            if (_weakPage.TryGetTarget(out var page))
            {
                page.Navigation.PushAsync(new RunTestPage(selectedItem));
            }
        }
    }
}