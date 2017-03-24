using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class AssetLoaderMgr
    {
        private static AssetLoaderMgr _instance;
        public static AssetLoaderMgr Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new AssetLoaderMgr();
                }
                return _instance;
            }
        }

        const int MAX_LOADING_COUNT = 9999;
        private List<AssetLoader> mLoadingAsset = new List<AssetLoader>();
        private List<AssetLoader> mWaitAsset = new List<AssetLoader>();

        private AssetLoader _GetAssetLoader( string name )
        {
            AssetLoader[] loaders = mLoadingAsset.Where(ld => ld.Name.Equals(name)).ToArray();
            if (loaders.Length > 0)
            {
                return loaders[0];
            }
            loaders = mWaitAsset.Where(ld => ld.Name.Equals(name)).ToArray();
            if (loaders.Length > 0)
            {
                return loaders[0];
            }
            return null;
        }

        public void LoadAsset( Asset asset, bool async, ELoadPriority priority )
        {
            AssetLoader loader = _GetAssetLoader(asset.Name);
            if (loader == null)
            {
                loader = AssetLoader.CreateInstance(asset, async);
                mWaitAsset.Add(loader);
                string bundleName = AssetMap.Instance.GetBundleName(asset.Name);
                BundlePool.Instance.GetBundle(bundleName, (cbBundleName, cbBundle) =>
                {
                    loader.OnLoadBundle(cbBundleName, cbBundle);
                }, priority);
                Update();
            }
        }

        
        public void Update()
        {
            if (mLoadingAsset.Count < MAX_LOADING_COUNT && mWaitAsset.Count > 0)
            {
                mWaitAsset.Sort(AssetLoader.AssetLoaderSort);
                mLoadingAsset.Add(mWaitAsset[0]);
                mWaitAsset.RemoveAt(0);
            }
            for ( int i = 0; i < mLoadingAsset.Count; )
            {
                AssetLoader loader = mLoadingAsset[i];
                loader.Update();
                if( loader.State == EAssetState.Done)
                {
                    mLoadingAsset.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }

    }
}
