using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class LoadingProgressMgr
    {
        private static LoadingProgressMgr _instance;
        public static LoadingProgressMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LoadingProgressMgr();
                }
                return _instance;
            }
        }

        class ProgressInfo
        {
            public string BundleName { set; get; }
            public string Md5Name { set; get; }
            public int Size { set; get; }
            public float Progress { set; get; }
        }

        private List<ProgressInfo> mListProgress = new List<ProgressInfo>();
        private Action<float> mProgressHandler = null;
        private Action mEndHandler = null;
        private int mAllSize = 0;

        public void SetProgress(string[] wwwNames, string[] bundleNames, string[] assetNames, Action<float> handler, Action onEnd = null)
        {
            mListProgress.Clear();
            HashSet<string> wwwNameSet = new HashSet<string>();
            if (wwwNames != null)
            {
                foreach (string name in wwwNames)
                {
                    wwwNameSet.Add(name);
                }
            }
            HashSet<string> bundleNameSet = new HashSet<string>();
            if (bundleNames != null)
            {
                foreach (string bundleName in bundleNames)
                {
                    bundleNameSet.Add(bundleName);
                }
            }
            if (assetNames != null)
            {
                foreach (string assetName in assetNames)
                {
                    string bundleName = AssetMap.Instance.GetBundleName(assetName);
                    bundleNameSet.Add(bundleName);
                }
            }
            HashSet<string> dependsSet = new HashSet<string>();
            foreach (string bundleName in bundleNameSet)
            {
                string[] depends = BundleMap.Instance.GetDependence(bundleName);
                foreach (string dep in depends)
                {
                    dependsSet.Add(dep);
                }
            }
            bundleNameSet.UnionWith(dependsSet);
            int mAllSize = 0;
            foreach (string bundleName in bundleNameSet)
            {
                int size = BundleMap.Instance.GetSize(bundleName);
                string md5Name = BundleMap.Instance.GetMd5Name(bundleName);
                ProgressInfo pi = new ProgressInfo();
                pi.BundleName = bundleName;
                pi.Md5Name = md5Name;
                pi.Size = size;
                pi.Progress = 0;
                mListProgress.Add(pi);
                mAllSize += size;
            }
            mProgressHandler = handler;
            mEndHandler = onEnd;
        }

        public void Update()
        {
            float loadSize = 0;
            foreach (ProgressInfo pi in mListProgress)
            {
                bool cached = BundlePool.Instance.IsBundleCached(pi.BundleName);
                if (cached)
                {
                    pi.Progress = 1;
                }
                else
                {
                    cached = WWWFilePool.Instance.IsWWWFileCached(pi.Md5Name);
                    if (cached)
                    {
                        pi.Progress = 1;
                    }
                    else
                    {
                        pi.Progress = WWWLoaderMgr.Instance.GetProgress(pi.Md5Name);
                    }
                }
                loadSize += pi.Progress * pi.Size;
            }
            if (mProgressHandler != null)
            {
                mProgressHandler(loadSize / mAllSize);
            }
            if( Mathf.Abs(loadSize/mAllSize) - 1.0f <= 0.01f)
            {
                mEndHandler();
            }
        }

    }
}
