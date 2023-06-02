using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UICharacterIcon : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private UIAllyManagement uiAllyManagement;
        private int index;

        private void Awake()
        {
            button.onClick.AddListener(OnClickIcon);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClickIcon);
        }

        public void InitUI(UIAllyManagement uiAllyManagement, int index)
        {
            this.uiAllyManagement = uiAllyManagement;
            this.index = index;
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        private void OnClickIcon()
        {
            uiAllyManagement.OnClickIcon?.Invoke(index);
        }
    }
}

