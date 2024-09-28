using System;

namespace StagesView
{
    public class CoveColorScheme : IStageColorScheme
    {
        public string Name { get; } = "Cove";
        public string ExpectedTimeColor { get; } = "#f8e6cb";
        public string TimeColor { get; } = "#f8d49b";
        public string RunningColor { get; } = "#62c4c3";
        public string NotRunningColor { get; } = "#5193b3";
    }

    public class WisteriaColorScheme : IStageColorScheme
    {
        public string Name { get; } = "Wisteria";
        public string ExpectedTimeColor { get; } = "#fbf5aa";
        public string TimeColor { get; } = "#aec9bc";
        public string RunningColor { get; } = "#d3c5e5";
        public string NotRunningColor { get; } = "#735da5";
    }

    public class SkyColorScheme : IStageColorScheme
    {
        public string Name { get; } = "Sky";
        public string ExpectedTimeColor { get; } = "#ECF8BA";
        public string TimeColor { get; } = "#77EFBD";
        public string RunningColor { get; } = "#5ADFDF";
        public string NotRunningColor { get; } = "#59C4EB";
    }
}
