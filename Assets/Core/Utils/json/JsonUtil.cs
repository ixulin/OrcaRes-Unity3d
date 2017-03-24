using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using fastJSON;
using System.Reflection;

namespace OrcaCore
{
	public class JsonUtil
	{
        public static void InitJson()
        {
            JSON.RegisterCustomType(typeof(UnityEngine.Vector3), Vector3Json.SerilizeVector3, Vector3Json.DserializeVector3);
            JSON.RegisterCustomType(typeof(UnityEngine.Quaternion), QuaternionJson.SerilizeQuaternion, QuaternionJson.DserializeQuaternion);
            JSON.RegisterCustomType(typeof(UnityEngine.Rect), JsonCustom.SerilizeRect, JsonCustom.DserializeRect);
        }
        public static PropertyInfo[] GetProperties(object obj)
        {
            Type t = obj.GetType();
            return t.GetProperties();
        }
        public static Type GetPropertyType(object obj, string proName)
        {
            Type t = obj.GetType();
            foreach (System.Reflection.FieldInfo p in t.GetFields())
            {
                if (p.Name == proName)
                    return p.FieldType;
            }

            return null;
        }

        public static string[] StringToStringArray(string s, char splitchar)
        {
            if (s.Length == 0)
                return new string[] { };
            string[] sa = s.Split(splitchar);
            return sa;
        }
        public static string[] StringToStringArray(string s)
        {
            return StringToStringArray(s, '|');
        }
        public static string StringArrayToString(string[] sa)
        {
            string s = "";
            for (int i = 0; i < sa.Length; i++)
            {
                string str = sa[i];
                s += str;
                if (i < sa.Length - 1)
                    s += "|";
            }
            return s;
        }

        public static string ArrayToString<T>(T[] sa)
        {
            if (sa == null)
                return "";
            string s = "";
            for (int i = 0; i < sa.Length; i++)
            {
                string str = sa[i].ToString();
                s += str;
                if (i < sa.Length - 1)
                    s += ",";
            }
            return s;
        }

        public static T[] StringToArray<T>(string s, char splitchar = ',', int precision = 2)
        {
            if (s.Length == 0)
                return new T[] { };
            string[] sa = s.Split(splitchar);
            List<T> fl = new List<T>();
            foreach (string str in sa)
            {
                object f;
                if(typeof(T) == typeof(float))
                {
                    f = (float)Math.Round(float.Parse(str), precision);
                }
                else if (typeof(T) == typeof(double))
                {
                    f = Math.Round(float.Parse(str), precision);
                }
                else if(typeof(T) == typeof(int))
                {
                    f = int.Parse(str);
                }
                else
                {
                    f = str;
                }
                    
                fl.Add((T)f);
            }
            return fl.ToArray();
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
        public static object ReadJsonObject(string path)
        {
            //读文件//
            string buf = JsonUtil.ReadText(path);
            return JSON.ToObject(buf);
        }

        public static object ConvertData(string data, Type type)
        {
            if (type == typeof(string))
                return data;
            else
            {
                if (data == string.Empty)
                    return null;
                if (type == typeof(int))
                    return int.Parse(data);
                else if (type == typeof(short))
                    return short.Parse(data);
                else if (type == typeof(float))
                    return float.Parse(data);
                else if (type == typeof(string[]))
                    return JsonUtil.StringToStringArray(data);
                else if (type == typeof(float[]))
                    return JsonUtil.StringToArray<float>(data);
                else if (type == typeof(int[]))
                    return JsonUtil.StringToArray<int>(data);
                else if (type == typeof(double))
                    return double.Parse(data);
                else if (type == typeof(bool))
                {
                    if (data == "0" || data.ToLower() == "false" || data == string.Empty)
                        return false;
                    else
                        return true;
                }
                else
                    return data;
            }
        }
	}
}
