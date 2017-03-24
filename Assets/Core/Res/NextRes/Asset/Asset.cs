using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class Asset
    {
        public static Func<UnityEngine.GameObject, bool> PrefabUnInstantiateRule;

        private MemoryPool<UnityEngine.Object> mObjectPool = new MemoryPool<UnityEngine.Object>();
        private UnityEngine.Object mObject;
        private string mName;
        public string Name { get { return mName; } }
        public int Reference { set; get; }
        public bool HasNoReference { get { return Reference <= 0; } }
        public bool AssetValid { get { return mObject != null; } }
        private Action<string, UnityEngine.Object> mObjectCallback;

        public void AddCallback(Action<string, UnityEngine.Object> callback)
        {
            mObjectCallback -= callback;
            mObjectCallback += callback;
        }
        public void ReleaseCallback(Action<string, UnityEngine.Object> callback)
        {
            mObjectCallback -= callback;
        }

        public static Asset CreateInstance(string name)
        {
            Asset asset = new Asset();
            asset.mName = name;
            return asset;
        }

        public void OnAssetLoad( string name, UnityEngine.Object obj )
        {
            if( name.Contains("nvTD"))
            {
                Debug.LogError("OnAssetLoad " + name);
            }
            mObject = obj;
            if( mObjectCallback != null)
            {
                if (name.Contains("nvTD"))
                {
                    Debug.LogError("OnAssetLoad callback " + name);
                }
                mObjectCallback(mName, GetObject());
            }
        }

        public UnityEngine.Object GetObject()
        {
            if (AssetValid)
            {
                Reference++;
                UnityEngine.Object obj = mObjectPool.Alloc();
                if (obj != null)
                {
                    if (obj is GameObject)
                    {
                        (obj as GameObject).SetActive(true);
                    }
                    return obj;
                }
                else
                {
                    return InstanceAsset(mObject, mName);
                }
            }
            else
            {
                return null;
            }
        }
        public static UnityEngine.Object InstanceAsset(UnityEngine.Object obj, string name)
        {
            if (name.IndexOf(".exr") >= 0)
            {
                return obj;
            }
            if (needInstance(obj))
            {
                return GameObject.Instantiate(obj);
            }
            return obj;
        }

        /// <summary>
        /// 判定是否需要实例化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool needInstance(UnityEngine.Object obj)
        {
            if (obj is GameObject)
            {
                if (PrefabUnInstantiateRule != null && PrefabUnInstantiateRule(obj as GameObject))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void ReleaseAssetReference(UnityEngine.Object obj)
        {
            Reference--;
            if (obj is GameObject)
            {
                (obj as GameObject).SetActive(false);
                (obj as GameObject).transform.parent = null;
            }
            if (needInstance(obj))
            {
                if (mObjectPool.Free(obj))
                {
                    return;
                }
                UnityEngine.Object.Destroy(obj);
            }
        }

        public bool CanRelease()
        {
            if (mObject == null)
            {
                return true;
            }
            if (mObject is TextAsset || mObject is Font)
            {
                return false;
            }
            return true;
        }

        public void ReleaseAsset(bool includeSelf)
        {
            mObjectCallback = null;
            while (true)
            {
                UnityEngine.Object obj = mObjectPool.Alloc();
                if (obj != null)
                {
                    UnityEngine.Object.Destroy(obj);
                }
                else
                {
                    break;
                }
            }
            if (includeSelf)
            {
                if (mObject != null)
                {
                    GameObject.DestroyImmediate(mObject, true);
                }
                mObjectPool.Dispose();
                mObject = null;
            }
        }
    }
}
