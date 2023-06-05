using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class UIEnemyManagement : UIBaseManagement<EnemyInfo, EnemyData, EnemyCharacter>
    {
        #region Action
        protected override void ClickSelection(int index)
        {
            EnemyInfo info = tempInfos[index];
            EnemyData positionData = GetPositionData(info.character);

            // Replace stats data from position data if any
            EnemyData data = GetInstanceData();
            data.info = info;
            data.stats = positionData.stats;

            SetData(data);
            SetPreviewImage(data.info.fullBodySprite);

            CheckPositionIndex(data.info.character);
        }

        protected override void ClickPosition(int index, bool isNew)
        {
            if (isNew == true)
            {
                SetPositionData(index, tempData);
                SetPositionImage(index, tempData.info.fullBodySprite);

                CheckPositionIndex(tempData.info.character);
                SetEnableControlButtons(true, true, true, true);
            }
            else
            {
                EnemyData data = tempDatas[index];

                if (data.info.character != EnemyCharacter.ENE000)
                {
                    SetPreviewImage(data.info.fullBodySprite);
                    SetEnableControlButtons(true, true, true, true);
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
        }
        #endregion

        #region Data Handler
        protected override void LoadControlData()
        {
            EnemyInfo info = tempInfos[0];
            EnemyData positionData = GetPositionData(info.character);

            // Replace stats data from position data if any
            EnemyData data = GetInstanceData();
            data.info = info;
            data.stats = positionData.stats;

            SetData(data);
            SetPreviewImage(data.info.fullBodySprite);

            CheckPositionIndex(data.info.character);
        }

        protected override void LoadCustomizeData() { }

        protected override void LoadDetailData() { }

        protected override void LoadSelectionData()
        {
            DestroyChildrenContent(selectionContentPanel);

            GameObject uiPrefab = AssetManager.instance.GetUI(UIReference.UISelectionIcon);
            int index = 0;

            foreach (EnemyInfo info in tempInfos)
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

            foreach (EnemyData data in tempDatas)
            {
                GameObject uiTemp = Instantiate(uiPrefab, positionContentPanel);
                UIPositionIcon icon = uiTemp.GetComponent<UIPositionIcon>();
                icon.InitUI(this, index);
                icon.SetImage(data.info.character != EnemyCharacter.ENE000 ? data.info.fullBodySprite : null);
                index++;
            }
        }

        protected override void LoadDatas()
        {
            tempInfos = AssetManager.instance.GetAllEnemyInfo();

            if (levelManager.GetEnemyDatas().Count == maxPosition)
            {
                SetEnablePositionButtons(true, true, true);
                tempDatas = levelManager.GetEnemyDatas();
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
            levelManager.InitEnemyCharacter(tempDatas);

            if (isClearDatas == true) { levelManager.ResetEnemyDatas(); }

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
        protected override EnemyData GetInstanceData()
        {
            return new EnemyData(ScriptableObject.CreateInstance("EnemyInfo") as EnemyInfo, new EnemyStats());
        }

        protected override int GetPositionIndex(EnemyCharacter reference)
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

        protected override EnemyData GetPositionData(EnemyCharacter reference)
        {
            foreach (EnemyData data in tempDatas)
            {
                if (data.info.character == reference)
                {
                    return data;
                }
            }

            return GetInstanceData();
        }
        #endregion

        #region Other
        protected override bool IsPositionDatasEmpty()
        {
            foreach (EnemyData data in tempDatas)
            {
                if (data.info.character != EnemyCharacter.ENE000)
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

