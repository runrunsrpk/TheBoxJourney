using System;
using System.Collections;
using UnityEngine.AddressableAssets;

namespace NumGates
{
    public interface IAssetLoader
    {
        public IEnumerator LoadAsset(AssetReference reference);
        public IEnumerator LoadAssets(AssetLabelReference assetLabel);
        public void ReleaseAsset(string guid);
        public void ReleaseAssets();
    }
}

