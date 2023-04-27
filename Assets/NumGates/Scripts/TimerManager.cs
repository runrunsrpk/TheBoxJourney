using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates
{
    public class TimerManager : MonoBehaviour, ITimer
    {
        public Action OnInitTimer;
        public Action OnStartTimer;
        public Action OnUpdateTimer;
        public Action OnPauseTimer;
        public Action OnResumeTimer;
        public Action OnStopTimer;
        public Action OnResetTimer;

        private const float TICK_TIMER_MAX = 0.1f;

        private int tick;
        private float tickTimer;

        private void Start()
        {
            InitTimer();
            StartTimer();
        }

        private void Update()
        {
            UpdateTimer();
        }

        #region ITimer
        private bool isTimerUpdate;

        public void InitTimer()
        {
            Debug.Log($"Init Timer");
            OnInitTimer?.Invoke();
        }

        public void StartTimer()
        {
            Debug.Log($"Start Timer");
            isTimerUpdate = true;
            OnStartTimer?.Invoke();
        }

        public void UpdateTimer()
        {
            if (isTimerUpdate)
            {
                tickTimer += Time.deltaTime;

                if (tickTimer >= TICK_TIMER_MAX)
                {
                    tickTimer -= TICK_TIMER_MAX;
                    tick++;

                    OnUpdateTimer?.Invoke();

                    //Debug.Log($"TimerHelper Tick: {tick}");
                }
            }
        }

        public void PauseTimer()
        {
            Debug.Log($"Pause Timer");
            isTimerUpdate = false;
            OnPauseTimer?.Invoke();
        }

        public void ResumeTimer()
        {
            Debug.Log($"Resume Timer");
            isTimerUpdate = true;
            OnResumeTimer?.Invoke();
        }

        public void StopTimer()
        {
            Debug.Log($"Stop Timer");
            isTimerUpdate = false;
            OnStopTimer?.Invoke();
        }

        public void ResetTimer()
        {
            Debug.Log($"Reset Timer");
            tick = 0;
            tickTimer = 0;
            OnResetTimer?.Invoke();
        }
        #endregion
    }
}


