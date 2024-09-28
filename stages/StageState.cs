using System;

namespace StagesView
{
    public enum StageResult
    {
        None,
        Error,
        Warning,
        Success
    }

    public class StageState
    {
        public string Label { get; set; } = string.Empty;
        public StageResult Result { get; set; }
        public bool IsRunning { get; set; }
        public string Time { get; set; } = "---";
    }
}
