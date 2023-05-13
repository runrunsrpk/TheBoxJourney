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
        private GameObject allyParent;
        private GameObject enemyParent;

        private List<Ally> allies;
        private List<Enemy> enemies;

        private List<GameObject> alliesObject;

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
        }

        #region Character Management

        // Bypass InitCharacter from LevelManager
        public void InitCharacter(List<Ally> allies, List<Enemy> enemies)
        {
            this.enemies = enemies;
            Debug.Log($"Enemy count: {this.enemies.Count}");
            int enemyIndex = 0;

            foreach (Enemy enemy in this.enemies)
            {
                enemyIndex++;
                Debug.Log($"{enemy.name} Index {enemyIndex}");
                Enemy tempEnemy = Instantiate(enemy);
                tempEnemy.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(enemyIndex, this.enemies.Count, BattleGroup.Enemy));
                tempEnemy.InitCharacter(timerManager, this);
            }

            this.allies = allies;
            int allyIndex = 0;

            foreach (Ally ally in this.allies)
            {
                allyIndex++;
                Ally tempAlly = Instantiate(ally);
                tempAlly.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(allyIndex, this.allies.Count, BattleGroup.Ally));
                tempAlly.SetFlipX(true);
                tempAlly.InitCharacter(timerManager, this);
                tempAlly.InitCharacter(timerManager, this);
            }
        }

        // Bypass InitCharacter from LevelManager 2
        public void InitCharacter(List<CharacterAlly> allies, List<CharacterEnemy> enemies)
        {
            int enemyIndex = 0;
            this.enemies = new List<Enemy>();

            foreach (CharacterEnemy enemy in enemies)
            {
                enemyIndex++;

                Enemy spawnedEnemy = Instantiate(AssetManager.instance.GetEnemyCharacter(enemy).GetComponent<Enemy>(), enemyParent.transform);
                spawnedEnemy.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(enemyIndex, this.enemies.Count, BattleGroup.Enemy));
                spawnedEnemy.InitCharacter(timerManager, this);

                this.enemies.Add(spawnedEnemy);
            }

            int allyIndex = 0;
            this.allies = new List<Ally>();

            foreach (CharacterAlly ally in allies)
            {
                allyIndex++;

                Ally spawnedAlly = Instantiate(AssetManager.instance.GetAllyCharacter(ally).GetComponent<Ally>(), allyParent.transform);
                spawnedAlly.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(allyIndex, this.allies.Count, BattleGroup.Ally));
                spawnedAlly.SetFlipX(true);
                spawnedAlly.InitCharacter(timerManager, this);

                this.allies.Add(spawnedAlly);
            }
        }

        public void InitCharacter()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.InitCharacter(timerManager, this);
            }

            foreach (Ally ally in allies)
            {
                ally.InitCharacter(timerManager, this);
            }
        }

        public void AddCharacter(BattleGroup group, Character character)
        {
            switch (group)
            {
                case BattleGroup.Ally:
                    allies.Add(character as Ally);
                    break;
                case BattleGroup.Enemy:
                    enemies.Add(character as Enemy);
                    break;
            }
        }

        public void AddCharacter(BattleGroup group, Character character, int position)
        {
            switch (group)
            {
                case BattleGroup.Ally:
                    allies.Insert(position, character as Ally);
                    break;
                case BattleGroup.Enemy:
                    enemies.Insert(position, character as Enemy);
                    break;
            }
        }

        public void RemoveCharacter(BattleGroup group, int position)
        {
            switch (group)
            {
                case BattleGroup.Ally:
                    {
                        DestroyCharacter(allyParent.transform, position);
                        allies.RemoveAt(position);
                        break;
                    }
                    
                case BattleGroup.Enemy:
                    {
                        DestroyCharacter(enemyParent.transform, position);
                        enemies.RemoveAt(position);
                        break;
                    }
                    
            }
        }

        public void RemoveAllCharacters(BattleGroup group)
        {
            switch (group)
            {
                case BattleGroup.Ally:
                    {
                        DestroyAllCharacters(allyParent.transform);
                        allies.Clear();
                        break;
                    }

                case BattleGroup.Enemy:
                    {
                        DestroyAllCharacters(enemyParent.transform);
                        enemies.Clear();
                        break;
                    }

            }
        }

        private void DestroyCharacter(Transform parent, int position)
        {
            Destroy(parent.GetChild(position - 1));
        }

        private void DestroyAllCharacters(Transform parent)
        {
            foreach(Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        //public void SwitchCharacter(BattleGroup group, int position1, int position2)
        //{
        //    switch (group)
        //    {
        //        case BattleGroup.Ally: break;
        //        case BattleGroup.Enemy: break;
        //    }
        //}
        #endregion

        #region Character Action

        public void NormalAttackOnTarget(BattleGroup group, List<int> targets, int damage)
        {
            switch (group)
            {
                case BattleGroup.Ally:
                    {
                        foreach(int target in targets)
                        {
                            allies[target].Hit(damage);
                        }

                        break;
                    }

                case BattleGroup.Enemy:
                    {
                        foreach (int target in targets)
                        {
                            enemies[target].Hit(damage);
                        }

                        break;
                    }
            }
        }

        #endregion
    }
}

