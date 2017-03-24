using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public enum EWWWState
    {
        None,
        Wait,
        Loading,
        Done,
    }

    public enum ELoadPriority
    {
        MostPrior = 20,
        HighPrior = 15,
        PriorLoad = 10,
        Default = 0,
        PostLoad = -10,
    }

    public class WWWLoader
    {
        private string mName = string.Empty;
        private string mOriginName = string.Empty;
        private WWW mWWW = null;
        private EWWWState mState = EWWWState.None;
        private Action<string, WWW> mWWWCallback;
        public ELoadPriority LoadPriority { set; get; }

        public string Name { get { return mName; } }
        public string OriginName { get { return mOriginName; } }
        public EWWWState LoadState { get { return mState; } }
        public float Progress { set; get; }

        public void AddCallback( Action<string, WWW> callback )
        {
            mWWWCallback -= callback;
            mWWWCallback += callback;
        }

        public void ReleaseCallback(Action<string, WWW> callback)
        {
            mWWWCallback -= callback;
        }

        public static WWWLoader CreateInstance( string name, string originName, ELoadPriority priority, bool isRaw, bool isWeb )
        {
            WWWLoader loader = new WWWLoader();
            loader.mName = name;
            loader.mOriginName = originName;
            loader.mWWW = WWWUtil.CreateWWW(name, isRaw, isWeb);
            loader.LoadPriority = priority;
            return loader;
        }

        public void Update()
        {
            Progress = mWWW.progress;
            if( mWWW.isDone )
            {
                mState = EWWWState.Done;
                Progress = 1f;
                if( mWWWCallback != null)
                {
                    Debug.LogError("www " + mOriginName + " is ok and callback");
                    mWWWCallback(mName, mWWW);
                }
            }
        }
        public static int WWWLoaderSort( WWWLoader lhs, WWWLoader rhs )
        {
            if( lhs.LoadPriority > rhs.LoadPriority)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
