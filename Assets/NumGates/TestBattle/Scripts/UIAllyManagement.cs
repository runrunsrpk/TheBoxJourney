using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{
    public class UIAllyManagement : MonoBehaviour
    {
        public Action<int> OnClickIcon;
        public Action<int> OnClickTeamMember;
        public Action<int> OnClickAddTeamMember;

        [Header("Control Group")]
        [SerializeField] private Image previewImage;
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private Button indexButton;

        //[Header("Customize Group")]

        //[Header("Detail Group")]

        [Header("Selection Group")]
        [SerializeField] private Transform selectionContentPanel;

        [Header("Team Group")]
        [SerializeField] private int maxTeamMember;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Transform teamContentPanel;
        //[SerializeField] private GameObject uiCharacterIndexPref;

        private LevelManager levelManager;

        private List<AllyInfo> allies = new List<AllyInfo>();
        private List<AllyData> allyDatas = new List<AllyData>();

        private AllyData customData;

        public void InitUI()
        {
            levelManager = GameManager.instance.LevelManager;
        }

        private void Awake()
        {
            OnClickIcon += ClickIcon;
            OnClickTeamMember += ClickMember;
            OnClickAddTeamMember += ClickAddMember;

            addButton.onClick.AddListener(OnClickAdd);
            removeButton.onClick.AddListener(OnClickRemove);

            saveButton.onClick.AddListener(OnClickSave);
            clearButton.onClick.AddListener(OnClickClear);
            exitButton.onClick.AddListener(OnClickExit);
        }

        private void OnDestroy()
        {
            addButton.onClick.RemoveListener(OnClickAdd);
            removeButton.onClick.RemoveListener(OnClickRemove);

            saveButton.onClick.RemoveListener(OnClickSave);
            clearButton.onClick.RemoveListener(OnClickClear);
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

        private bool isAddCharacter = false;


        // TODO: Load ally data to custom data
        private void OnClickAdd()
        {
            if(isAddCharacter == false)
            {
                isAddCharacter = true;
                addButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";

                foreach (Transform child in teamContentPanel)
                {
                    UIMemberIcon member = child.gameObject.GetComponent<UIMemberIcon>();
                    member.EnableAddMember();
                }
            }
            else
            {
                isAddCharacter = false;
                addButton.GetComponentInChildren<TextMeshProUGUI>().text = "Add";

                foreach (Transform child in teamContentPanel)
                {
                    UIMemberIcon member = child.gameObject.GetComponent<UIMemberIcon>();
                    member.DisableAddMember();
                }
            }
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
            LoadCharacterMember();
            LoadPreviewCharacter();
            // TODO: Load FirstPositionCharacter or FirstSelectionIcon
        }

        #region Action

        private void ClickIcon(int index)
        {
            AllyInfo allyInfo = allies[index];

            previewImage.sprite = allyInfo.fullBodySprite;

            AllyData allyData = GetAllyDataInTeam(allyInfo.character);

            customData.info = allyInfo;
            customData.stats = allyData.stats;

            // TODO: Set all base stats
        }

        private void ClickMember(int index)
        {
            AllyData allyData = allyDatas[index];

            previewImage.sprite = allyData.info.fullBodySprite;

            // TODO: Set all base stats
        }

        private void ClickAddMember(int index)
        {

        }

        #endregion

        private void LoadPreviewCharacter()
        {
            if(allyDatas.Count > 0)
            {
                previewImage.sprite = allyDatas[0].info.fullBodySprite;
            }
            else
            {
                previewImage.sprite = allies[0].fullBodySprite;
            }
        }

        private void LoadCharacterIcon()
        {
            DestroyChildren(selectionContentPanel);

            allies = AssetManager.instance.GetAllAllyInfo();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UICharacterIcon);
            int index = 0;
            foreach (AllyInfo ally in allies)
            {
                GameObject uiTemp = Instantiate(uiPrefab, selectionContentPanel);
                UICharacterIcon icon = uiTemp.GetComponent<UICharacterIcon>();
                icon.InitUI(this, index);
                icon.SetImage(ally.iconSprite);
                index++;
            }
        }

        private void LoadCharacterMember()
        {
            DestroyChildren(teamContentPanel);

            allyDatas = levelManager.GetAllyDatas();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UIMemberIcon);

            for (int index = 0; index < maxTeamMember; index++)
            {
                GameObject uiTemp = Instantiate(uiPrefab, teamContentPanel);
                UIMemberIcon member = uiTemp.GetComponent<UIMemberIcon>();
                member.InitUI(this, index);

                if (index < allyDatas.Count)
                {
                    member.SetImage(allyDatas[index].info.fullBodySprite);
                }
                else
                {
                    member.SetImage(null);
                }
            }
        }

        #region Helper

        private AllyData GetAllyDataInTeam(CharacterAlly character)
        {
            foreach(AllyData allyData in allyDatas)
            {
                if(allyData.info.character == character)
                {
                    return allyData;
                }
            }
            return new AllyData();
        }
        private void DestroyChildren(Transform parent)
        {
            foreach(Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        #endregion
    }
}

