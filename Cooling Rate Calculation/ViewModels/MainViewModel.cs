﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cooling_Rate_Calculator.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() {

        }

        [ObservableProperty] private string title = string.Empty;

        [ObservableProperty] private string carboneum = string.Empty;
        [ObservableProperty] private string niccolum = string.Empty;
        [ObservableProperty] private string cobaltum = string.Empty;
        [ObservableProperty] private string cuprum = string.Empty;
        [ObservableProperty] private string wolframium = string.Empty;
        [ObservableProperty] private string manganum = string.Empty;
        [ObservableProperty] private string silicium = string.Empty;
        [ObservableProperty] private string aluminium = string.Empty;
        [ObservableProperty] private string chromium = string.Empty;
        [ObservableProperty] private string vanadium = string.Empty;
        [ObservableProperty] private string titanium = string.Empty;
        [ObservableProperty] private string molybdaenum = string.Empty;    
        [ObservableProperty] private string niobium = string.Empty;
        [ObservableProperty] private string zirconium = string.Empty;

        [ObservableProperty] private double sigma;
        [ObservableProperty] private double lambda;
        [ObservableProperty] private double gamma;
        [ObservableProperty] private double alpha;

        [RelayCommand]
        void ClearFields()
        {
            Carboneum = string.Empty;
            Niccolum = string.Empty;
            Cobaltum = string.Empty;
            Cuprum = string.Empty;
            Wolframium = string.Empty;
            Manganum = string.Empty;
            Silicium = string.Empty;
            Aluminium = string.Empty;
            Chromium = string.Empty;
            Vanadium = string.Empty;  
            Titanium = string.Empty;
            Molybdaenum = string.Empty;
            Niobium = string.Empty;
            Zirconium = string.Empty;
        }


        [RelayCommand] 
        void EvaluateSigma() {
            try
            {
                Sigma = 0.25f * (1 / 3.0f * (double.Parse(Carboneum) + (0.2f) * (double.Parse(Niccolum) + double.Parse(Cobaltum) + double.Parse(Cuprum) + double.Parse(Wolframium) / 3.0f)) + double.Parse(Manganum) / 14.0f + (double.Parse(Silicium) + double.Parse(Aluminium)) / 7.0f + (double.Parse(Chromium) + double.Parse(Vanadium) + double.Parse(Titanium)) / 13.0f + (double.Parse(Molybdaenum) + double.Parse(Niobium) + double.Parse(Zirconium)) / 24.0f);
                Lambda = (1 / 20.0f) * (11 - 7 * Math.Pow(Sigma, 0.25f));
                Gamma = 0.1f * (55 - 9 * Math.Pow(Sigma, 0.25f));
                Alpha = Lambda / Gamma;
            } catch (Exception e) {
                Console.Error.WriteLine(e.ToString());
            }
            
        }
    }
}
