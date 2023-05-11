using System.Collections;
using System.Collections.Generic;
using NumGates.TestBattle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates
{
    public class UIHealthGauge : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI healthText;

        private float smoothTime = 0.3f;
        private float velocity = 0.0f;

        private float maxHealth;
        private float currentHealth;
        private float targetHealth;

        // Create UITimerGauge with customize setup
        public static UIHealthGauge Create(Vector3 origin, Vector3 offset, float width, float height, float maxHealth)
        {
            UIHealthGauge healthGauge = Instantiate(AssetManager.instance.GetUIHealthGauge());

            healthGauge.InitHealthGauge(origin, offset, width, height, maxHealth);

            return healthGauge;
        }

        private void InitHealthGauge(Vector3 origin, Vector3 offset, float width, float height, float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            targetHealth = 1f;

            slider.transform.position = Camera.main.WorldToScreenPoint(origin + offset);
            slider.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            slider.value = 1f;

            healthText.transform.position = Camera.main.WorldToScreenPoint(origin + offset);
            healthText.text = $"{maxHealth}/{maxHealth}";
        }

        public void UpdateHealth(float damage)
        {
            currentHealth -= damage;
            targetHealth = currentHealth / maxHealth;

            Debug.Log($"Current: {slider.value} / Target: {targetHealth}");

            healthText.text = $"{currentHealth}/{maxHealth}";
        }

        private void Update()
        {
            float currentHealthValue = Mathf.SmoothDamp(slider.value, targetHealth, ref velocity, smoothTime);
            slider.value = currentHealthValue;
        }
    }
}

