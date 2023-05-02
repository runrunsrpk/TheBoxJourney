using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NumGates.TestBattle;

namespace NumGates
{
    public class FloatingTextUI : MonoBehaviour
    {
        [SerializeField] TextMeshPro textMeshPro;

        private string text;
        private Vector3 origin;
        private Vector3 direction;
        private float speed;
        private float duration;

        public static FloatingTextUI Create(string text, Vector3 origin, Vector3 direction, float speed, float duration)
        {
            FloatingTextUI floatingText = Instantiate(UIManager.instance.GetFloatingTextUI());
            floatingText.StartFloating(text, origin, direction, speed, duration);

            return floatingText;
        }

        public void StartFloating(string text, Vector3 origin, Vector3 direction, float speed, float duration)
        {
            this.text = text;
            this.origin = origin;
            this.direction = direction;
            this.speed = speed;
            this.duration = duration;

            StartCoroutine(OnStartFloating());
        }

        private IEnumerator OnStartFloating()
        {
            gameObject.SetActive(true);

            textMeshPro.text = text;

            transform.position = origin;
            transform.Translate(direction * speed * Time.deltaTime);

            yield return new WaitForSecondsRealtime(duration);

            Destroy(gameObject);
            //StopFloating();
        }

        private void StopFloating()
        {
            gameObject.SetActive(false);

            transform.Translate(Vector3.zero);
            transform.position = Vector3.zero;
        }
    }
}

