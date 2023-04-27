using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UIManager uiManager;

        private LevelManager tempLevelManager;
        private UIManager tempUIManager;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }

            instance = this;
            DontDestroyOnLoad(instance);
        }

        private void Start()
        {
            InitManager();
        }

        private void Update()
        {
            
        }

        private void InitManager()
        {
            tempLevelManager = Instantiate(levelManager, transform);
            tempLevelManager.InitManager();

            tempUIManager = Instantiate(uiManager, transform);
        }
    }
}

