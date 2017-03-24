using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class Bundle
    {
        private string mName;
        private AssetBundle mAssetBundle = null;

        public AssetBundle AssetBundle
        {
            get { return mAssetBundle; }
        }

        public static Bundle CreateInstance( string name )
        {
            Bundle bundle = new Bundle();
            bundle.mName = name;
            return bundle;
        }

        public void OnLoadWWW( string name, WWW www)
        {
            mAssetBundle = www.assetBundle;
        }
    }
}
