using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIImageBenchmarks;

public partial class RunTestPage : ContentPage
{
    bool _canGoBack = true;
    
    public RunTestPage(BenchmarkMenuItem benchmarkMenuItem)
    {
        InitializeComponent();
        BindingContext = new RunTestPageModel(this, benchmarkMenuItem);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Don't let device sleep while testing.
        DeviceDisplay.Current.KeepScreenOn = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is RunTestPageModel model)
        {
            model.CancelCountdown();
        }

        DeviceDisplay.Current.KeepScreenOn = false;
    }

    protected override bool OnBackButtonPressed()
    {
        // Disable going back if we are running a test
        return _canGoBack == false;
    }

    internal void EnableBack()
    {
        _canGoBack = true;
        NavigationPage.SetHasBackButton(this, true);
    }

    internal void DisableBack()
    {
        _canGoBack = false;
        NavigationPage.SetHasBackButton(this, false);
    }
}