using System;

namespace StagesView
{
    public class StageService
    {
        private static StageService _service = new StageService();

        public delegate void StageStateChanged(string eventLabel, StageResult result, bool isRunning);
        public event StageStateChanged StageStateChangedEvent;

        private void OnStageStateChanged(string eventLabel, StageResult result, bool isRunning)
        {
            if (StageStateChangedEvent != null)
            {
                StageStateChangedEvent(eventLabel, result, isRunning);
            }

        }

        public delegate void ResetAllDelegate();
        public event ResetAllDelegate ResetAllEvent;

        private void OnResetAll()
        {
            if (ResetAllEvent != null)
            {
                ResetAllEvent();
            }

        }

        private StageService()
        {
        }

        public static StageService Get()
        {
            if (_service == null)
            {
                _service = new StageService();
            }
            return _service;
        }

        public void UpdateStageState(string eventLabel, StageResult result, bool isRunning)
        {
            OnStageStateChanged(eventLabel, result, isRunning);
        }

        public void ResetAll()
        {
            OnResetAll();
        }
    }
}
