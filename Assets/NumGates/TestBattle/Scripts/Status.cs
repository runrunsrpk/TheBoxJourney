using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NumGates.TestBattle
{
    public struct Status
    {
        public PrimaryStatus primaryStatus;
        public SecondaryStatus secondaryStatus;
    }

    [System.Serializable]
    public struct PrimaryStatus
    {
        public float STR;
        public float AGI;
        public float VIT;
        public float DEX;
        public float INT;
        public float WIS;
        public float CHR;
        public float LUK;

        public PrimaryStatus(float STR = 0, float AGI = 0, float VIT = 0, float DEX = 0, float INT = 0, float WIS = 0, float CHR = 0, float LUK = 0)
        {
            this.STR = STR;
            this.AGI = AGI;
            this.VIT = VIT;
            this.DEX = DEX;
            this.INT = INT;
            this.WIS = WIS;
            this.CHR = CHR;
            this.LUK = LUK;
        }
    }

    [System.Serializable]
    public struct SecondaryStatus
    {
        [Header("Physical")]
        public float physicalAttack;
        public float physicalDefense;
        public float physicalResist;

        [Header("Action")]
        public float timer;
        public float speed;
        public float dodge;
        public float accuracy;

        [Header("Critical")]
        public float criticalDamage;
        public float criticalChance;
        public float criticalMultiplier;

        [Header("Health")]
        public float health;

        [Header("Magical")]
        public float magicalAttack;
        public float magicalDefense;
        public float magicalResist;

        [Header("Healing")]
        public float healingPower;
        public float healingEffect;

        [Header("Buff/Debuff")]
        public float buffEffect;
        public float debuffEffect;

        public SecondaryStatus(PrimaryStatus primaryStatus)
        {
            this.physicalAttack = 5f + (primaryStatus.STR * 5f);
            this.physicalDefense = 2f + (primaryStatus.VIT * 5f);
            this.physicalResist = primaryStatus.VIT * 10f;

            this.timer = 50f;
            this.speed = primaryStatus.AGI * 5f;
            this.dodge = primaryStatus.AGI * 10f;
            this.accuracy = primaryStatus.DEX * 10f;

            this.criticalDamage = 0;
            this.criticalChance = 0;
            this.criticalMultiplier = 0;

            this.health = 50f + (primaryStatus.VIT * 50f);

            this.magicalAttack = 0;
            this.magicalDefense = 0;
            this.magicalResist = 0;

            this.healingPower = 0;
            this.healingEffect = 0;

            this.buffEffect = 0;
            this.debuffEffect = 0;
        }

        public SecondaryStatus(PrimaryStatus primaryStatus, float statusMultiplier)
        {
            this.physicalAttack = primaryStatus.STR * 5f * statusMultiplier;
            this.physicalDefense = primaryStatus.VIT * 5f * statusMultiplier;
            this.physicalResist = primaryStatus.VIT * 10f;

            this.timer = 50f;
            this.speed = primaryStatus.AGI * 5f;
            this.dodge = primaryStatus.AGI * 10f;
            this.accuracy = primaryStatus.DEX * 10f;

            this.criticalDamage = 0;
            this.criticalChance = 0;
            this.criticalMultiplier = 0;

            this.health = primaryStatus.VIT * 50f * statusMultiplier;

            this.magicalAttack = 0;
            this.magicalDefense = 0;
            this.magicalResist = 0;

            this.healingPower = 0;
            this.healingEffect = 0;

            this.buffEffect = 0;
            this.debuffEffect = 0;
        }
    }
}


