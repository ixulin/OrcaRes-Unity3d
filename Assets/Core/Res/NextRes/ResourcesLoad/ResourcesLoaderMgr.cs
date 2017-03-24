using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class ResourceLoaderMgr
    {
        private static ResourceLoaderMgr mInst;
        public static ResourceLoaderMgr Instance
        {
            get
            {
                if (mInst == null)
                {
                    mInst = new ResourceLoaderMgr();
                }
                return mInst;
            }
        }

        const int MAX_LOADING_COUNT = 9999;
        private List<ResourceLoader> mLoadingAsset = new List<ResourceLoader>();
        private List<ResourceLoader> mWaitAsset = new List<ResourceLoader>();

        private ResourceLoader _GetResourceLoader(string name)
        {
            ResourceLoader[] loaders = mLoadingAsset.Where(ld => ld.Name.Equals(name)).ToArray();
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

        public void LoadAsset(string path, Action<string, UnityEngine.Object> callback, bool async, ELoadPriority priority)
        {
            int dotIdx = path.IndexOf('.');
            if (dotIdx >= 0)
            {
                path = path.Remove(dotIdx);
            }
            ResourceLoader loader = _GetResourceLoader(path);
            if (loader == null)
            {
                loader = ResourceLoader.CreateInstance(path, callback, async);
                mWaitAsset.Add(loader);
                Update();
            }
        }


        public void Update()
        {
            if (mLoadingAsset.Count < MAX_LOADING_COUNT && mWaitAsset.Count > 0)
            {
                mWaitAsset.Sort(ResourceLoader.ResourceLoaderSort);
                mLoadingAsset.Add(mWaitAsset[0]);
                mWaitAsset.RemoveAt(0);
            }
            for (int i = 0; i < mLoadingAsset.Count;)
            {
                ResourceLoader loader = mLoadingAsset[i];
                loader.Update();
                if (loader.State == ERequestState.Done)
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
