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

        private List<AllyInfo> tempAllies = new List<AllyInfo>();
        private List<AllyData> tempAllyDatas = new List<AllyData>();

        private AllyData tempAllyData;

        public void InitUI()
        {
            levelManager = GameManager.instance.LevelManager;

            addButton.interactable = false;
            removeButton.interactable = false;
            updateButton.interactable = false;
            indexButton.interactable = false;
            clearButton.interactable = false;
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
            levelManager.InitAllyCharacter(tempAllyDatas);
        }

        private void OnClickClear()
        {
            ResetAllyDatas();

            for(int index = 0; index < maxTeamMember; index++)
            {
                SetTeamMemberData(index, tempAllyDatas[index]);
                SetTeamMemberImage(index, null);
            }
        }
        #endregion

        #region Control Button

        private bool isAddMember = false;

        // TODO: Load ally data to custom data
        private void OnClickAdd()
        {
            if(isAddMember == false)
            {
                SetButtonText(addButton, "Cancel");
                SetEnableAddMember(true);
                SetEnableIconButton(false);
            }
            else
            {
                SetButtonText(addButton, "Add");
                SetEnableAddMember(false);
                SetEnableIconButton(true);
            }
        }

        private void OnClickRemove()
        {
            string indexText = indexButton.GetComponentInChildren<TextMeshProUGUI>().text;
            int targetIndex = int.Parse(indexText.Split('-').GetValue(1).ToString());

            SetTeamMemberData(targetIndex, new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats()));
            SetTeamMemberImage(targetIndex, null);

            CheckAllyIndex(tempAllyData.info.character);
        }

        private void OnClickUpdate()
        {

        }

        private void OnClickIndex()
        {

        }

        #endregion

        #region Show and Hide

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

        #endregion

        #region Action

        private void ClickIcon(int index)
        {
            AllyInfo allyInfo = tempAllies[index];
            AllyData allyData = GetAllyDataInTeam(allyInfo.character);

            SetAllyData(allyInfo, allyData.stats);
            SetPreviewImage(allyInfo.fullBodySprite);

            CheckAllyIndex(allyInfo.character);
            // TODO: Set all base stats
        }

        private void ClickMember(int index)
        {
            AllyData allyData = tempAllyDatas[index];

            SetPreviewImage(allyData.info.fullBodySprite);

            // TODO: Set all base stats
        }

        private void ClickAddMember(int index)
        {
            // TODO: Update current data to stats
            // TODO: Clean code

            SetTeamMemberData(index, tempAllyData);
            SetTeamMemberImage(index, tempAllyData.info.fullBodySprite);
            SetButtonText(addButton, "Add");
            SetButtonText(indexButton, $"Index-{index}");
            SetEnableAddMember(false);
            SetEnableIconButton(true);

            CheckAllyIndex(tempAllyData.info.character);

        }

        #endregion

        #region Load Data

        private void LoadPreviewCharacter()
        {
            AllyInfo allyInfo = tempAllies[0];
            AllyData allyData = GetAllyDataInTeam(allyInfo.character);

            SetAllyData(allyInfo, allyData.stats);
            SetPreviewImage(allyInfo.fullBodySprite);

            CheckAllyIndex(allyInfo.character);
        }

        private void LoadCharacterIcon()
        {
            DestroyChildren(selectionContentPanel);

            tempAllies = AssetManager.instance.GetAllAllyInfo();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UICharacterIcon);
            int index = 0;
            foreach (AllyInfo ally in tempAllies)
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

            foreach(AllyData allyData in tempAllyDatas)
            {
                GameObject uiTemp = Instantiate(uiPrefab, teamContentPanel);
                UIMemberIcon member = uiTemp.GetComponent<UIMemberIcon>();
                member.InitUI(this, index);
                member.SetImage(allyData.info.character != CharacterAlly.EmptyAlly ? allyData.info.fullBodySprite : null);
                index++;
            }
        }

        private void LoadAllyDatas()
        {
            if(levelManager.GetAllyDatas().Count == maxTeamMember)
            {
                tempAllyDatas = levelManager.GetAllyDatas();
            }
            else
            {
                ResetAllyDatas();
            }
        }

        #endregion

        #region Helper

        private void SetPreviewImage(Sprite sprite)
        {
            previewImage.sprite = sprite;
        }

        private void SetAllyData(AllyInfo allyInfo, AllyStats allyStats)
        {
            tempAllyData.info = allyInfo;
            tempAllyData.stats = allyStats;
        }

        private void SetButtonText(Button button, string text)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }

        private void SetTeamMemberData(int index, AllyData allyData)
        {
            tempAllyDatas[index] = allyData;
        }

        private void SetTeamMemberImage(int index, Sprite sprite)
        {
            teamContentPanel.GetChild(index).GetComponent<UIMemberIcon>().SetImage(sprite);
        }

        private void SetEnableIconButton(bool isEnable)
        {
            foreach (Transform child in selectionContentPanel)
            {
                UICharacterIcon icon = child.gameObject.GetComponent<UICharacterIcon>();
                icon.SetEnableButton(isEnable);
            }
        }

        private void SetEnableAddMember(bool isEnable)
        {
            isAddMember = isEnable;

            foreach (Transform child in teamContentPanel)
            {
                UIMemberIcon member = child.gameObject.GetComponent<UIMemberIcon>();

                if (isEnable) { member.EnableAddMember(); }
                else { member.DisableAddMember(); }
            }
        }

        private void ResetAllyData()
        {

        }

        private void ResetAllyDatas()
        {
            tempAllyDatas.Clear();

            if (tempAllyDatas.Count == 0)
            {
                for (int index = 0; index < maxTeamMember; index++)
                {
                    tempAllyDatas.Add(new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats()));
                }
            }
        }

        private int GetIndexInTeam(CharacterAlly character)
        {
            for(int index = 0; index < maxTeamMember; index++)
            {
                if(tempAllyDatas[index].info.character == character)
                {
                    return index;
                }
            }
            return maxTeamMember;
        }

        private AllyData GetAllyDataInTeam(CharacterAlly character)
        {
            foreach(AllyData allyData in tempAllyDatas)
            {
                if(allyData.info.character == character)
                {
                    return allyData;
                }
            }
            return new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats());
        }

        private void CheckAllyIndex(CharacterAlly character)
        {
            int memberIndex = GetIndexInTeam(character);
            if (memberIndex == maxTeamMember)
            {
                addButton.interactable = true;
                removeButton.interactable = false;
                updateButton.interactable = false;
                indexButton.interactable = false;

                SetButtonText(indexButton, $"Index-X");
            }
            else
            {
                addButton.interactable = false;
                removeButton.interactable = true;
                updateButton.interactable = true;
                indexButton.interactable = true;

                SetButtonText(indexButton, $"Index-{memberIndex}");
            }
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

