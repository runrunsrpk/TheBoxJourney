using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NumGates.TestBattle
{ 
    public class UIAllyManagement : UIBaseManagement<AllyInfo, AllyData, AllyCharacter>
    {
        [Header("Customize Group")]
        [SerializeField] private Button resetAllButton;
        [SerializeField] private Button restatusButton;
        [SerializeField] private TextMeshProUGUI allPointsText;
        [SerializeField] private UIAllyStatCustomize strStat;
        [SerializeField] private UIAllyStatCustomize agiStat;
        [SerializeField] private UIAllyStatCustomize vitStat;
        [SerializeField] private UIAllyStatCustomize dexStat;
        [SerializeField] private UIAllyStatCustomize intStat;
        [SerializeField] private UIAllyStatCustomize wisStat;
        [SerializeField] private UIAllyStatCustomize chrStat;
        [SerializeField] private UIAllyStatCustomize lukStat;

        private PrimaryStatus tempPrimatyStatus;
        private int allStatusPoints;

        [Header("Detail Group")]
        [SerializeField] private Button detailButton;
        [SerializeField] private Button growthButton;

        #region Action
        protected override void ClickSelection(int index) 
        {
            AllyInfo info = tempInfos[index];
            AllyData positionData = GetPositionData(info.character);

            // Replace stats data from position data if any
            AllyData data = GetInstanceData();
            data.info = info;
            data.stats = positionData.stats;

            SetData(data);
            SetPreviewImage(data.info.fullBodySprite);

            CheckPositionIndex(data.info.character);
        }

        protected override void ClickPosition(int index, bool isNew) 
        {
            if(isNew == true)
            {
                SetPositionData(index, tempData);
                SetPositionImage(index, tempData.info.fullBodySprite);

                CheckPositionIndex(tempData.info.character);
            }
            else
            {
                AllyData data = tempDatas[index];

                if(data.info.character != AllyCharacter.ALY000)
                {
                    SetPreviewImage(data.info.fullBodySprite);
                    SetEnableControlButtons(false, true, true, true);
                    SetIndexButtonText($"{index}");
                }
                else
                {
                    SetPreviewImage(null);
                    SetEnableControlButtons(false, false, false, false);
                    SetIndexButtonText("X");
                }
            }

            base.ClickPosition(index, isNew);
        }
        #endregion

        #region Init
        public override void InitUI()
        {
            base.InitUI();

            strStat.InitUI();
            agiStat.InitUI();
            vitStat.InitUI();
            dexStat.InitUI();
            intStat.InitUI();
            wisStat.InitUI();
            chrStat.InitUI();
            lukStat.InitUI();
        }
        #endregion

        #region Data Handler
        protected override void LoadControlData() 
        {
            AllyInfo info = tempInfos[0];
            AllyData positionData = GetPositionData(info.character);

            // Replace stats data from position data if any
            AllyData data = GetInstanceData();
            data.info = info;
            data.stats = positionData.stats;

            SetData(data);
            SetPreviewImage(data.info.fullBodySprite);

            CheckPositionIndex(data.info.character);
        }

        protected override void LoadCustomizeData() 
        {
            AllyInfo info = tempInfos[0];
            AllyData positionData = GetPositionData(info.character);

            strStat.LoadStatPoint(positionData.stats.primaryStatus.STR);
            agiStat.LoadStatPoint(positionData.stats.primaryStatus.AGI);
            vitStat.LoadStatPoint(positionData.stats.primaryStatus.VIT);
            dexStat.LoadStatPoint(positionData.stats.primaryStatus.DEX);
            intStat.LoadStatPoint(positionData.stats.primaryStatus.INT);
            wisStat.LoadStatPoint(positionData.stats.primaryStatus.WIS);
            chrStat.LoadStatPoint(positionData.stats.primaryStatus.CHR);
            lukStat.LoadStatPoint(positionData.stats.primaryStatus.LUK);

            tempPrimatyStatus = new PrimaryStatus
                (
                    strStat.StatPoint,
                    agiStat.StatPoint,
                    vitStat.StatPoint,
                    dexStat.StatPoint,
                    intStat.StatPoint,
                    wisStat.StatPoint,
                    chrStat.StatPoint,
                    lukStat.StatPoint
                );
        }

        protected override void LoadDetailData() { }

        protected override void LoadSelectionData() 
        {
            DestroyChildrenContent(selectionContentPanel);

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UISelectionIcon);
            int index = 0;

            foreach (AllyInfo info in tempInfos)
            {
                GameObject uiTemp = Instantiate(uiPrefab, selectionContentPanel);
                UISelectionIcon icon = uiTemp.GetComponent<UISelectionIcon>();
                icon.InitUI(this, index);
                icon.SetImage(info.iconSprite);
                index++;
            }
        }

        protected override void LoadPositionData() 
        {
            DestroyChildrenContent(positionContentPanel);
            LoadDatas();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UIPositionIcon);
            int index = 0;

            foreach (AllyData data in tempDatas)
            {
                GameObject uiTemp = Instantiate(uiPrefab, positionContentPanel);
                UIPositionIcon icon = uiTemp.GetComponent<UIPositionIcon>();
                icon.InitUI(this, index);
                icon.SetImage(data.info.character != AllyCharacter.ALY000 ? data.info.fullBodySprite : null);
                index++;
            }
        }

        protected override void LoadDatas() 
        {
            tempInfos = AssetManager.instance.GetAllAllyInfo();

            if (levelManager.GetAllyDatas().Count == maxPosition)
            {
                SetEnablePositionButtons(true, true, true);
                tempDatas = levelManager.GetAllyDatas();
            }
            else
            {
                SetEnablePositionButtons(true, false, true);
                ResetDatas();
            }
        }
        #endregion

        #region Control Group
        protected override void OnClickAdd() 
        {
            base.OnClickAdd();
        }

        protected override void OnClickRemove() 
        {
            base.OnClickRemove();
            CheckPositionIndex(tempData.info.character);
        }

        protected override void OnClickUpdate() { }

        protected override void OnClickIndex() { }
        #endregion

        #region Customize Group
        #endregion

        #region Detail Group
        #endregion

        #region Selection Group
        #endregion

        #region Position Group
        protected override void OnClickSave() 
        {
            // Init to reset spawned character
            levelManager.InitAllyCharacter(tempDatas);

            if (isClearDatas == true) { levelManager.ResetAllyDatas(); }

            base.OnClickSave();
        }

        protected override void OnClickClear() 
        {
            base.OnClickClear();
        }

        protected override void OnClickExit() 
        {
            base.OnClickExit();
        }
        #endregion

        #region Helper

        #region Getting
        protected override AllyData GetInstanceData()
        {
            return new AllyData(ScriptableObject.CreateInstance("AllyInfo") as AllyInfo, new AllyStats());
        }

        protected override int GetPositionIndex(AllyCharacter reference)
        {
            for (int index = 0; index < maxPosition; index++)
            {
                if (tempDatas[index].info.character == reference)
                {
                    return index;
                }
            }

            return maxPosition;
        }

        protected override AllyData GetPositionData(AllyCharacter reference)
        {
            foreach (AllyData allyData in tempDatas)
            {
                if (allyData.info.character == reference)
                {
                    return allyData;
                }
            }

            return GetInstanceData();
        }
        #endregion

        #region Other
        protected override bool IsPositionDatasEmpty()
        {
            foreach(AllyData allyData in tempDatas)
            {
                if(allyData.info.character != AllyCharacter.ALY000)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #endregion
    }
}

