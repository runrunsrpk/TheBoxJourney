using System;
using System.Collections;
using TMPro;
using UnityEngine;
using NumGates.TestBattle;
using DG.Tweening;

namespace NumGates
{
    public class UIFloatingText : MonoBehaviour
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
        public static UIFloatingText Create(string text, Vector3 origin, Vector3 direction, float distance, float speed, float duration, Color color)
        {
            GameObject uiTemp = Instantiate(AssetManager.instance.GetUI(UIReference.UIFloatingText));
            UIFloatingText floatingText = uiTemp.GetComponent<UIFloatingText>();
            floatingText.InitFloating(text, origin, direction, distance, speed, duration, color);
            floatingText.StartFloating();

            return floatingText;
        }

        private void InitFloating(string text, Vector3 origin, Vector3 direction, float distance, float speed, float duration, Color color)
        {
            this.text = text;
            this.origin = origin;
            this.direction = direction;
            this.distance = distance;
            this.speed = speed;
            this.duration = duration;
            this.color = color;
        }

        private void StartFloating()
        {
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

