using System;
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

    public struct AllyData
    {
        public AllyInfo info;
        public AllyStats stats;

        public AllyData(AllyInfo info, AllyStats stats)
        {
            this.info = info;
            this.stats = stats;
        }
    }

    public struct EnemyData
    {
        public CharacterEnemy character;
    }

    public class LevelManager : MonoBehaviour
    {
        public Action<bool> OnBattleReady;

        [SerializeField] private List<AllyCharacter> characterAllies;
        [SerializeField] private List<CharacterEnemy> characterEnemies;

        [SerializeField] private TimerManager timerManagerPrefab;
        [SerializeField] private BattleManager battleManagerPrefab;

        private List<AllyData> allyDatas = new List<AllyData>();
        private List<EnemyData> enemyDatas = new List<EnemyData>();

        private TimerManager timerManager;
        private BattleManager battleManager;

        public void InitManager()
        {
            timerManager = Instantiate(timerManagerPrefab, transform);
            timerManager.InitTimer();

            battleManager = Instantiate(battleManagerPrefab, transform);
            battleManager.InitBattle(timerManager);
            //battleManager.InitCharacter(characterAllies, characterEnemies);
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

        #region Character Data
        private bool IsCharacterReady()
        {
            return allyDatas.Count > 0 && enemyDatas.Count > 0;
        }

        private void CheckBattleReady()
        {
            if (IsCharacterReady())
            {
                battleManager.InitCharacterTimer();
            }

            OnBattleReady?.Invoke(IsCharacterReady());
        }
        #endregion

        #region Ally Data

        public void InitAllyCharacter(List<AllyData> allyDatas)
        {
            this.allyDatas = allyDatas;

            battleManager.InitAllyCharacter(allyDatas);

            CheckBattleReady();
        }

        public List<AllyData> GetAllyDatas()
        {
            return allyDatas;
        }

        public void ResetAllyDatas()
        {
            allyDatas.Clear();
        }
        #endregion

        #region Enemy Data

        public void InitEnemyCharacter(List<EnemyData> enemyDatas)
        {
            this.enemyDatas = enemyDatas;

            battleManager.InitEnemyCharacter(enemyDatas);

            CheckBattleReady();
        }

        #endregion
    }
}

