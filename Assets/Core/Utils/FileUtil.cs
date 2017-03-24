using UnityEngine;
using System.IO;

namespace OrcaCore
{
	public class EngineFileUtil
	{
        //IndexMap分隔字符//
        public const string msIndexMapSpliter = ":";
        //扩展名//
        public const string m_fbxExt = ".fbx";
        public const string m_matExt = ".mat";
        public const string m_maxExt = ".max";
        public const string m_prefabExt = ".prefab";
        public const string m_animExt = ".anim";
        public const string m_animSrcFileContain = "@";
        public const string m_txtExt = ".txt";
        public const string m_md5Ext = ".md5.txt";
        public const string m_packExt = ".pack.txt";
        public const string m_curveAnimExt = ".ca.txt";
        public const string m_sceneDataExt = ".scene.txt";
        public const string m_unityExt = ".unity";
        public const string m_sceneExt = ".unity3d";
        public const string m_bundleExt = ".bundle";
        public const string m_bytesExt = ".bytes";
        public const string m_hmdExt = ".unity.txt";
        public const string m_reportExt = ".report.txt";
        public const string m_oggExt = ".ogg";
        public const string m_assetExt = ".asset";
        public const string m_txtidxExt = ".txt_idx";
        public const string m_idxExtPost = "_idx";

        //搜索文件。由于FirePackBundle处于Unity运行时环境中，其.NET版本不支持SearchOption，所以必须由此编辑期模块中的函数处理。//
        public static string[] GetFilesFunc(string path, string searchPattern, bool includeSubDir)
        {
            //if(!Directory.Exists(path))
            //	return new string[]{};
            return Directory.GetFiles(path, searchPattern,
                includeSubDir ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        //获取文件大小。由于FirePackBundle处于Unity运行时环境中，其.NET版本不支持FileInfo.Length，所以必须由此编辑期模块中的函数处理。//
        public static long GetFileLength(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Length;
        }

        //工具函数//
        public static void SaveText(string str, string textFilePath)
        {
            FileStream fileStream = new FileStream(textFilePath, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(str);
            writer.Flush();
            writer.Close();
            fileStream.Close();
        }
        public static string ReadText(string textFilePath)
        {
            if (!File.Exists(textFilePath))
                return string.Empty;
            FileStream fileStream = new FileStream(textFilePath, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            string buf = reader.ReadToEnd();
            reader.Close();
            fileStream.Close();
            return buf;
        }
	}
}
