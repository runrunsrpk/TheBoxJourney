using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NumGates.TestBattle
{
    public enum EnemyCharacter
    {
        ENE000,
        ENE001,
        ENE002,
        ENE003
    }

    public class AssetLoaderEnemy : AssetLoader, IAssetLoader
    {
        private Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

        public GameObject GetAsset(EnemyCharacter enemy)
        {
            string guid = GetAssetGUID(enemy.ToString(), assetFormat);
            return cache.ContainsKey(guid) ? cache[guid] : null;
        }

        public IEnumerator LoadAsset(AssetReference reference)
        {
            yield return LoadAssetToCache(reference, cache);
        }

        public IEnumerator LoadAssets(AssetLabelReference assetLabel)
        {
            yield return LoadAssetsToCache(assetLabel, cache);
        }

        public void ReleaseAsset(string guid)
        {
            cache.Remove(guid);
        }

        public void ReleaseAssets()
        {
            cache.Clear();
        }
    }
}



