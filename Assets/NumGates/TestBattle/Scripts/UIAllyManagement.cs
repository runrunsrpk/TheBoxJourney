using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UIAllyManagement : MonoBehaviour
    {
        [Header("Control Group")]
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private Button indexButton;

        //[Header("Customize Group")]

        //[Header("Detail Group")]

        [Header("Selection Group")]
        [SerializeField] private Transform selectionContentPanel;
        //[SerializeField] private GameObject uiCharacterIconPref;

        [Header("Team Group")]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private GameObject teamContentPanel;
        //[SerializeField] private GameObject uiCharacterIndexPref;

        private void Awake()
        {
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDestroy()
        {
            exitButton.onClick.RemoveListener(OnClickExit);
        }

        public void OnClickExit()
        {
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            LoadCharacterIcon();
        }

        private void LoadCharacterIcon()
        {
            DestroyChildren(selectionContentPanel.transform);

            List<AllyInfo> allies = AssetManager.instance.GetAllAllyInfo();

            UICharacterIcon uiCharacterIconPref = AssetManager.instance.GetUICharacterIcon();

            foreach (AllyInfo ally in allies)
            {
                GameObject icon = Instantiate(uiCharacterIconPref.gameObject, selectionContentPanel);
                icon.GetComponent<UICharacterIcon>().SetImage(ally.iconSprite);
            }
        }

        private void LoadCharacterIndex()
        {

        }

        private void DestroyChildren(Transform parent)
        {
            foreach(Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

