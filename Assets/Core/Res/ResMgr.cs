using System;
using System.Collections.Generic;
using UnityEngine;

namespace OrcaCore
{
    public enum ELoadSrc
    {
        DataBase = 0,
        Resources,
        AssetBundle,
    }
    public class ResMgr
    {
        private static ResMgr mInst = null;
        public static ResMgr Inst
        {
            get
            {
                if (mInst == null)
                {
                    mInst = new ResMgr();
                }
                return mInst;
            }
        }
        private static ELoadSrc mLoadSrc = ELoadSrc.DataBase;
        public static ELoadSrc LoadSrc
        {
            set { mLoadSrc = value; }
            get { return mLoadSrc; }
        }

        public static string GetResPath( string assetName )
        {
            string resPath = Application.streamingAssetsPath + @"/" + assetName;
            return resPath;
        }

        public T GetAsset<T>(string path, bool async = true, ELoadPriority priority = ELoadPriority.Default) where T :UnityEngine.Object
        {
            T t = null;
            switch (LoadSrc)
            {
                case ELoadSrc.DataBase:
#if UNITY_EDITOR
                    EditorResMgr.Inst.GetAsset(path, (name, obj) =>
                    {
                        t = obj as T;
                    });
                    break;
#endif
                case ELoadSrc.Resources:
                    ResourceLoaderMgr.Instance.LoadAsset(path, (name, obj) =>
                    {
                        t = obj as T;
                    }, async, priority);
                    break;
                case ELoadSrc.AssetBundle:
                    AssetPool.Instance.GetAsset(path, (name, obj) =>
                     {
                         t = obj as T;
                     }, async, priority);
                    break;
                default:
                    break;
            }
            return t;
        }

        /// <summary>
        /// 所有异步以及加载等待的管理类需要update //
        /// </summary>
        public void Update()
        {
            AssetLoaderMgr.Instance.Update();
            ResourceLoaderMgr.Instance.Update();
            WWWLoaderMgr.Instance.Update();
        }
    }
}
