using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIImageBenchmarks;

public partial class ListTestsPage : ContentPage
{
    public ListTestsPage()
    {
        InitializeComponent();
        BindingContext= new ListTestsPageModel(this);
    }
}