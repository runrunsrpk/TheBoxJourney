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

        #region Team Button
        private void OnClickExit()
        {
            Hide();
        }

        private void OnClickSave()
        {

        }

        private void OnClickClear()
        {

        }
        #endregion

        #region Control Button

        private void OnClickAdd()
        {

        }

        private void OnClickRemove()
        {

        }

        private void OnClickUpdate()
        {

        }

        private void OnClickIndex()
        {

        }

        #endregion

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            LoadCharacterIcon();
            LoadCharacterIndex();
        }

        private void LoadCharacterIcon()
        {
            DestroyChildren(selectionContentPanel.transform);

            List<AllyInfo> allies = AssetManager.instance.GetAllAllyInfo();

            GameObject uiCharacterIconPref = AssetManager.instance.GetUI(UIReference.UICharacterIcon);

            foreach (AllyInfo ally in allies)
            {
                GameObject uiTemp = Instantiate(uiCharacterIconPref, selectionContentPanel);
                UICharacterIcon icon = uiTemp.GetComponent<UICharacterIcon>();
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

