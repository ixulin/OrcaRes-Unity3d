using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrcaCore;

namespace OrcaCore
{
    public class AssetMap
    {
        private static AssetMap _instance;
        public static AssetMap Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new AssetMap();
                }
                return _instance;
            }
        }

        private Dictionary<int, string> mDicAssetMap = new Dictionary<int, string>();
        

        public void RegAssetIndex(string assetname, string bundlename)
        {
            if (mDicAssetMap.ContainsKey(assetname.GetHashCode()))
            {
                D.error("ResIdxMap:" + assetname + " is exist in " + GetBundleName(assetname));
                return;
            }
            mDicAssetMap.Add(assetname.GetHashCode(), bundlename);
        }

        public void UnRegisterByAssetName(string assetname)
        {
            mDicAssetMap.Remove(assetname.GetHashCode());
        }

        internal string GetBundleName(string assetname)
        {
            string bundlename;
            if (mDicAssetMap.TryGetValue(assetname.GetHashCode(), out bundlename))
            {
                return bundlename;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
