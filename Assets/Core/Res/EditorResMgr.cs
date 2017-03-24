using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
namespace OrcaCore
{
    public class EditorResMgr
    {
        private static EditorResMgr mInst;
        public static EditorResMgr Inst
        {
            get
            {
                if( mInst == null )
                {
                    mInst = new EditorResMgr();
                }
                return mInst;
            }
        }

        private Dictionary<string, string> mDicFilePath = new Dictionary<string, string>();

        public void GetAsset(string name, Action<string, UnityEngine.Object> func, ELoadPriority priority = ELoadPriority.Default)
        {
            UnityEngine.Object obj = null;
            string fullName = "";
            if (!mDicFilePath.TryGetValue(name, out fullName))
            {
                foreach (string file in UnityEditor.AssetDatabase.GetAllAssetPaths())
                {
                    string fileName = Path.GetFileName(file);
                    if (fileName == name)
                    {
                        fullName = file;
                        mDicFilePath.Add(name, fullName);
                        break;
                    }
                }
            }

            if ( string.IsNullOrEmpty(fullName))
            {
                Debug.LogError("Can't find any asset named " + name);
            }
            else
            {
                obj = AssetDatabaseLoad(fullName);
                if (func != null)
                {
                    func(name, obj);
                }
            }
        }

        public UnityEngine.Object AssetDatabaseLoad(string fullName)
        {
            UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath(fullName, typeof(UnityEngine.Object));
            return obj;
        }
    }
}

#endif

