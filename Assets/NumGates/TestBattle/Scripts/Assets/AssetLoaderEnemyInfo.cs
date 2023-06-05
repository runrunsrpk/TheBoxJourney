using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NumGates.TestBattle
{
    public class AssetLoaderEnemyInfo : AssetLoader, IAssetLoader
    {
        private Dictionary<string, EnemyInfo> cache = new Dictionary<string, EnemyInfo>();

        public EnemyInfo GetAsset(EnemyCharacter enemy)
        {
            string guid = GetAssetGUID($"{enemy}Info", assetFormat);
            return cache.ContainsKey(guid) ? cache[guid] : null;
        }

        public List<EnemyInfo> GetAllAssets()
        {
            List<EnemyInfo> temp = new List<EnemyInfo>();

            foreach(KeyValuePair<string, EnemyInfo> pair in cache)
            {
                temp.Add(pair.Value);
            }

            return temp;
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

