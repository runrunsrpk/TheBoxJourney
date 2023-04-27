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
            
        }

        private void Update()
        {
            
        }
    }
}

