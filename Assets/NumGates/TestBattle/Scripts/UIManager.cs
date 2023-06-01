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
                    uiAllyManagement.Hide();
                }

                return uiAllyManagement;
            }
        }
        private UIAllyManagement uiAllyManagement;

        public void InitManager()
        {
            if (uiBattleEvent == null)
            {
                GameObject uiTemp = Instantiate(AssetManager.instance.GetUI(UIReference.UIBattleEvent));
                uiBattleEvent = uiTemp.GetComponent<UIBattleEvent>();
                uiBattleEvent.InitUIBattle();
            }
        }

        // TODO: Create object pooling function
    }
}

