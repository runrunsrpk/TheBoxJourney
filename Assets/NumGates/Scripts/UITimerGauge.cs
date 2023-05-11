using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NumGates.TestBattle;
using UnityEngine.UI;

namespace NumGates
{
    public class UITimerGauge : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        //private float width;
        //private float height;
        private float maxTimer;

        // Create UITimerGauge with customize setup
        public static UITimerGauge Create(Vector3 origin, Vector3 offset, float width, float height, float maxTimer)
        {
            UITimerGauge timerGauge = Instantiate(AssetManager.instance.GetUITimerGauge());

            timerGauge.InitTimerGauge(origin, offset, width, height, maxTimer);

            return timerGauge;
        }

        private void InitTimerGauge(Vector3 origin, Vector3 offset, float width, float height, float maxTimer)
        {
            this.maxTimer = maxTimer;

            slider.transform.position = Camera.main.WorldToScreenPoint(origin + offset);
            slider.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }

        public void UpdateTimer(float timer)
        {
            slider.value = timer/maxTimer;
        }
    }

}

