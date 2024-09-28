using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Windows.Threading;
using System.Globalization;
using System.Windows.Media;

namespace StagesView
{
    public class StagesViewModel : ViewModelBase
    {
        DispatcherTimer _updateTimer;

        private StageViewModel _stageTotal;
        private IStageColorScheme _colorScheme;

        public StagesViewModel()
        {
            ColorSchemes = new ObservableCollection<IStageColorScheme>
            {
                new CoveColorScheme(),
                new WisteriaColorScheme(),
                new SkyColorScheme()
            };

            IStageColorScheme defaultColorScheme = ColorSchemes.First();
            _stageTotal = new StageViewModel("All Stages", defaultColorScheme);
            Stages.Add(new StageViewModel("Prepare", defaultColorScheme));
            Stages.Add(new StageViewModel("Verify", defaultColorScheme));
            Stages.Add(new StageViewModel("Justify", defaultColorScheme));
            Stages.Add(new StageViewModel("Build GV", defaultColorScheme));
            Stages.Add(new StageViewModel("Process Noodles", defaultColorScheme));
            Stages.Add(new StageViewModel("Destroy Worlds", defaultColorScheme));
            Stages.Add(new StageViewModel("Build Worlds", defaultColorScheme));
            Stages.Add(new StageViewModel("Finish", defaultColorScheme));

            StageService.Get().StageStateChangedEvent += StageStageChanged;

            _updateTimer = new DispatcherTimer();
            _updateTimer.Tick += OnUpdateTime;
            _updateTimer.Interval = TimeSpan.FromSeconds(.05);
            _updateTimer.Start();

            StageService.Get().ResetAllEvent += ResetAllStages;

            ColorScheme = defaultColorScheme;
        }

        private void InitializeStages()
        {

        }

        public string Title
        {
            get
            {
                return "Stage View";
            }
        }

        public ObservableCollection<IStageColorScheme> ColorSchemes { get; set; }

        public IStageColorScheme ColorScheme
        {
            get
            {
                return _colorScheme;
            }

            set
            {
                if (value == null)
                {
                    return;
                }
                _colorScheme = value;
                foreach (var stage in Stages)
                {
                    stage.ColorScheme = _colorScheme;
                }
                RaisePropertyChanged(nameof(ColorScheme));
                RaisePropertyChanged(nameof(TimeColor));
                RaisePropertyChanged(nameof(ExpectedTimeColor));
            }
        }

        public StageViewModel StageTotal
        {
            get
            {
                return _stageTotal;
            }
        }

        private void ResetAllStages()
        {
            foreach (var stage in Stages)
            {
                stage.Reset();
            }

            _stageTotal.Reset();
        }

        public BindingList<StageViewModel> Stages { get; set; } = new BindingList<StageViewModel>();

        public LinearGradientBrush TimeColor
        {
            get
            {
                System.Windows.Media.Color color2 = StagesViewModel.GetColorFromHexString(ColorScheme.TimeColor);

                return GetGradient(color2);
            }
        }

        public LinearGradientBrush ExpectedTimeColor
        {
            get
            {
                System.Windows.Media.Color color2 = StagesViewModel.GetColorFromHexString(ColorScheme.ExpectedTimeColor);

                return GetGradient(color2);
            }
        }

        public void StageStageChanged(string stageLabel, StageResult result, bool isRunning)
        {
            StageViewModel stage = Stages.Where(x => x.Label == stageLabel).FirstOrDefault();
            if (stage != null)
            {
                stage.Result = result;
                stage.IsRunning = isRunning;
                if (stage.Time == null)
                {
                    stage.Time = TimeSpan.FromSeconds(0);
                }
            }

            if (stageLabel == _stageTotal.Label)
            {
                _stageTotal.Result = result;
                _stageTotal.IsRunning = isRunning;
            }
        }

        private void OnUpdateTime(object sender, EventArgs e)
        {
            DateTime current = DateTime.Now;

            foreach (var stage in Stages)
            {
                UpdateStage(stage);
            }

            UpdateStage(_stageTotal);
        }

        private void UpdateStage(StageViewModel stage)
        {
            if (stage.IsRunning)
            {
                if (stage.StartTime == null)
                {
                    stage.StartTime = DateTime.Now;
                    stage.Time = TimeSpan.FromSeconds(0);
                }
                else
                {
                    stage.Time = DateTime.Now - stage.StartTime;
                }
            }
        }

        public static LinearGradientBrush GetGradient(System.Windows.Media.Color color)
        {
            byte xFactor = 30;

            System.Windows.Media.Color color2 = Colors.White;
            if (color.R <= xFactor)
            {
                color2.R = 0;
            }
            else
            {
                color2.R = (byte)((int)color.R - xFactor);
            }

            if (color.G <= xFactor)
            {
                color2.G = 0;
            }
            else
            {
                color2.G = (byte)((int)color.G - xFactor);
            }

            if (color.B <= xFactor)
            {
                color2.B = 0;
            }
            else
            {
                color2.B = (byte)((int)color.B - xFactor);
            }
            LinearGradientBrush myHorizontalGradient = new LinearGradientBrush();
            myHorizontalGradient.StartPoint = new System.Windows.Point(0, 0.5);
            myHorizontalGradient.EndPoint = new System.Windows.Point(1, 0.5);
            myHorizontalGradient.GradientStops.Add(
                new GradientStop(color2, 0.0));
            myHorizontalGradient.GradientStops.Add(
                new GradientStop(color, 0.5));
            myHorizontalGradient.GradientStops.Add(
                new GradientStop(color2, 1.0));
            return myHorizontalGradient;
        }

        public static System.Windows.Media.Color GetColorFromHexString(string hexColor)
        {
            System.Windows.Media.Color color = Colors.White;
            if (hexColor == null)
            {
                return color;
            }

            if (hexColor[0] == '#')
            {
                hexColor = hexColor.Substring(1);
            }

            if (hexColor.Length == 8)
            {
                color.A = (byte)Int32.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
                color.R = (byte)Int32.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
                color.G = (byte)Int32.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
                color.B = (byte)Int32.Parse(hexColor.Substring(6, 2), NumberStyles.HexNumber);
            }
            else if (hexColor.Length == 6)
            {
                color.R = (byte)Int32.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber);
                color.G = (byte)Int32.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber);
                color.B = (byte)Int32.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber);
            }

            return color;
        }
    }
}
