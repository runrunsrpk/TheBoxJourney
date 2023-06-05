using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumGates.TestBattle;

namespace NumGates
{
    [CreateAssetMenu(menuName = "Scriptable/Enemy/EnemyInfo", order = 0, fileName = "EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        public EnemyCharacter character;
        public string enemyName;
        public string enemyStory;
        public Sprite iconSprite;
        public Sprite halfBodySprite;
        public Sprite fullBodySprite;
    }
}

