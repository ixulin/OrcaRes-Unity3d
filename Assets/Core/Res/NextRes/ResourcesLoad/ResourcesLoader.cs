using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OrcaCore
{
    public enum ERequestState
    {
        None = 0,
        Wait,
        BundleOk,
        Loading,
        Done,
    }
    public class ResourceLoader
    {
        private ERequestState mState = ERequestState.None;
        private bool mAsync = true;
        private ResourceRequest mRequest;
        private string mName = string.Empty;
        private Action<string, UnityEngine.Object> mCallback;
        public Action<string, UnityEngine.Object> Callback
        {
            set
            {
                mCallback -= value;
                mCallback += value;
            }
            get { return mCallback; }
        }

        public string Name
        {
            get { return mName; }
        }
        public ERequestState State
        {
            get { return mState; }
            set { mState = value; }
        }

        public static ResourceLoader CreateInstance(string name, Action<string, UnityEngine.Object> callback, bool async)
        {
            ResourceLoader loader = new ResourceLoader();
            loader.mName = name;
            loader.mAsync = async;
            loader.mState = ERequestState.Wait;
            loader.Callback += callback;
            return loader;
        }

        public void Update()
        {
            if (mName.Contains("nvTD"))
            {
                int xx = 0;
                ++xx;
                Debug.LogError("Update " + mName + " State = " + mState.ToString());
            }
            if (mState == ERequestState.BundleOk)
            {
                if (mName.Contains("nvTD"))
                {
                    int xx = 0;
                    ++xx;
                    D.error("resources load " + mName);
                }
                if (mAsync)
                {
                    mRequest = Resources.LoadAsync(mName);
                    mState = ERequestState.Loading;
                }
                else
                {
                    UnityEngine.Object obj = Resources.Load(mName);
                    mState = ERequestState.Done;
                    Debug.LogError("on load asset " + mName);
                    if( mCallback != null )
                    {
                        mCallback(mName, obj);
                    }
                }
            }
            if (mState == ERequestState.Loading && mRequest.isDone)
            {
                if (mName.Contains("nvTD"))
                {
                    int xx = 0;
                    ++xx;
                }
                mState = ERequestState.Done;
                if( mCallback != null )
                {
                    mCallback( mName, mRequest.asset);
                }
            }
        }

        public static int ResourceLoaderSort(ResourceLoader lhs, ResourceLoader rhs)
        {
            if (lhs.State > rhs.State)
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
