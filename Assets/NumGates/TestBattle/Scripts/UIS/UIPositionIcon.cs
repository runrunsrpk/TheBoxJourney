using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UIPositionIcon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private IUIBaseManagement uiBaseManagement;
        private int index;
        private bool isEnable = false;

        private void Awake()
        {
            button.onClick.AddListener(OnClickPosition);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClickPosition);
        }

        public void InitUI(IUIBaseManagement uiBaseManagement, int index)
        {
            this.uiBaseManagement = uiBaseManagement;
            this.index = index;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void EnablePosition()
        {
            isEnable = true;

            button.GetComponent<Image>().color = Color.green;
        }

        public void DisablePosition()
        {
            isEnable = false;

            button.GetComponent<Image>().color = Color.white;
        }

        private void OnClickPosition()
        {
            uiBaseManagement.OnClickPosition?.Invoke(index, isEnable);
        }
    }
}

