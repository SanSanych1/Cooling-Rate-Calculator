using Android.Locations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using static Android.Renderscripts.ScriptGroup;

namespace Cooling_Rate_Calculator.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {

        }

        [ObservableProperty] private string title = string.Empty;
        [ObservableProperty] private string hideElementsButtonText = "Показать элементы";
        [ObservableProperty] private string hideOtherFieldsButtonText = "Показать другие поля";

        private string marking = "10Г2ФБЮ";
        public string Marking
        {
            get
            {
                return marking;
            }
            set
            {
                marking = value;
                DecodeMark();
                Evaluate();
                OnPropertyChanged();
            }
        }
        private bool isElementsVisible = false;
        public bool IsElementsVisible
        {
            get 
            { 
                return isElementsVisible; 
            }
            set
            {
                isElementsVisible = value;
                if (value)
                    HideElementsButtonText = "Скрыть элементы";
                else
                    HideElementsButtonText = "Показать элементы";
                OnPropertyChanged();
            }
        }
        private bool isOtherFieldsVisible = false;
        public bool IsOtherFieldsVisible
        {
            get
            {
                return isOtherFieldsVisible;
            }
            set
            {
                isOtherFieldsVisible = value;
                if (value)
                    HideOtherFieldsButtonText = "Скрыть другие поля";
                else
                    HideOtherFieldsButtonText = "Показать другие поля";
                OnPropertyChanged();
            }
        }

        #region ElementsProperties

        /// <summary>
        /// Элементы сплава
        /// </summary>
        [ObservableProperty] private double carboneum;
        [ObservableProperty] private double niccolum;
        [ObservableProperty] private double cobaltum;
        [ObservableProperty] private double cuprum;
        [ObservableProperty] private double wolframium;
        [ObservableProperty] private double manganum;
        [ObservableProperty] private double silicium;
        [ObservableProperty] private double aluminium;
        [ObservableProperty] private double chromium;
        [ObservableProperty] private double vanadium;
        [ObservableProperty] private double titanium;
        [ObservableProperty] private double molybdaenum;
        [ObservableProperty] private double niobium;
        [ObservableProperty] private double zirconium;

        #endregion

        #region OtherFields

        [ObservableProperty] private double thicknessOfWeldedSheets;
        [ObservableProperty] private double temperatureMinOfAustenite;
        [ObservableProperty] private double temperatureHeating;
        [ObservableProperty] private double weldingSpeed;
        [ObservableProperty] private double voltage;
        [ObservableProperty] private double currentStrength;
        [ObservableProperty] private double thermalPowerOfTheArc;
        [ObservableProperty] private double heatInputOfWelding;
        [ObservableProperty] private double electrodeDiameter;
        [ObservableProperty] private double specificHeat;
        [ObservableProperty] private double matterDensity;

        #endregion

        #region Sigma, Lambda, Gamma, Alpha, Liquidus

        [ObservableProperty] private double sigma;
        [ObservableProperty] private double lambda;
        [ObservableProperty] private double gamma;
        [ObservableProperty] private double alpha;
        [ObservableProperty] private double liquidus;
        [ObservableProperty] private double coolingSpeed;

        #endregion

        #region Functions

        double EvaluateSigma()
        {
            return ((Carboneum + (Niccolum + Cobaltum + Cuprum + Wolframium / 3.0d) / 5.0d) / 3.0d +
                    Manganum / 14.0d +
                    (Silicium + Aluminium) / 7.0d +
                    (Chromium + Vanadium + Titanium) / 13.0d +
                    (Molybdaenum + Niobium + Zirconium) / 24.0d) / 4;
        }

        double EvaluateLambda()
        {
            return (11 - 7 * Math.Pow(Sigma, 0.25d)) / 20.0d;
        }

        double EvaluateGamma()
        {
            return (55 - 9 * Math.Pow(Sigma, 0.25f)) / 10.0d;
        }

        double EvaluateAlpha()
        {
            return Lambda / Gamma;
        }

        double EvaluateLiquidus()
        {
            return  5 * (4 * (77 - 3 * Carboneum) - Manganum) - 
                    (13 * Math.Sqrt(Chromium) + 25 * Math.Sqrt(Niccolum)) / 2 - 
                    2 * (4 * Silicium + Molybdaenum + Vanadium + Cobaltum + Aluminium + 3 * Cuprum + 7 * Titanium + 9 * Niobium) - 
                    13 * Zirconium;
        }

        #endregion

        #region Commands
        
        [RelayCommand]
        void ClearFields()
        {
            Carboneum = 0;
            Niccolum = 0;
            Cobaltum = 0;
            Cuprum = 0;
            Wolframium = 0;
            Manganum = 0;
            Silicium = 0;
            Aluminium = 0;
            Chromium = 0;
            Vanadium = 0;
            Titanium = 0;
            Molybdaenum = 0;
            Niobium = 0;
            Zirconium = 0;
        }

        [RelayCommand]
        void DecodeMark()
        {
            ClearFields();

            var tmp = "";
            var elements = new List<string>();

            int index = 0;
            foreach (char sym in Marking)
            {
                if (char.IsDigit(sym))
                {
                    tmp += sym;
                    if (index == Marking.Length - 1)
                        elements.Add(tmp);
                }
                else
                {
                    if (tmp.Length != 0)
                        elements.Add(tmp);
                    tmp = sym.ToString();
                }
                index++;
            }
            
            index = 0;

            foreach (string elem in elements)
            { 
                double _carboneum = 0;
                if(index == 0 && double.TryParse(elem, out _carboneum))
                    Carboneum = _carboneum / 100; 
                else
                {
                    double persent;
                    if (elem.Length == 1)
                        persent = 1;
                    else
                        persent = double.Parse(elem.Substring(1, elem.Length-1));


                    switch (char.ToUpper(elem[0]))
                    {
                        case 'Н':
                            Niccolum = persent;
                            break;
                        case 'К':
                            Cobaltum = persent;
                            break;
                        case 'Д':
                            Cuprum = persent;
                            break;
                        case 'В':
                            Wolframium = persent;
                            break;
                        case 'Г':
                            Manganum = persent;
                            break;
                        case 'С':
                            Silicium = persent;
                            break;
                        case 'Ю':
                            Aluminium = persent;
                            break;
                        case 'Х':
                            Chromium = persent;
                            break;
                        case 'Ф':
                            Vanadium = persent;
                            break;
                        case 'Т':
                            Titanium = persent;
                            break;
                        case 'М':
                            Molybdaenum = persent;
                            break;
                        case 'Б':
                            Niobium = persent;
                            break;
                        case 'Ц':
                            Zirconium = persent;
                            break;

                    }
                }
                
                index++;
            }
        }

        [RelayCommand]
        void Evaluate()
        {
            Sigma = EvaluateSigma();
            Lambda = EvaluateLambda();
            Gamma = EvaluateGamma();
            Alpha = EvaluateAlpha();
            Liquidus = EvaluateLiquidus();
        }

        [RelayCommand]
        void EvaluateCoolingSpeed()
        {
            CoolingSpeed = 2 * Math.PI * Lambda * Gamma * Math.Pow(TemperatureMinOfAustenite-TemperatureHeating,3)/Math.Pow(HeatInputOfWelding/ WeldingSpeed*ThicknessOfWeldedSheets, 2);
        }

        [RelayCommand]
        void HideElements()
        {
            
            IsOtherFieldsVisible = false;
            IsElementsVisible = !IsElementsVisible;
            
        }

        [RelayCommand]
        void HideOtherFields()
        {
            IsOtherFieldsVisible = !IsOtherFieldsVisible;
            IsElementsVisible = false;
        }

        #endregion
    }
}
