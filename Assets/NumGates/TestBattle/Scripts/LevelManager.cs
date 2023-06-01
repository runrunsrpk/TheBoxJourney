using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public enum LevelEvent
    {
        Battle,
        Trap
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<CharacterAlly> characterAllies;
        [SerializeField] private List<CharacterEnemy> characterEnemies;

        [SerializeField] private TimerManager timerManagerPrefab;
        [SerializeField] private BattleManager battleManagerPrefab;

        private TimerManager timerManager;
        private BattleManager battleManager;

        public void InitManager()
        {
            if(timerManager == null)
            {
                timerManager = Instantiate(timerManagerPrefab, transform);
                timerManager.InitTimer();
            }

            if(battleManager == null)
            {
                battleManager = Instantiate(battleManagerPrefab, transform);
                battleManager.InitBattle(timerManager);
                battleManager.InitCharacter(characterAllies, characterEnemies);
            }
        }

        public void StartEvent(LevelEvent levelEvent)
        {
            switch(levelEvent)
            {
                case LevelEvent.Battle:
                    {
                        timerManager.StartTimer();
                        break;
                    }
            }
        }

        public void StartTestBattle()
        {
            timerManager.StartTimer();
        }

        public void StopTestBattle()
        {
            timerManager.StopTimer();
            timerManager.ResetTimer();
        }

        public void ResetTestBattle()
        {
            battleManager.RemoveAllCharacters(BattleGroup.Ally);
            battleManager.DestroyAllCharacters(BattleGroup.Ally);

            battleManager.RemoveAllCharacters(BattleGroup.Enemy);
            battleManager.DestroyAllCharacters(BattleGroup.Enemy);
        }

        public void DestroyManager()
        {
            Destroy(timerManager.gameObject);
            Destroy(battleManager.gameObject);
        }
    }
}

