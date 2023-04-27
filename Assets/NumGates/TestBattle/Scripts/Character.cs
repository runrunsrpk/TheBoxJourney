using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class Character : MonoBehaviour, ITimer
    {
        [Header("Character Script")]
        protected Status characterStatus;

        // For set and show value in inspector //
        [SerializeField] protected PrimaryStatus characterPrimaryStatus;
        [SerializeField] protected SecondaryStatus characterSecondaryStatus;

        private TimerManager timerManager;

        private void Awake()
        {

        }

        private void Start()
        {
            //InitCharacter();
        }

        public void InitCharacter(TimerManager timerManager)
        {
            this.timerManager = timerManager;

            InitTimerAction();
            InitStatus();
        }

        private void InitTimerAction()
        {
            timerManager.OnInitTimer += InitTimer;
            timerManager.OnStartTimer += StartTimer;
            timerManager.OnUpdateTimer += UpdateTimer;
            timerManager.OnPauseTimer += PauseTimer;
            timerManager.OnResumeTimer += ResumeTimer;
            timerManager.OnStopTimer += StopTimer;
            timerManager.OnResetTimer += ResetTimer;
        }

        private void RemoveTimerAction()
        {
            timerManager.OnInitTimer -= InitTimer;
            timerManager.OnStartTimer -= StartTimer;
            timerManager.OnUpdateTimer -= UpdateTimer;
            timerManager.OnPauseTimer -= PauseTimer;
            timerManager.OnResumeTimer -= ResumeTimer;
            timerManager.OnStopTimer -= StopTimer;
            timerManager.OnResetTimer -= ResetTimer;
        }

        protected virtual void InitStatus()
        {
            //Debug.LogWarning($"Primary " +
            //    $"STR[{characterStatus.primaryStatus.STR}], " +
            //    $"AGI[{characterStatus.primaryStatus.AGI}], " +
            //    $"VIT[{characterStatus.primaryStatus.VIT}], " +
            //    $"DEX[{characterStatus.primaryStatus.DEX}], " +
            //    $"INT[{characterStatus.primaryStatus.INT}], " +
            //    $"WIS[{characterStatus.primaryStatus.WIS}], " +
            //    $"CHR[{characterStatus.primaryStatus.CHR}], " +
            //    $"LUK[{characterStatus.primaryStatus.LUK}], ");

            //Debug.LogWarning($"Secondary " +
            //    $"P.Atk[{characterStatus.secondaryStatus.basePhysicalAttack}], " +
            //    $"Speed[{characterStatus.secondaryStatus.baseSpeed}], " +
            //    $"Dodge[{characterStatus.secondaryStatus.baseDodge}], " +
            //    $"Acc[{characterStatus.secondaryStatus.baseAccuracy}], ");
        }

        #region ITimer
        private bool isTimerUpdate;
        private int tick;

        public void InitTimer()
        {
            Debug.Log($"Init [{this.name}] Timer ");
        }

        public void StartTimer()
        {
            Debug.Log($"Start [{this.name}] Timer ");

            isTimerUpdate = true;
        }

        public void UpdateTimer()
        {
            Debug.Log($"Update [{this.name}] Timer ");

            if (isTimerUpdate)
            {
                tick++;
                Debug.Log($"[{this.name}] Tick: {tick}");

                if (tick >= characterStatus.secondaryStatus.timer)
                {
                    StartCoroutine(Attack());
                }
            }
        }

        public void PauseTimer()
        {
            Debug.Log($"Pause [{this.name}] Timer ");
            isTimerUpdate = false;
        }

        public void ResumeTimer()
        {
            Debug.Log($"Resume [{this.name}] Timer ");
            isTimerUpdate = true;
        }

        public void StopTimer()
        {
            Debug.Log($"Stop [{this.name}] Timer ");
            isTimerUpdate = false;
        }

        public void ResetTimer()
        {
            Debug.Log($"Reset [{this.name}] Timer ");
            tick = 0;
        }
        #endregion

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetFlipX(bool isFlip)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = isFlip;
        }

        private IEnumerator Attack()
        {
            Debug.LogWarning($"[{this.name}] Attack");

            StopTimer();

            yield return new WaitForSecondsRealtime(1f);

            ResetTimer();
            StartTimer();
        }
    }
}


