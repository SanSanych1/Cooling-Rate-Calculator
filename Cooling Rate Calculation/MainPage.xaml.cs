using Cooling_Rate_Calculator.ViewModels;

namespace Cooling_Rate_Calculation;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

