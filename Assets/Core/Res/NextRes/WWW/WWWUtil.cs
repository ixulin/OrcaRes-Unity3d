using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    /// <summary>
    /// 加载的url类型
    /// </summary>
    public enum EUrlType
    {
        Default,
        Streaming,
        Web,
    }
    public class WWWUtil
    {
        /// <summary>
        /// 从网络加载的url地址
        /// </summary>
        public static string GAME_RES_URL { set; get; }
        /// <summary>
        /// 是否禁用Cache
        /// </summary>
        public static bool DisableCache { set; get; }

        /// <summary>
        /// 异步加载的优先级
        /// </summary>
        public static bool LowAsyncLoadPriority { set; get; }
        /// <summary>
        /// 创建www
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isRaw">新加载资源</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static WWW CreateWWW( string name, bool isRaw, bool isWeb )
        {
            WWW www;
            string path = string.Empty;
            if( isWeb )
            {
                path = GAME_RES_URL + "/" + name;
            }
            else if( Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.streamingAssetsPath + "/" + name;
            }
            else
            {
                path = "file:///" + Application.dataPath + "/../../bin/res/" + name;
            }

            if( isRaw || DisableCache)
            {
                www = new WWW(path);
            }
            else
            {
                www = WWW.LoadFromCacheOrDownload(path, 1);
            }
            if( LowAsyncLoadPriority )
            {
                www.threadPriority = ThreadPriority.Low;
            }
            else
            {
                www.threadPriority = ThreadPriority.High;
            }
            return www;
        }
    }
}
