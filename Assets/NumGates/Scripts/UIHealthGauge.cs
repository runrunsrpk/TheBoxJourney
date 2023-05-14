using System.Collections;
using System.Collections.Generic;
using NumGates.TestBattle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace NumGates
{
    public class UIHealthGauge : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI healthText;

        private Vector3 offset;

        private float smoothTime = 0.3f;
        private float velocity = 0.0f;

        private float maxHealth;
        private float currentHealth;
        private float targetHealth;

        // Create UITimerGauge with customize setup
        public static UIHealthGauge Create(Vector3 origin, Vector3 offset, float width, float height, float maxHealth, Transform parent)
        {
            UIHealthGauge healthGauge = Instantiate(AssetManager.instance.GetUIHealthGauge(), parent);

            healthGauge.InitHealthGauge(origin, offset, width, height, maxHealth);

            return healthGauge;
        }

        private void InitHealthGauge(Vector3 origin, Vector3 offset, float width, float height, float maxHealth)
        {
            this.offset = offset;

            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            targetHealth = 1f;

            slider.transform.position = Camera.main.WorldToScreenPoint(origin + offset);
            slider.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            slider.value = 1f;

            healthText.transform.position = Camera.main.WorldToScreenPoint(origin + offset);
            healthText.text = $"{maxHealth}";
        }

        public void UpdatePosition(Vector3 position, float duration)
        {
            // TODO: Change duration to move speed
            slider.transform.DOMove(Camera.main.WorldToScreenPoint(position + offset), duration);
            healthText.transform.DOMove(Camera.main.WorldToScreenPoint(position + offset), duration);
        }

        public void UpdateHealth(float damage)
        {
            currentHealth = (currentHealth - damage > 0) ? currentHealth - damage : 0;
            targetHealth = currentHealth / maxHealth;

            Debug.Log($"Damage: {damage} / Health: {currentHealth} / Current: {slider.value} / Target: {targetHealth}");

            healthText.text = $"{currentHealth}";
        }

        private void Update()
        {
            float currentHealthValue = Mathf.SmoothDamp(slider.value, targetHealth, ref velocity, smoothTime);
            slider.value = currentHealthValue;
        }
    }
}

