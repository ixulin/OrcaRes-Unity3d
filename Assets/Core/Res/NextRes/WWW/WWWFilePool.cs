using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public class WWWFilePool
    {
        private static WWWFilePool _instance;
        public static WWWFilePool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WWWFilePool();
                }
                return _instance;
            }
        }
        private Dictionary<string, System.Object> mDicObject = new Dictionary<string, System.Object>();

        public bool IsWWWFileCached( string name )
        {
            System.Object obj;
            if( mDicObject.TryGetValue(name, out obj))
            {
                return true;
            }
            return false;
        }

        public void GetTextFile(string name, Action<string, string> func, ELoadPriority priority = ELoadPriority.Default, bool isRaw = true, bool isWeb = false)
        {
            System.Object obj = null;
            if (mDicObject.TryGetValue(name, out obj))
            {
                func(name, obj as string);
            }
            else
            {
                Debug.LogError("load text " + name);
                WWWLoaderMgr.Instance.LoadWWW(name, name, (cbName, cbWWW) =>
                {
                    Debug.LogError("on load text " + name);
                    if (cbWWW.text == null)
                    {
                        Debug.LogError("get text from WWW, but www.text == null.");
                    }
                    else
                    {
                        mDicObject.Add(name, cbWWW.text);
                        func(cbName, cbWWW.text);
                    }
                }, priority, isRaw, isWeb);
            }
        }

        public void GetBytesFile(string name, Action<string, byte[]> func, ELoadPriority priority = ELoadPriority.Default, bool isRaw = true, bool isWeb = false)
        {
            System.Object obj = null;
            if (mDicObject.TryGetValue(name, out obj))
            {
                func(name, obj as byte[]);
            }
            else
            {
                Debug.LogError("load bytes " + name);
                WWWLoaderMgr.Instance.LoadWWW(name, name, (cbName, cbWWW) =>
                {
                    Debug.LogError("on load bytes " + name);
                    if (cbWWW.bytes == null)
                        {
                            Debug.LogError("get bytes from WWW, but www.bytes == null.");
                        }
                        else
                        {
                            mDicObject.Add(name, cbWWW.bytes);
                            func(cbName, cbWWW.bytes);
                        }
                    }, priority, isRaw, isWeb);
            }
        }

        public void GetWWWFile(string name, Action<string, WWW> func, ELoadPriority priority = ELoadPriority.Default, bool isRaw = true, bool isWeb = false)
        {
            System.Object obj = null;
            if (mDicObject.TryGetValue(name, out obj))
            {
                func(name, obj as WWW);
            }
            else
            {
                Debug.LogError("load www " + name);
                WWWLoaderMgr.Instance.LoadWWW(name, name, (cbName, cbWWW) =>
                {
                    Debug.LogError("on load www " + name);
                    mDicObject.Add(name, cbWWW);
                        func(cbName, cbWWW);
                    }, priority, isRaw, isWeb);
            }
        }

        public void RemoveWWWFile(string name)
        {
            mDicObject.Remove(name);
        }
    }
}
