using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public enum EAssetState
    {
        None = 0,
        Wait,
        BundleOk,
        Loading,
        Done,
    }
    public class AssetLoader
    {
        private Asset mAsset = null;
        private EAssetState mState = EAssetState.None;
        private Bundle mBundle = null;
        private bool mAsync = true;
        private AssetBundleRequest mRequest;

        public string Name
        {
            get
            {
                if (mAsset == null)
                {
                    return string.Empty;
                }
                else
                {
                    return mAsset.Name;
                }
            }
        }
        public EAssetState State
        {
            get { return mState; }
            set { mState = value; }
        }
        public static AssetLoader CreateInstance( Asset asset, bool async )
        {
            AssetLoader loader = new AssetLoader();
            loader.mAsync = async;
            loader.mAsset = asset;
            loader.mState = EAssetState.Wait;
            return loader;
        }
        
        public void Update()
        {
            if( mAsset.Name.Contains("nvTD"))
            {
                int xx = 0;
                ++xx;
                Debug.LogError("Update " + mAsset.Name + " State = " + mState.ToString());
            }
            if( mState == EAssetState.BundleOk)
            {
                if (mAsset.Name.Contains("nvTD"))
                {
                    int xx = 0;
                    ++xx;
                    Debug.LogError("OnAssetLoad " + mAsset.Name);
                }
                if (false)
                {
                    mRequest  = mBundle.AssetBundle.LoadAssetAsync(mAsset.Name);
                    mState = EAssetState.Loading;
                }
                else
                {
                    UnityEngine.Object obj = mBundle.AssetBundle.LoadAsset(mAsset.Name);
                    mState = EAssetState.Done;
                    Debug.LogError("on load asset " + mAsset.Name);
                    mAsset.OnAssetLoad( mAsset.Name, obj);
                }
            }
            if (mState == EAssetState.Loading && mRequest.isDone)
            {
                if (mAsset.Name.Contains("nvTD"))
                {
                    int xx = 0;
                    ++xx;
                }
                mState = EAssetState.Done;
                mAsset.OnAssetLoad(mAsset.Name, mRequest.asset);
            }
        }

        public static int AssetLoaderSort( AssetLoader lhs, AssetLoader rhs)
        {
            if( lhs.State > rhs.State)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public void OnLoadBundle( string bundleName, Bundle bundle )
        {
            Debug.LogError("asset loader " + mAsset.Name + "on load bundle " + bundleName);
            mState = EAssetState.BundleOk;
            mBundle = bundle;
        }
    }
}
