using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class Enemy : Character
    {
        // Only enemy character can set status multiplier //
        // Enemy don't have base status //
        [Header("Enemy Script")]
        [SerializeField] private float statusMultiplier = 1;

        protected override void InitStatus()
        {
            base.InitStatus();

            characterStatus = new Status();
            characterStatus.primaryStatus = characterPrimaryStatus;
            characterStatus.secondaryStatus = new SecondaryStatus(characterStatus.primaryStatus, statusMultiplier);
            characterSecondaryStatus = characterStatus.secondaryStatus;

            characterCurrentPrimaryStatus = characterStatus.primaryStatus;
            characterCurrentSecondaryStatus = characterStatus.secondaryStatus;
        }

        protected override IEnumerator OnDead()
        {
            // TODO: Call action to battle manager when character die (switch position)

            StopTimer();

            yield return new WaitForSecondsRealtime(1f);

            FadeCharacter();
            ResetTimer();
            RemoveTimerAction();

            yield return new WaitForSecondsRealtime(1f);

            DestroyCharacter();
        }

        protected override void DestroyCharacter()
        {
            Destroy(gameObject);
        }
    }
}


