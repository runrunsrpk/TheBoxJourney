using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NumGates.TestBattle
{
    public class AssetLoaderAllyInfo : AssetLoader, IAssetLoader
    {
        private Dictionary<string, AllyInfo> cache = new Dictionary<string, AllyInfo>();

        public AllyInfo GetAsset(AllyCharacter ally)
        {
            string guid = GetAssetGUID($"{ally}_Info", assetFormat);
            return cache.ContainsKey(guid) ? cache[guid] : null;
        }

        public List<AllyInfo> GetAllAssets()
        {
            List<AllyInfo> temp = new List<AllyInfo>();

            foreach(KeyValuePair<string, AllyInfo> pair in cache)
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

