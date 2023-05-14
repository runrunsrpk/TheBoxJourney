using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class Ally : Character
    {
        protected override void InitStatus()
        {
            base.InitStatus();

            characterStatus = new Status();
            characterStatus.primaryStatus = characterPrimaryStatus;
            characterStatus.secondaryStatus = new SecondaryStatus(characterStatus.primaryStatus);
            characterSecondaryStatus = characterStatus.secondaryStatus;

            characterCurrentPrimaryStatus = characterStatus.primaryStatus;
            characterCurrentSecondaryStatus = characterStatus.secondaryStatus;
        }

        protected override IEnumerator OnDead()
        {
            // TODO: Call action to battle manager when character die (switch position)

            StopTimer();

            yield return new WaitForSecondsRealtime(1f);

            ResetTimer();
            RemoveTimerAction();

            yield return new WaitForSecondsRealtime(1f);

            DestroyCharacter();
        }

        protected override void DestroyCharacter()
        {

        }
    }
}

