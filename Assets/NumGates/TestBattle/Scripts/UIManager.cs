using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NumGates.TestBattle
{
    public class UIManager : MonoBehaviour
    {
        // TODO: Create object pooling function

        [SerializeField] private List<FloatingTextUI> floatingTextUIs;
        [SerializeField] private FloatingTextUI floatingTextUIPrefab;

        public FloatingTextUI GetFloatingTextUI()
        {
            FloatingTextUI floatingText = null;

            return floatingText;
        }

        //public void SpawnFloatingText()
        //{
        //    FloatingTextUI floatingText = Instantiate(floatingTextUI);
        //    floatingText.StartFloating();
        //}
    }
}

