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
            allyDatas[index] = customData;

            teamContentPanel.GetChild(index).GetComponent<UIMemberIcon>().SetImage(customData.info.fullBodySprite);

            // TODO: Update current data to stats
            // TODO: Clean code
            isAddCharacter = false;
            addButton.GetComponentInChildren<TextMeshProUGUI>().text = "Add";

            foreach (Transform child in teamContentPanel)
            {
                UIMemberIcon member = child.gameObject.GetComponent<UIMemberIcon>();
                member.DisableAddMember();
            }
        }

        #endregion

        private void LoadPreviewCharacter()
        {
            previewImage.sprite = allies[0].fullBodySprite;

            AllyInfo allyInfo = allies[0];
            AllyData allyData = GetAllyDataInTeam(allyInfo.character);

            customData.info = allyInfo;
            customData.stats = allyData.stats;
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
            LoadAllyDatas();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UIMemberIcon);
            int index = 0;

            foreach(AllyData allyData in allyDatas)
            {
                GameObject uiTemp = Instantiate(uiPrefab, teamContentPanel);
                UIMemberIcon member = uiTemp.GetComponent<UIMemberIcon>();
                member.InitUI(this, index);
                member.SetImage(allyData.info.character != CharacterAlly.EmptyAlly ? allyData.info.fullBodySprite : null);
                index++;
            }
        }

        #region Helper
        private void LoadAllyDatas()
        {
            allyDatas.Clear();
            allyDatas = levelManager.GetAllyDatas();

            if(allyDatas.Count == 0)
            {
                for (int index = 0; index < maxTeamMember; index++)
                {
                    allyDatas.Add(new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats()));
                }
            }
        }

        private AllyData GetAllyDataInTeam(CharacterAlly character)
        {
            foreach(AllyData allyData in allyDatas)
            {
                if(allyData.info.character == character)
                {
                    return allyData;
                }
            }
            return new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats());
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

