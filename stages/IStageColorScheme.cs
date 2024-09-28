using System;

namespace StagesView
{
    public interface IStageColorScheme
    {
        string Name { get; }
        string ExpectedTimeColor { get; }
        string TimeColor { get; }
        string RunningColor { get; }
        string NotRunningColor { get; }
    }
}
