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
        public AllyCharacter character;
        public string allyName;
        public string allyStory;
        public Sprite iconSprite;
        public Sprite halfBodySprite;
        public Sprite fullBodySprite;
        public StyleStats styleStats;
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

