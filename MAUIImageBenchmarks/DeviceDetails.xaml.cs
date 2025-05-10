using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIImageBenchmarks;

public partial class DeviceDetails : ContentPage
{
    public DeviceDetails()
    {
        InitializeComponent();
        BindingContext = new DeviceDetailsModel(this);
    }
}