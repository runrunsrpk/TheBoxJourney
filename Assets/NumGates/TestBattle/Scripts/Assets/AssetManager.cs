using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NumGates.TestBattle
{
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager instance;

        public Action OnLoadComplete;

        //private AssetLoader assetLoader;
        [Header("Asset Loader")]
        [SerializeField] private AssetLoaderUI assetLoaderUI;
        [SerializeField] private AssetLoaderAlly assetLoaderAlly;
        [SerializeField] private AssetLoaderEnemy assetLoaderEnemy;
        [SerializeField] private AssetLoaderAllyInfo assetLoaderAllyInfo;

        [Header("UI Reference")]
        [SerializeField] private AssetLabelReference uiLable;

        [Header("Character Reference")]
        [SerializeField] private AssetLabelReference allyLable;
        [SerializeField] private AssetLabelReference enemyLable;

        [Header("Database Reference")]
        [SerializeField] private AssetLabelReference allyInfoLable;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }

            instance = this;
            DontDestroyOnLoad(instance);
        }

        public IEnumerator InitAssets()
        {
            yield return StartCoroutine(assetLoaderUI.LoadAssets(uiLable));
            yield return StartCoroutine(assetLoaderAlly.LoadAssets(allyLable));
            yield return StartCoroutine(assetLoaderAllyInfo.LoadAssets(allyInfoLable));
            yield return StartCoroutine(assetLoaderEnemy.LoadAssets(enemyLable));

            OnLoadComplete?.Invoke();
        }

        #region UI Asset

        //public UIFloatingText GetUIFloatingText() => assetLoaderUI.GetAsset(UIReference.UIFloatingText).GetComponent<UIFloatingText>();
        //public UIBattleEvent GetUIBattleEvent() => assetLoaderUI.GetAsset(UIReference.UIBattleEvent).GetComponent<UIBattleEvent>();
        //public UITimerGauge GetUITimerGauge() => assetLoaderUI.GetAsset(UIReference.UITimerGauge).GetComponent<UITimerGauge>();
        //public UIHealthGauge GetUIHealthGauge() => assetLoaderUI.GetAsset(UIReference.UIHealthGauge).GetComponent<UIHealthGauge>();
        //public UIAllyManagement GetUIAllyManagement() => assetLoaderUI.GetAsset(UIReference.UIAllyManagement).GetComponent<UIAllyManagement>();
        //public UICharacterIcon GetUICharacterIcon() => assetLoaderUI.GetAsset(UIReference.UICharacterIcon).GetComponent<UICharacterIcon>();

        public GameObject GetUI(string ui) => assetLoaderUI.GetAsset(ui);
        #endregion

        #region Character Asset

        public GameObject GetAllyCharacter(AllyCharacter ally) => assetLoaderAlly.GetAsset(ally);
        public GameObject GetEnemyCharacter(CharacterEnemy enemy) => assetLoaderEnemy.GetAsset(enemy);

        #endregion

        #region Database Asset

        public AllyInfo GetAllyInfo(AllyCharacter ally) => assetLoaderAllyInfo.GetAsset(ally);
        public List<AllyInfo> GetAllAllyInfo() => assetLoaderAllyInfo.GetAllAssets();
        #endregion
    }
}

