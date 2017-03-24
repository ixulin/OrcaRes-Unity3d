using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace OrcaCore
{
    public class BundleInfo
    {
        public string Md5Name { set; get; }
        public int Size { set; get; }
        public string Md5 { set; get; }
        public HashSet<string> AssetFiles { set; get; }
        public HashSet<string> DependsOn { set; get; }
        public string FirstAsset { set; get; }
    }
    public class BundleMap
    {
        private static BundleMap _instance;
        public static BundleMap Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BundleMap();
                }
                return _instance;
            }
        }

        const string BUNDLE_INFO_PREFIX = "&BundleInfo%";
        const char splitchar = ':';
        private Dictionary<string, BundleInfo> mDicBundleMap = new Dictionary<string, BundleInfo>();

        public int GetSize(string name)
        {
            int size = 0;
            BundleInfo bi = GetBundleInfo(name);
            if (bi != null)
            {
                size = bi.Size;
            }
            else
            {
                Debug.LogError(name + " is not in bundle map");
            }
            return size;
        }

        public string GetMd5Name(string name)
        {
            string md5name = string.Empty;
            BundleInfo bi = GetBundleInfo(name);
            if (bi != null)
            {
                md5name = bi.Md5Name;
            }
            else
            {
                Debug.LogError(name + " is not in bundle map");
            }
            return md5name;
        }

        public string[] GetDependence(string name)
        {
            BundleInfo bi;
            if (!mDicBundleMap.TryGetValue(name, out bi))
            {
                return new string[0];
            }
            else
            {
                return bi.DependsOn.ToArray();
            }
        }

        private BundleInfo GetBundleInfo(string name)
        {
            BundleInfo bi;
            if (!mDicBundleMap.TryGetValue(name, out bi))
            {
                return null;
            }
            else
            {
                return bi;
            }
        }

        public void Read( string stream)
        {
            mDicBundleMap.Clear();

            StringReader sr = new StringReader(stream);
            string line = sr.ReadLine();
            while (null != line)
            {
                if (line.StartsWith(BUNDLE_INFO_PREFIX))
                {
                    byte[] buf = Convert.FromBase64String(line.Substring(BUNDLE_INFO_PREFIX.Length + 1));
                    MemoryStream ms = new MemoryStream(buf);
                    BinaryReader br = new BinaryReader(ms);

                    int bundleCnt = br.ReadInt32();
                    for (int i = 0; i < bundleCnt; i++)
                    {
                        string name = br.ReadString();
                        int size = br.ReadInt32();
                        int assetCnt = br.ReadInt32();
                        HashSet<string> assets = new HashSet<string>();
                        string firstAsset = null;
                        for (int j = 0; j < assetCnt; j++)
                        {
                            string assetName = br.ReadString();
                            assets.Add(assetName);
                            if (firstAsset == null)
                            {
                                firstAsset = assetName;
                            }
                            AssetMap.Instance.RegAssetIndex(assetName, name);
                        }
                        BundleInfo bi = GetBundleInfo(name);
                        if( bi != null)
                        {
                            bi.AssetFiles = assets;
                            bi.FirstAsset = firstAsset;
                            int dependCnt = br.ReadInt32();
                            HashSet<string> depends = new HashSet<string>();
                            for (int j = 0; j < dependCnt; j++)
                            {
                                string depName = br.ReadString();
                                depends.Add(depName);
                            }
                            bi.DependsOn = depends;
                        }
                    }
                }
                else
                {
                    string[] strs = OrcaCore.JsonUtil.StringToStringArray(line, splitchar);
                    string name = strs[0];
                    BundleInfo bi = GetBundleInfo(name);
                    if( bi == null)
                    {
                        bi = new BundleInfo();
                        mDicBundleMap.Add(name, bi);
                    }
                    bi.Md5Name = strs[1];
                    bi.Size = int.Parse(strs[2]);
                    bi.Md5 = strs[3];
                }
                line = sr.ReadLine();
            }
        }

    }
}
