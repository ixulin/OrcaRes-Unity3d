using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OrcaCore
{
    public enum EMsgType
    {
        None = 0,
        Res,
    }
    public class D
    {
        private static long _frameNum;
        public static long FrameNum
        {
            set { _frameNum = value; }
            get { return _frameNum; }
        }
        public static void error(string str, EMsgType type = EMsgType.None)
        {
            Debug.LogError(_frameNum + " : " + str);
        }
    }
}
