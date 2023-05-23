using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public interface ITimer
    {
        public void InitTimer();
        public void StartTimer();
        public void UpdateTimer();
        public void PauseTimer();
        public void ResumeTimer();
        public void StopTimer();
        public void ResetTimer();
    }
}

