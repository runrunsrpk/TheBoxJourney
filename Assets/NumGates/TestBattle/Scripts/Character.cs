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
        [SerializeField] protected PrimaryStatus characterCurrentPrimaryStatus;
        [SerializeField] protected SecondaryStatus characterCurrentSecondaryStatus;

        [Header("Battle Script")]
        [SerializeField] protected BattleGroup targetGroup;
        [SerializeField] protected List<int> targets;

        private UITimerGauge uiTimerGauge;
        private UIHealthGauge uiHealthGauge;

        private TimerManager timerManager;
        private BattleManager battleManager;

        private void Awake()
        {

        }

        private void Start()
        {
            //InitCharacter();
        }

        public void InitCharacter(TimerManager timerManager, BattleManager battleManager)
        {
            this.timerManager = timerManager;
            this.battleManager = battleManager;

            InitTimerAction();
            InitStatus();
            InitUIs();
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

        protected virtual void InitUIs()
        {
            uiTimerGauge = UITimerGauge.Create(transform.position, new Vector3(0.0f, -1.2f, 0.0f), 48, 24, characterSecondaryStatus.timer, transform);
            uiHealthGauge = UIHealthGauge.Create(transform.position, new Vector3(0.0f, -0.8f, 0.0f), 48, 24, characterSecondaryStatus.health, transform);
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
            //Debug.Log($"Update [{this.name}] Timer ");

            if (isTimerUpdate)
            {
                tick++;
                uiTimerGauge.UpdateTimer(tick);
                //Debug.Log($"[{this.name}] Tick: {tick}");

                if (tick >= characterStatus.secondaryStatus.timer)
                {
                    uiTimerGauge.UpdateTimer(0);
                    StartCoroutine(OnAttack());
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
            uiTimerGauge.UpdateTimer(tick);
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

        // TODO: Move to each children class

        #region Character Action

        public void Hit(int damage)
        {
            StartCoroutine(OnHit(damage));
        }

        private IEnumerator OnAttack()
        {
            StopTimer();

            Debug.LogWarning($"[{this.name}] Attack");

            battleManager.NormalAttackOnTarget(targetGroup, targets, Mathf.RoundToInt(characterSecondaryStatus.physicalAttack));

            yield return new WaitForSecondsRealtime(1f);

            ResetTimer();
            StartTimer();
        }

        private IEnumerator OnHit(int damage)
        {
            StopTimer();

            UIFloatingText.Create($"{damage}", transform.position, Vector3.up, 0.5f, 2f, 1f, Color.white);

            // TODO: Change to use action
            uiHealthGauge.UpdateHealth(damage);

            if(characterCurrentSecondaryStatus.health - damage > 0)
            {
                characterCurrentSecondaryStatus.health -= damage;

                yield return new WaitForSecondsRealtime(1f);

                StartTimer();
            }
            else
            {
                characterCurrentSecondaryStatus.health = 0;

                yield return OnDead();
            }
        }

        private IEnumerator OnDead()
        {
            // TODO: Call action to battle manager when character die (switch position)

            yield return new WaitForSecondsRealtime(1f);

            ResetTimer();
            RemoveTimerAction();

            yield return new WaitForSecondsRealtime(1f);

            Destroy(gameObject);
        }

        #endregion
    }
}


