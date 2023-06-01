using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NumGates.TestBattle
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        [SerializeField] private LevelManager levelManagerPrefab;
        [SerializeField] private UIManager uiManagerPrefab;

        public LevelManager LevelManager => levelManager;
        private LevelManager levelManager;

        public UIManager UIManager => uiManager;
        private UIManager uiManager;

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

        private void Start()
        {
            InitAssets();
        }

        private void Update()
        {
            
        }

        private void InitAssets()
        {
            Debug.Log($"GameManage 'InitAsset'");

            AssetManager.instance.OnLoadComplete += OnLoadComplete;

            StartCoroutine(AssetManager.instance.InitAssets());
        }

        private void OnLoadComplete()
        {
            InitManager();
        }

        private void InitManager()
        {
            Debug.Log($"GameManage 'InitManager'");

            if(IsAvailableInstanstitate(levelManagerPrefab, levelManager))
            {
                levelManager = Instantiate(levelManagerPrefab, transform);
                levelManager.InitManager();
            }

            if(IsAvailableInstanstitate(uiManagerPrefab, uiManager))
            {
                //uiManager = Instantiate(uiManagerPrefab, transform);
                uiManager = uiManagerPrefab;
                uiManager.InitManager();
            }
        }

        private bool IsAvailableInstanstitate<T>(T managerPrefab, T manager)
        {
            return managerPrefab != null && manager == null;
        }
    }
}

