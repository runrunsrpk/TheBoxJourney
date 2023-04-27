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
        }
    }
}

