using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NumGates.TestBattle
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance { get; private set; }

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

        // TODO: Create object pooling function

        [SerializeField] private FloatingTextUI floatingTextUIPrefab;

        public FloatingTextUI GetFloatingTextUI()
        {
            return floatingTextUIPrefab;
        }

        //public void SpawnFloatingText()
        //{
        //    FloatingTextUI floatingText = Instantiate(floatingTextUI);
        //    floatingText.StartFloating();
        //}
    }
}

