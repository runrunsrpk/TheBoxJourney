using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates
{
    public static class TheBoxUI
    {
        #region Setting
        public static void SetTextByValue(TextMeshProUGUI text, int value)
        {
            text.text = $"{value}";
        }

        public static void SetTextByValue(TextMeshProUGUI text, float value)
        {
            text.text = $"{value}";
        }

        public static void SetTextByString(TextMeshProUGUI text, string value)
        {
            text.text = value;
        }

        public static void SetImage(Image image, Sprite sprite)
        {
            image.sprite = sprite;
        }
        #endregion

        #region Getting
        public static int GetTextValueInt(TextMeshProUGUI text)
        {
            return int.Parse(text.text);
        }
        #endregion

        #region Enable and Disable
        public static void EnableText(TextMeshProUGUI text, bool isEnable)
        {
            text.gameObject.SetActive(isEnable);
        }

        public static void EnableButton(Button button, bool isEnable)
        {
            button.interactable = isEnable;
        }
        #endregion
    }
}

