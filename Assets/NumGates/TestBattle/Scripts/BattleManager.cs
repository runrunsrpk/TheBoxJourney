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
        private List<Ally> allies;
        private List<Enemy> enemies;

        private TimerManager timerManager;

        public void InitBattle(TimerManager timerManager)
        {
            this.timerManager = timerManager;
        }

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
                tempEnemy.InitCharacter(timerManager);
            }

            this.allies = allies;
            int allyIndex = 0;

            foreach (Ally ally in this.allies)
            {
                allyIndex++;
                Ally tempAlly = Instantiate(ally);
                tempAlly.SetPosition(TheBoxCalculator.GetCharacterPositionFrontPivot(allyIndex, this.allies.Count, BattleGroup.Ally));
                tempAlly.SetFlipX(true);
                tempAlly.InitCharacter(timerManager);
                tempAlly.InitCharacter(timerManager);
            }
        }

        public void InitCharacter()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.InitCharacter(timerManager);
            }

            foreach (Ally ally in allies)
            {
                ally.InitCharacter(timerManager);
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
                    allies.RemoveAt(position);
                    break;
                case BattleGroup.Enemy:
                    enemies.RemoveAt(position);
                    break;
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
    }
}

