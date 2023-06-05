using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NumGates.TestBattle
{
    public class UIManager : MonoBehaviour
    {
        private UIBattleEvent uiBattleEvent;

        public UIAllyManagement UIAllyManagement
        {
            get
            {
                if (uiAllyManagement == null)
                {
                    GameObject uiTemp = Instantiate(AssetManager.instance.GetUI(UIReference.UIAllyManagement));
                    uiAllyManagement = uiTemp.GetComponent<UIAllyManagement>();
                    uiAllyManagement.InitUI();
                    uiAllyManagement.Hide();
                }

                return uiAllyManagement;
            }
        }
        private UIAllyManagement uiAllyManagement;

        public UIEnemyManagement UIEnemyManagement
        {
            get
            {
                if (uiEnemyManagement == null)
                {
                    GameObject uiTemp = Instantiate(AssetManager.instance.GetUI(UIReference.UIEnemyManagement));
                    uiEnemyManagement = uiTemp.GetComponent<UIEnemyManagement>();
                    uiEnemyManagement.InitUI();
                    uiEnemyManagement.Hide();
                }

                return uiEnemyManagement;
            }
        }
        private UIEnemyManagement uiEnemyManagement;

        public void InitManager()
        {
            if (uiBattleEvent == null)
            {
                GameObject uiTemp = Instantiate(AssetManager.instance.GetUI(UIReference.UIBattleEvent));
                uiBattleEvent = uiTemp.GetComponent<UIBattleEvent>();
                uiBattleEvent.InitUI();
            }
        }

        // TODO: Create object pooling function
    }
}

