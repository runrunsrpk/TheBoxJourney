using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Ally> allies;
        [SerializeField] private List<Enemy> enemies;

        [SerializeField] private TimerManager timerManager;
        [SerializeField] private BattleManager battleManager;

        private TimerManager tempTimer;
        private BattleManager tempBattle;

        private void Awake()
        {

        }

        private void Start()
        {
            InitManager();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                tempTimer.StartTimer();
            }

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    InitManager();
            //}
        }

        public void InitManager()
        {
            if(tempTimer == null)
            {
                tempTimer = Instantiate(timerManager, transform);
                tempTimer.InitTimer();
            }

            if(tempBattle == null)
            {
                tempBattle = Instantiate(battleManager, transform);
                tempBattle.InitBattle(tempTimer);
                tempBattle.InitCharacter(allies, enemies);
            }
        }

        public void DestroyManager()
        {
            Destroy(tempTimer.gameObject);
            Destroy(tempBattle.gameObject);
        }
    }
}

