using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class WWWLoaderMgr
    {
        private static WWWLoaderMgr _instance;
        public static WWWLoaderMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WWWLoaderMgr();
                }
                return _instance;
            }
        }

        const int MAX_DOWN_COUNT = 5;
        /// <summary>
        /// 这里放弃使用dic而用list，主要用于update loader
        /// </summary>
        private List<WWWLoader> mWaitList = new List<WWWLoader>();
        private List<WWWLoader> mDownList = new List<WWWLoader>();
        
        public void LoadWWW( string name, string originName, Action<string, WWW> callback, ELoadPriority priority, bool isRaw, bool isWeb)
        {
            if( string.IsNullOrEmpty(name))
            {
                Debug.LogError("load www but name is empty or null.");
                return;
            }
            WWWLoader loader = _GetWWWLoader(name);
            if( loader == null)
            {
                loader = WWWLoader.CreateInstance(name, originName, priority, isRaw, isWeb);
                mWaitList.Add(loader);
            }
            loader.AddCallback(callback);
        }

        private WWWLoader _GetWWWLoader( string name )
        {
            WWWLoader loader = null;
            WWWLoader[] loaders = mDownList.Where(ld => ld.Name.Equals(name)).ToArray();
            if (loaders.Length > 0)
            {
                loader = loaders[0];
            }
            loaders = mWaitList.Where(ld => ld.Name.Equals(name)).ToArray();
            if (loaders.Length > 0)
            {
                loader = loaders[0];
            }
            return loader;
        }

        public float GetProgress(string name)
        {
            WWWLoader loader = _GetWWWLoader(name);
            if (loader == null)
            {
                return 0;
            }
            else
            {
                return loader.Progress;
            }
        }

        public void Update()
        {
            for( int i = 0; i < mDownList.Count; )
            {
                WWWLoader loader = mDownList[i];
                loader.Update();
                if( loader.LoadState == EWWWState.Done)
                {
                    mDownList.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
            if( mDownList.Count < MAX_DOWN_COUNT && mWaitList.Count > 0)
            {
                mWaitList.Sort(WWWLoader.WWWLoaderSort);
                mDownList.Add(mWaitList[0]);
                mWaitList.RemoveAt(0);
            }
        }

    }
}
