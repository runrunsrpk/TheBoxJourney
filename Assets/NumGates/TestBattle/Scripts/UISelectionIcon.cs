using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UISelectionIcon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private IUIBaseManagement uiBaseManagement;
        private int index;

        private void Awake()
        {
            button.onClick.AddListener(OnClickIcon);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClickIcon);
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

        public void SetEnableButton(bool isEnable)
        {
            button.interactable = isEnable;
        }

        private void OnClickIcon()
        {
            //uiBaseManagement.OnClickIcon?.Invoke(index);
            uiBaseManagement.OnClickSelection?.Invoke(index);
        }
    }
}

