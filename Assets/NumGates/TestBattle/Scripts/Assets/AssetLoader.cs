using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor;

namespace NumGates.TestBattle
{
    public class AssetLoader : MonoBehaviour
    {
        [SerializeField] protected string assetPath;
        [SerializeField] protected string assetFormat;

        // Load single asset to cache
        protected async Task LoadAssetToCache<T>(AssetReference reference, Dictionary<string, T> caches) where T : Object
        {
            AsyncOperationHandle<T> handle = reference.LoadAssetAsync<T>();

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                T result = handle.Result;
                string key = reference.AssetGUID;

                Debug.Log($"Load [{reference}] resoruce succeed {result}");

                if (!caches.ContainsKey(key))
                {
                    //Debug.Log($"Add {result} to caches");
                    caches.Add(key, result);
                }

                Addressables.Release(handle);
            }
            else
            {
                Debug.Log($"Load [{reference}] resoruce fail!");
            }
        }

        // Load multiple asset to cache with multiple thread
        //protected async Task LoadAssetsToCache<T>(AssetLabelReference lableReferences, Dictionary<string, T> cache) where T : Object
        //{
        //    AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(lableReferences, (result) => {});

        //    await handle.Task;

        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        Debug.Log($"Load [{lableReferences}] resoruce succeed {handle.Result}");

        //        foreach (T result in handle.Result)
        //        {
        //            string key = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(result));

        //            if (!cache.ContainsKey(key))
        //            {
        //                Debug.Log($"Add {result} to caches [{key}]");
        //                cache.Add(key, result);
        //            }
        //        }

        //        Addressables.Release(handle);
        //    }
        //    else
        //    {
        //        Debug.Log($"Load [{lableReferences}] resoruce fail!");
        //    }
        //}

        // Load multiple asset to cache with single thread
        protected IEnumerator LoadAssetsToCache<T>(AssetLabelReference lableReferences, Dictionary<string, T> cache) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(lableReferences, (result) => { });

            yield return new WaitUntil(() => handle.IsDone);

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Load [{lableReferences}] resoruce succeed {handle.Result}");

                foreach (T result in handle.Result)
                {
                    string key = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(result));

                    if (!cache.ContainsKey(key))
                    {
                        Debug.Log($"Add {result} to caches [{key}]");
                        cache.Add(key, result);
                    }
                }
            }
            else
            {
                Debug.Log($"Load [{lableReferences}] resoruce fail!");
            }

            Addressables.Release(handle);
        }

        protected string GetAssetGUID(string name, string format)
        {
            return AssetDatabase.AssetPathToGUID($"{assetPath}/{name}.{format}");
        }
    }
}

