using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NumGates.TestBattle
{
    public class UIManager : MonoBehaviour
    {
        // TODO: Change UIManager instance to AssetManager script
        public static UIManager instance { get; private set; }

        [SerializeField] private UIBattleEvent uiBattleEventPrefab;        
        [SerializeField] private UIFloatingText uiFloatingTextPrefab;
        [SerializeField] private UIAllyManagement uiAllyManagementPrefab;

        private UIBattleEvent uiBattleEvent;
        private UIAllyManagement uiAllyManagement;

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

        public void InitManager(LevelManager levelManager)
        {
            if(uiAllyManagement == null)
            {
                uiAllyManagement = Instantiate(uiAllyManagementPrefab);
                uiAllyManagement.Show();
            }

            if(uiBattleEvent == null)
            {
                uiBattleEvent = Instantiate(uiBattleEventPrefab);
                uiBattleEvent.InitUIBattle(levelManager);
            }
        }

        // TODO: Create object pooling function
        //public UIFloatingText GetFloatingTextUI()
        //{
        //    return uiFloatingTextPrefab;
        //}
    }
}

