using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class AssetPool
    {
        private static AssetPool _instance;
        private Dictionary<string, Asset> mDicAsset = new Dictionary<string, Asset>();

        public static AssetPool Instance
        {
            get 
            {
                if( _instance == null)
                {
                    _instance = new AssetPool();
                }
                return _instance;
            }
        }
        public void GetAsset(string name, Action<string, UnityEngine.Object> callback, bool async = true, ELoadPriority priority = ELoadPriority.Default)
        {
            Asset asset = null;
            if (mDicAsset.TryGetValue(name, out asset))
            {
                if (callback != null)
                {
                    if (asset.AssetValid)
                    {
                        callback(name, asset.GetObject());
                    }
                    else
                    {
                        asset.AddCallback(callback);
                    }
                }
            }
            else
            {
                Debug.LogError("get asset " + name);
                asset = Asset.CreateInstance(name);
                if (name.Contains(".ttf") || name.Contains(".TTF"))
                {
                    int xx = 0;
                    ++xx;
                }
                asset.AddCallback(callback);
                mDicAsset.Add(name, asset);
                AssetLoaderMgr.Instance.LoadAsset(asset, async, priority);
            }
        }

        public void ReleaseAssetCallback( string name, Action<string, UnityEngine.Object> callback)
        {
            Asset asset = null;
            if (mDicAsset.TryGetValue(name, out asset))
            {
                asset.ReleaseCallback(callback);
            }
        }

        public void ReleaseAsset(string name, bool includeSelf = false)
        {
            Asset asset = null;
            if (mDicAsset.TryGetValue(name, out asset))
            {
                asset.ReleaseAsset(includeSelf);
                if (includeSelf)
                {
                    mDicAsset.Remove(name);
                }
            }
        }
        public void ReleaseAssetRefrence( string name, UnityEngine.Object obj)
        {
            Asset asset = null;
            if (mDicAsset.TryGetValue(name, out asset))
            {
                asset.ReleaseAssetReference(obj);
            }
        }
    }
}
