using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using NumGates.TestBattle;

namespace NumGates
{
    [CreateAssetMenu(menuName = "Scriptable/Ally/AllyInfo", order = 0, fileName = "AllyInfo")]
    public class AllyInfo : ScriptableObject
    {
        public string allyName;
        public string allyStory;
        //public SoulColor soulColor;
        //public SoulStyle primaryStyle;
        //public SoulStyle secodaryStyle;
        public Sprite iconSprite;
        public Sprite halfBodySprite;
        public Sprite fullBodySprite;
        public StyleStats styleStats;
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

    [System.Serializable]
    public class StyleStats
    {
        public SecondaryStatus baseStats;
        public SecondaryStatus attackerStyle;
        public SecondaryStatus defenderStyle;
        public SecondaryStatus energeticStyle;
        public SecondaryStatus beasterStyle;
        public SecondaryStatus souleaverStyle;
    }
}

