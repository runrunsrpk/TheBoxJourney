using System;
using System.Collections;
using TMPro;
using UnityEngine;
using NumGates.TestBattle;
using DG.Tweening;

namespace NumGates
{
    public class FloatingTextUI : MonoBehaviour
    {
        [SerializeField] TextMeshPro textMeshPro;
        [SerializeField] float fadingTime = 1f;

        private string text;
        private Vector3 origin;
        private Vector3 direction;
        private float distance;
        private float speed;
        private float duration;
        private Color color;

        // Create FloatingText with customize setup
        public static FloatingTextUI Create(string text, Vector3 origin, Vector3 direction, float distance, float speed, float duration, Color color)
        {
            FloatingTextUI floatingText = Instantiate(UIManager.instance.GetFloatingTextUI());
            floatingText.StartFloating(text, origin, direction, distance, speed, duration, color);

            return floatingText;
        }

        public void StartFloating(string text, Vector3 origin, Vector3 direction, float distance, float speed, float duration, Color color)
        {
            this.text = text;
            this.origin = origin;
            this.direction = direction;
            this.distance = distance;
            this.speed = speed;
            this.duration = duration;
            this.color = color;

            StartCoroutine(StartFloating(StopFloating));
        }

        private IEnumerator StartFloating(Action<bool> callback)
        {
            gameObject.SetActive(true);

            textMeshPro.text = text;
            textMeshPro.color = color;

            transform.position = origin;
            transform.DOMove(origin + (direction * distance), duration / speed);
         
            yield return new WaitForSecondsRealtime(duration);

            callback(true);
        }

        private void StopFloating(bool isFinished)
        {
            if(isFinished)
            {
                StartCoroutine(StopFloating(DestroyFloating));
            }
        }

        private IEnumerator StopFloating(Action<bool> callback)
        {
            textMeshPro.DOFade(0f, fadingTime);

            yield return new WaitForSecondsRealtime(fadingTime);

            callback(true);
        }

        private void DestroyFloating(bool isFinished)
        {
            if (isFinished)
            {
                Destroy(gameObject);
            }
        }
    }
}

