using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NumGates.TestBattle
{
    public enum CharacterAlly
    {
        RedAlly,
        OrangeAlly,
        YellowAlly,
        GreenAlly
    }

    public class AssetLoaderAlly : AssetLoader, IAssetLoader
    {
        private Dictionary<string, GameObject> cache = new Dictionary<string, GameObject>();

        public GameObject GetAsset(CharacterAlly ally)
        {
            string guid = GetAssetGUID(ally.ToString(), assetFormat);
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

