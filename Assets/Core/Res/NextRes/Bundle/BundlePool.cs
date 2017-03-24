using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class BundlePool
    {
        private static BundlePool _instance;
        public static BundlePool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BundlePool();
                }
                return _instance;
            }
        }

        private Dictionary<string, Bundle> mDicBundle = new Dictionary<string, Bundle>();

        public bool IsBundleCached(string name)
        {
            Bundle bundle = null;
            if (mDicBundle.TryGetValue(name, out bundle))
            {
                return true;
            }
            return false;
        }
        
        public void GetBundle(string bundleName, Action<string, Bundle> callback, ELoadPriority priority, bool isRaw = true, bool isWeb = false)
        {
            Bundle bundle = null;
            if (mDicBundle.TryGetValue(bundleName, out bundle))
            {
                callback(bundleName, bundle);
            }
            else
            {
                string[] depends = BundleMap.Instance.GetDependence(bundleName);
                if( depends.Length > 0)
                {
                    int dependsCount = depends.Length;
                    for (int i = 0; i < depends.Length; ++i)
                    {
                        string dependBundleName = depends[i];
                        GetBundle(dependBundleName, (cbName, cbBundle) =>
                        {
                            if(--dependsCount <= 0)
                            {
                                LoadBundle(bundleName, callback, priority, isRaw, isWeb);
                            }
                        }, priority);
                        
                    }
                }
                else
                {
                    LoadBundle(bundleName, callback, priority, isRaw, isWeb);
                }
                
            }
        }

        private void LoadBundle(string bundleName, Action<string, Bundle> callback, ELoadPriority priority, bool isRaw, bool isWeb)
        {
            Bundle bundle = null;
            string md5Name = BundleMap.Instance.GetMd5Name(bundleName);
            Debug.LogError("load bundle " + bundleName + " : " + md5Name);
            WWWLoaderMgr.Instance.LoadWWW(md5Name, bundleName, (cbBundleName, cbWWW) =>
            {
                if (bundleName.Contains("ui_font"))
                {
                    int xx = 0;
                    ++xx;
                }
                Debug.LogError("on load bundle ok " + bundleName);
                if (!mDicBundle.TryGetValue(bundleName, out bundle))
                {
                    bundle = Bundle.CreateInstance(bundleName);
                    mDicBundle.Add(bundleName, bundle);
                }
                bundle.OnLoadWWW(bundleName, cbWWW);
                callback(bundleName, bundle);
            }, priority, isRaw, isWeb);
        }
    }
}
