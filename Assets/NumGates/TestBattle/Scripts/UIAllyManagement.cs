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
        #region Action
        protected override void ClickSelection(int index) 
        {
            AllyInfo allyInfo = tempInfos[index];
            AllyData allyPositionData = GetPositionData(allyInfo.character);

            // Replace stats data from position data if any
            AllyData allyData = GetInstanceData();
            allyData.info = allyInfo;
            allyData.stats = allyPositionData.stats;

            SetData(allyData);
            SetPreviewImage(allyData.info.fullBodySprite);

            CheckPositionIndex(allyData.info.character);
        }

        protected override void ClickPosition(int index, bool isNew) 
        {
            base.ClickPosition(index, isNew);

            if(isNew == true)
            {
                SetPositionData(index, tempData);
                SetPositionImage(index, tempData.info.fullBodySprite);

                CheckPositionIndex(tempData.info.character);
            }
            else
            {
                AllyData allyData = tempDatas[index];

                SetPreviewImage(allyData.info.fullBodySprite);
            }
        }
        #endregion

        #region Init
        public override void InitUI()
        {
            base.InitUI();
        }
        #endregion

        #region Data Handler
        protected override void LoadControlData() 
        {
            AllyInfo allyInfo = tempInfos[0];
            AllyData allyPositionData = GetPositionData(allyInfo.character);

            // Replace stats data from position data if any
            AllyData allyData = GetInstanceData();
            allyData.info = allyInfo;
            allyData.stats = allyPositionData.stats;

            SetData(allyData);
            SetPreviewImage(allyData.info.fullBodySprite);

            CheckPositionIndex(tempData.info.character);
        }

        protected override void LoadCustomizeData() { }

        protected override void LoadDetailData() { }

        protected override void LoadSelectionData() 
        {
            DestroyChildrenContent(selectionContentPanel);

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UISelectionIcon);
            int index = 0;

            foreach (AllyInfo ally in tempInfos)
            {
                GameObject uiTemp = Instantiate(uiPrefab, selectionContentPanel);
                UISelectionIcon icon = uiTemp.GetComponent<UISelectionIcon>();
                icon.InitUI(this, index);
                icon.SetImage(ally.iconSprite);
                index++;
            }
        }

        protected override void LoadPositionData() 
        {
            DestroyChildrenContent(positionContentPanel);
            LoadDatas();

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UIPositionIcon);
            int index = 0;

            foreach (AllyData allyData in tempDatas)
            {
                GameObject uiTemp = Instantiate(uiPrefab, positionContentPanel);
                UIPositionIcon member = uiTemp.GetComponent<UIPositionIcon>();
                member.InitUI(this, index);
                member.SetImage(allyData.info.character != AllyCharacter.EmptyAlly ? allyData.info.fullBodySprite : null);
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
                if(allyData.info.character != AllyCharacter.EmptyAlly)
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

