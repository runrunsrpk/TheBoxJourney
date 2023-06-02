using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public struct AllyStats
    {
        public int level;
        public int soulPoint;
        public int rank;
        public int pureSoulPoint;
        public int statusPoint;
        public PrimaryStatus primaryStatus;
        public SecondaryStatus secondaryStatus;
        public SoulColor soulColor;
        public SoulStyle primaryStyle;
        public SoulStyle secodaryStyle;
        public GameObject firstSoulbox;
        public GameObject secondSoulbox;
        public GameObject thirdSoulbox;
        public AllyGrowth allyGrowth;
    }

    public enum SoulColor
    {
        Red,
        Orange,
        Yellow,
        Green,
        Turquoise,
        Navy,
        Violet,
        Pink,
        Black,
        White
    }

    public enum SoulStyle
    {
        Attacker,
        Defender,
        Energetic,
        Beaster,
        Souleaver
    }

    public enum GrowthPath
    {
        Initail,
        First,
        FirstUpper,
        FirstLower,
        Second,
        SecondUpper,
        SecondLower
    }

    [System.Serializable]
    public struct AllyGrowth
    {
        public GrowthPath soulGrowth;
        public GrowthPath primaryStyleGrowth;
        public GrowthPath soulkeyGrowth;
        public GrowthPath secondaryStyleGrowth;
        public GrowthPath uniqueGrowth;
    }
}

