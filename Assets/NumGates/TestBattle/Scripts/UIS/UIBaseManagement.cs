using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NumGates.TestBattle
{
    public abstract class UIBaseManagement<TInfo, TData, TRef> : MonoBehaviour, IUIBaseManagement
    {
        public Action<int> OnClickSelection { get; set; }
        public Action<int, bool> OnClickPosition { get; set; }

        [Header("Control Group")]
        [SerializeField] private Image previewImage;
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private Button indexButton;

        //[Header("Customize Group")]

        //[Header("Detail Group")]

        [Header("Selection Group")]
        [SerializeField] protected Transform selectionContentPanel;

        [Header("Position Group")]
        [SerializeField] protected int maxPosition;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button exitButton;
        [SerializeField] protected Transform positionContentPanel;

        protected LevelManager levelManager;

        protected List<TInfo> tempInfos = new List<TInfo>();
        protected List<TData> tempDatas = new List<TData>();
        protected TData tempData;

        protected bool isEnablePosition = false;
        protected bool isClearDatas = false;

        #region Unity

        private void OnEnable()
        {
            // Add actions
            OnClickSelection += ClickSelection;
            OnClickPosition += ClickPosition;

            // Add onClick "Selection Group"
            addButton.onClick.AddListener(OnClickAdd);
            removeButton.onClick.AddListener(OnClickRemove);

            // Add onClick "Customize Group"

            // Add onClick "Detail Group"

            // Add onClick "Position Group"
            saveButton.onClick.AddListener(OnClickSave);
            clearButton.onClick.AddListener(OnClickClear);
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDisable()
        {
            // Remove actions
            OnClickSelection -= ClickSelection;
            OnClickPosition -= ClickPosition;

            // Remove onClick "Selection Group"
            addButton.onClick.RemoveListener(OnClickAdd);
            removeButton.onClick.RemoveListener(OnClickRemove);

            // Remove onClick "Customize Group"

            // Remove onClick "Detail Group"

            // Remove onClick "Position Group"
            saveButton.onClick.RemoveListener(OnClickSave);
            clearButton.onClick.RemoveListener(OnClickClear);
            exitButton.onClick.RemoveListener(OnClickExit);
        }

        #endregion

        #region Shoaw and Hide
        public void Show()
        {
            gameObject.SetActive(true);

            LoadDatas();
            LoadControlData();
            LoadCustomizeData();
            LoadDetailData();
            LoadSelectionData();
            LoadPositionData();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Action
        protected virtual void ClickSelection(int index) { }
        protected virtual void ClickPosition(int index, bool isNew) 
        { 
            if(isNew == true)
            {
                SetButtonText(addButton, "Add");
                SetButtonText(indexButton, $"Index-{index}");

                SetEnablePositionIcon(false);
                SetEnableSelectionIcon(true);
                SetEnablePositionButtons(true, true, true);
            }
        }
        #endregion

        #region Init
        public virtual void InitUI() 
        {
            levelManager = GameManager.instance.LevelManager;

            SetEnableControlButtons(false, false, false, false);
            SetEnablePositionButtons(false, false, false);
        }
        #endregion

        #region Data Handler
        protected virtual void LoadControlData() { }
        protected virtual void LoadCustomizeData() { }
        protected virtual void LoadDetailData() { }
        protected virtual void LoadSelectionData() { }
        protected virtual void LoadPositionData() { }
        protected virtual void LoadDatas() { }
        #endregion

        #region Control Group
        protected virtual void OnClickAdd() 
        { 
            if(isEnablePosition == true)
            {
                SetButtonText(addButton, "Add");
                SetEnablePositionIcon(false);
                SetEnableSelectionIcon(true);
            }
            else
            {
                SetButtonText(addButton, "Cancel");
                SetEnablePositionIcon(true);
                SetEnableSelectionIcon(false);
            }
        }

        protected virtual void OnClickRemove() 
        {
            string indexText = indexButton.GetComponentInChildren<TextMeshProUGUI>().text;
            int targetIndex = int.Parse(indexText.Split('-').GetValue(1).ToString());

            SetPositionData(targetIndex, GetInstanceData());
            SetPositionImage(targetIndex, null);

            if (IsPositionDatasEmpty()) { SetEnablePositionButtons(true, false, true); }
                
        }

        protected virtual void OnClickUpdate() { }

        protected virtual void OnClickIndex() { }

        #endregion

        #region Customize Group
        #endregion

        #region Detail Group
        #endregion

        #region Selection Group
        #endregion

        #region Position Group
        protected virtual void OnClickSave() 
        {
            if (isClearDatas == true)
            {
                isClearDatas = false;
                SetEnablePositionButtons(true, false, true);
            }
            else
            {
                SetEnablePositionButtons(true, true, true);
            }
        }

        protected virtual void OnClickClear() 
        {
            isClearDatas = true;

            ResetDatas();
            SetEnablePositionButtons(true, false, true);

            for (int index = 0; index < maxPosition; index++)
            {
                SetPositionData(index, tempDatas[index]);
                SetPositionImage(index, null);
            }
        }

        protected virtual void OnClickExit() 
        {
            Hide();
        }

        #endregion

        #region Helper

        #region Setting
        protected void SetData(TData data)
        {
            tempData = data;
        }

        protected void SetPreviewImage(Sprite sprite)
        {
            previewImage.sprite = sprite;
        }

        protected void SetIndexButtonText(string text)
        {
            SetButtonText(indexButton, $"Index-{text}");
        }

        protected void SetButtonText(Button button, string text)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

        protected void SetPositionImage(int index, Sprite sprite)
        {
            positionContentPanel.GetChild(index).GetComponent<UIPositionIcon>().SetImage(sprite);
        }

        protected void SetEnableSelectionIcon(bool isEnable)
        {
            foreach (Transform child in selectionContentPanel)
            {
                UISelectionIcon icon = child.gameObject.GetComponent<UISelectionIcon>();
                icon.SetEnableButton(isEnable);
            }
        }

        protected void SetEnablePositionIcon(bool isEnable)
        {
            isEnablePosition = isEnable;

            foreach (Transform child in positionContentPanel)
            {
                UIPositionIcon member = child.gameObject.GetComponent<UIPositionIcon>();

                if (isEnable) { member.EnablePosition(); }
                else { member.DisablePosition(); }
            }
        }

        protected void SetPositionData(int index, TData data)
        {
            tempDatas[index] = data;
        }

        protected void SetEnableControlButtons(bool addEnable, bool removeEnable, bool updateEneble, bool indexEnable)
        {
            addButton.interactable = addEnable;
            removeButton.interactable = removeEnable;
            updateButton.interactable = updateEneble;
            indexButton.interactable = indexEnable;
        }

        protected void SetEnablePositionButtons(bool saveEnable, bool clearEnable, bool exitEneble)
        {
            saveButton.interactable = saveEnable;
            clearButton.interactable = clearEnable;
            exitButton.interactable = exitEneble;
        }
        #endregion

        #region Getting
        protected virtual TData GetInstanceData()
        {
            return (TData)Activator.CreateInstance(typeof(TData));
        }

        protected virtual int GetPositionIndex(TRef reference)
        {
            return maxPosition;
        }

        protected virtual TData GetPositionData(TRef reference)
        {
            return GetInstanceData();
        }
        #endregion

        #region Other
        protected virtual bool IsPositionDatasEmpty()
        {
            return true;
        }

        protected void ResetData()
        {

        }

        protected void ResetDatas()
        {
            tempDatas.Clear();

            if (tempDatas.Count == 0)
            {
                for (int index = 0; index < maxPosition; index++)
                {
                    tempDatas.Add(GetInstanceData());
                }
            }
        }

        protected void CheckPositionIndex(TRef reference)
        {
            int positionIndex = GetPositionIndex(reference);
            if (positionIndex == maxPosition)
            {
                SetEnableControlButtons(true, false, false, false);
                SetButtonText(indexButton, $"Index-X");
            }
            else
            {
                SetEnableControlButtons(false, true, true, true);
                SetButtonText(indexButton, $"Index-{positionIndex}");
            }
        }

        protected void DestroyChildrenContent(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
        #endregion

        #endregion
    }
}

