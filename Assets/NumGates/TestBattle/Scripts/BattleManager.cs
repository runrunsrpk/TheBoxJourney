using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public enum BattleGroup
    {
        Ally,
        Enemy
    }

    public class BattleManager : MonoBehaviour
    {
        public Action<BattleGroup> OnChangeAllCharacterPositions;

        private GameObject allyParent;
        private GameObject enemyParent;

        private List<Character> allies;
        private List<Character> enemies;

        private TimerManager timerManager;

        public void InitBattle(TimerManager timerManager)
        {
            this.timerManager = timerManager;

            if(allyParent == null)
            {
                allyParent = new GameObject("AllyParent");
                allyParent.transform.parent = transform;
            }
            
            if(enemyParent == null)
            {
                enemyParent = new GameObject("EnemyParent");
                enemyParent.transform.parent = transform;
            }

            InitAction();
        }

        private void InitAction()
        {
            OnChangeAllCharacterPositions += ChangeAllCharacterPositions;
        }

        private void RemoveAction()
        {
            OnChangeAllCharacterPositions -= ChangeAllCharacterPositions;
        }

        #region Character Management

        // Bypass InitCharacter from LevelManager
        public void InitCharacter(List<CharacterAlly> allies, List<CharacterEnemy> enemies)
        {
            int enemyIndex = 0;
            this.enemies = new List<Character>();

            foreach (CharacterEnemy enemy in enemies)
            {
                enemyIndex++;

                Enemy spawnedEnemy = Instantiate(AssetManager.instance.GetEnemyCharacter(enemy).GetComponent<Enemy>(), enemyParent.transform);
                spawnedEnemy.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(enemyIndex, enemies.Count, BattleGroup.Enemy));
                spawnedEnemy.InitCharacter(timerManager, this);

                this.enemies.Add(spawnedEnemy);
            }

            int allyIndex = 0;
            this.allies = new List<Character>();

            foreach (CharacterAlly ally in allies)
            {
                allyIndex++;

                Ally spawnedAlly = Instantiate(AssetManager.instance.GetAllyCharacter(ally).GetComponent<Ally>(), allyParent.transform);
                spawnedAlly.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(allyIndex, allies.Count, BattleGroup.Ally));
                spawnedAlly.SetFlipX(true);
                spawnedAlly.InitCharacter(timerManager, this);

                this.allies.Add(spawnedAlly);
            }
        }

        public void InitCharacterTimer()
        {
            foreach (Ally ally in allies)
            {
                ally.InitTimer(timerManager);
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.InitTimer(timerManager);
            }
        }

        // InitAllyCharacter from ally management setting
        public void InitAllyCharacter(List<AllyData> allies)
        {
            int allyIndex = 0;
            this.allies = new List<Character>();

            foreach (AllyData ally in allies)
            {
                allyIndex++;

                if(ally.info.character != CharacterAlly.EmptyAlly)
                {
                    Ally spawnedAlly = Instantiate(AssetManager.instance.GetAllyCharacter(ally.info.character).GetComponent<Ally>(), allyParent.transform);
                    spawnedAlly.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(allyIndex, allies.Count, BattleGroup.Ally));
                    spawnedAlly.SetFlipX(true);
                    spawnedAlly.InitCharacter(this);

                    this.allies.Add(spawnedAlly);
                }
            }
        }

        // InitEnemyCharacter from enemy management setting
        public void InitEnemyCharacter(List<EnemyData> enemies)
        {
            int enemyIndex = 0;
            this.enemies = new List<Character>();

            foreach (EnemyData enemy in enemies)
            {
                enemyIndex++;

                Enemy spawnedEnemy = Instantiate(AssetManager.instance.GetEnemyCharacter(enemy.character).GetComponent<Enemy>(), enemyParent.transform);
                spawnedEnemy.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(enemyIndex, enemies.Count, BattleGroup.Enemy));
                spawnedEnemy.InitCharacter(this);

                this.enemies.Add(spawnedEnemy);
            }
        }


        public void AddCharacter(BattleGroup group, Character character, int positionIndex)
        {
            GetCharacterGroupList(group).Insert(positionIndex, character);
        }

        public void RemoveCharacter(BattleGroup group, int positionIndex)
        {
            GetCharacterGroupList(group).RemoveAt(positionIndex);
        }

        public void RemoveAllCharacters(BattleGroup group)
        {
            GetCharacterGroupList(group).Clear();
        }

        public void DestroyCharacter(BattleGroup group, int positionIndex)
        {
            Transform parent = GetCharacterGroupParent(group);

            Destroy(parent.GetChild(positionIndex - 1));
        }

        public void DestroyAllCharacters(BattleGroup group)
        {
            Transform parent = GetCharacterGroupParent(group);

            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        private void ChangeCharacterPosition(BattleGroup group)
        {

        }

        private void ChangeAllCharacterPositions(BattleGroup group)
        {
            List<Character> characters = GetCharacterGroupList(group);

            int positionIndex = 0;

            foreach(Character character in characters)
            {
                positionIndex++;
                Vector3 targetPosition = TheBoxCalculator.GetCharacterPositionFrontPivot(positionIndex, characters.Count, group);
                character.SetMovePosition(targetPosition, 1f);
            }
        }

        private int GetNearestCharacterPositionIndex(BattleGroup group, int position)
        {
            List<Character> characters = GetCharacterGroupList(group);

            for(int nearestPosition = position + 1; nearestPosition < characters.Count; nearestPosition++)
            {
                if (!characters[nearestPosition].IsDead())
                {
                    return nearestPosition;
                }
            }

            return 0;
        }

        private List<Character> GetCharacterGroupList(BattleGroup group)
        {
            return group == BattleGroup.Ally ? allies : enemies;
        }

        private Transform GetCharacterGroupParent(BattleGroup group)
        {
            return group == BattleGroup.Ally ? allyParent.transform : enemyParent.transform;
        }

        private bool IsAllCharactersDead(BattleGroup group)
        {
            List<Character> characters = GetCharacterGroupList(group);

            foreach (Character character in characters)
            {
                if (!character.IsDead())
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Character Action

        private void CharacterDead(BattleGroup group)
        {
            if (IsAllCharactersDead(group))
            {
                Debug.LogWarning($"Game End");
                timerManager.StopTimer();
            }
        }

        public void NormalAttackOnTarget(BattleGroup group, List<int> targets, int damage)
        {
            List<Character> characters = GetCharacterGroupList(group);

            foreach (int target in targets)
            {
                Character character = characters[target];

                // TODO: Stop game from calling hit
                if(!IsAllCharactersDead(group))
                {
                    if (character.IsDead())
                    {
                        if (group == BattleGroup.Ally)
                        {
                            character = characters[GetNearestCharacterPositionIndex(group, target)];
                        }
                        else
                        {
                            RemoveCharacter(group, target);
                            character = characters[target];
                        }
                    }

                    character.Hit(damage);

                    // Recheck is charater dead after hit
                    if (character.IsDead())
                    {
                        RemoveCharacter(group, target);
                        CharacterDead(group);
                    }
                }
            }
        }

        public void SkillAttackOnTarget(BattleGroup group, List<int> targets)
        {

        }

        #endregion
    }
}

