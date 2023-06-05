using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UIBattleEvent : MonoBehaviour
    {
        public Action OnStartBattle;
        public Action OnStopBattle;
        public Action OnResetBattle;

        [Header("Battle Setup")]
        [SerializeField] private Button allyButton;
        [SerializeField] private Button soulboxButton;
        [SerializeField] private Button enemyButton;
        [SerializeField] private Button optionButton;

        [Header("Battle Controller")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button clearButton;

        private LevelManager levelManager;
        private UIManager uiManager;

        private void Awake()
        {
            allyButton.onClick.AddListener(OnClickAlly);

            startButton.onClick.AddListener(OnClickStart);
            stopButton.onClick.AddListener(OnClickStop);
            resetButton.onClick.AddListener(OnClickReset);
        }

        private void Start()
        {
            startButton.interactable = false;
            stopButton.interactable = false;
            resetButton.interactable = false;
        }

        private void OnDestroy()
        {
            allyButton.onClick.RemoveListener(OnClickAlly);

            startButton.onClick.RemoveListener(OnClickStart);
            stopButton.onClick.RemoveListener(OnClickStop);
            resetButton.onClick.RemoveListener(OnClickReset);

            levelManager.OnBattleReady -= BattleReady;
        }

        public void InitUI()
        {
            levelManager = GameManager.instance.LevelManager;
            uiManager = GameManager.instance.UIManager;

            levelManager.OnBattleReady += BattleReady;
        }

        #region Battle Setup

        public void OnClickAlly()
        {
            Debug.Log($"Click 'ALLY' button");

            uiManager.UIAllyManagement.Show();
        }

        public void OnClickSoulbox()
        {
            Debug.Log($"Click 'SOULBOX' button");
        }

        public void OnClickEnemy()
        {
            Debug.Log($"Click 'ENEMY' button");
        }

        public void OnClickOption()
        {
            Debug.Log($"Click 'OPTION' button");
        }

        #endregion

        #region Battle Controller
        private void BattleReady(bool isReady)
        {
            startButton.interactable = isReady;
        }

        public void OnClickStart()
        {
            Debug.Log($"Click 'START' button");

            levelManager.StartTestBattle();

            startButton.interactable = false;
            stopButton.interactable = true;
            resetButton.interactable = false;
        }

        public void OnClickStop()
        {
            Debug.Log($"Click 'STOP' button");

            levelManager.StopTestBattle();

            startButton.interactable = true;
            stopButton.interactable = false;
            resetButton.interactable = true;
        }

        public void OnClickReset()
        {
            Debug.Log($"Click 'RESET' button");

            levelManager.ResetTestBattle();

            startButton.interactable = false;
            stopButton.interactable = false;
            resetButton.interactable = false;
        }

        public void OnClickClear()
        {
            // TODO: Change clear to remove all character and reset to reset battle data.
        }
        #endregion
    }
}

