using System;
using System.Collections.Generic;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace StagesView
{
    public class StageViewModel : ViewModelBase
    {
        private string _label = string.Empty;
        private StageResult _result;
        private bool _isRunning = false;
        private TimeSpan? _time = null;
        private TimeSpan? _expectedTime = null;
        private List<TimeSpan?> _previousTimes = new List<TimeSpan?>();
        private IStageColorScheme _colorScheme;

        public StageViewModel(string label, IStageColorScheme colorScheme)
        {
            Label = label;
            ColorScheme = colorScheme;
        }

        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                RaisePropertyChanged(nameof(Label));
            }
        }

        public IStageColorScheme ColorScheme
        {
            get
            {
                return _colorScheme;
            }

            set
            {
                _colorScheme = value;
                RaisePropertyChanged(nameof(ColorScheme));
                RaisePropertyChanged(nameof(StageColorBrush));
                RaisePropertyChanged(nameof(TimeColor));
                RaisePropertyChanged(nameof(ExpectedTimeColor));
            }
        }

        public StageResult Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                RaisePropertyChanged(nameof(Result));
                RaisePropertyChanged(nameof(StageColorBrush));
                RaisePropertyChanged(nameof(Time));
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                RaisePropertyChanged(nameof(IsRunning));
                RaisePropertyChanged(nameof(StageColorBrush));
                RaisePropertyChanged(nameof(BoxBorderThickness));
            }
        }

        public DateTime? StartTime { get; set; } = null;

        public TimeSpan? Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        public TimeSpan? ExpectedTime
        {
            get
            {
                if (_previousTimes.Count == 0)
                {
                    return null;
                }

                double ms = 0;
                foreach (var t in _previousTimes)
                {
                    ms += t == null ? 0 : ((TimeSpan)t).TotalMilliseconds;
                }

                ms /= (double)_previousTimes.Count;

                TimeSpan result = TimeSpan.FromMilliseconds((int)ms);
                return result;
            }
        }

        public List<TimeSpan?> PreviousTimes
        {
            get
            {
                return _previousTimes;
            }
            set
            {
                _previousTimes = value;
                RaisePropertyChanged(nameof(PreviousTimes));
            }
        }

        public int BoxWidth
        {
            get
            {
                return 90;
            }
        }

        public int BoxHeight
        {
            get
            {
                return 60;
            }
        }

        public int BoxBorderThickness
        {
            get
            {
                return IsRunning ? 2 : 1;
            }
        }

        public int TimeBoxHeight
        {
            get
            {
                return 25;
            }
        }

        public LinearGradientBrush StageColorBrush
        {
            get
            {
                System.Windows.Media.Color color = Colors.Black;
                switch (Result)
                {
                    case StageResult.Error:
                        color.R = 255;
                        color.G = 10;
                        color.B = 10;
                        break;
                    case StageResult.Warning:
                        color = Colors.Orange;
                        break;
                    case StageResult.Success:
                        color.R = 60;
                        color.G = 250;
                        color.B = 60;
                        break;
                    case StageResult.None:
                        {
                            if (IsRunning)
                            {
                                color = StagesViewModel.GetColorFromHexString(ColorScheme.RunningColor);
                            }
                            else
                            {
                                color = StagesViewModel.GetColorFromHexString(ColorScheme.NotRunningColor);
                            }
                            break;
                        }
                }

                return StagesViewModel.GetGradient(color);
            }
        }

        public LinearGradientBrush TimeColor
        {
            get
            {
                System.Windows.Media.Color color = StagesViewModel.GetColorFromHexString(ColorScheme.TimeColor);

                return StagesViewModel.GetGradient(color);
            }
        }

        public LinearGradientBrush ExpectedTimeColor
        {
            get
            {
                System.Windows.Media.Color color = StagesViewModel.GetColorFromHexString(ColorScheme.ExpectedTimeColor);

                return StagesViewModel.GetGradient(color);
            }
        }

        public void Reset()
        {
            if (Time != null && Result != StageResult.Error)
            {
                PreviousTimes.Add(Time);
                RaisePropertyChanged(nameof(ExpectedTime));
            }
            Result = StageResult.None;
            Time = null;
            IsRunning = false;
            StartTime = null;
        }
    }
}
