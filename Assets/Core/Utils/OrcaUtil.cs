using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Orca
{
    public class OrcaUtil
    {
        public static string Emphasize( string str)
        {
            return " [" + str + "] ";
        }

        public static GameObject GetBone(string boneName, GameObject parent)
        {
            if( string.IsNullOrEmpty(boneName))
            {
                return parent;
            }

            Transform[] trs = parent.transform.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < trs.Length; ++i)
            {
                if (trs[i].name == boneName)
                {
                    return trs[i].gameObject;
                }
            }
            Debug.Log("can't find bone named " + boneName + Emphasize(parent.name));
            return null;
        }

        public static bool RayCastWalkSurface(out Vector3 pos)
        {
            pos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, 1 << (int)ELayerDef.WalkSurface))
            {
                pos = hit.point;
                return true;
            }
            return false;
        }

		public static Vector3 GetFocusPos()
		{
			Vector3 pos = Vector3.zero;
			Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000, 1 << (int)ELayerDef.WalkSurface))
			{
				pos = hit.point;
			}
			return pos;
		}
        
        public static Vector3 String2Vec3(string str)
        {
            float[] array = str.Split(',').Select(p => float.Parse(p)).ToArray<float>();
            return new Vector3(array[0], array[1], array[2]);
        }
        public static string Vec32String(Vector3 vec)
        {
            return "[" + vec.x + ", " + vec.y + ", " + vec.z + "]";
        }

        public static Rect String2Rect(string str)
        {
            float[] array = str.Split(',').Select(p => float.Parse(p)).ToArray<float>();
            return new Rect(array[0], array[1], array[2], array[3]);
        }
        public static string Rect2String(Rect vec)
        {
            return "[" + vec.x + ", " + vec.y + ", " + vec.width + ", " + vec.height + "]";
        }

		public static T Copy<T>( T RealObject )
		{
			using (Stream objctStream = new MemoryStream ()) {
				IFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (objctStream, RealObject);
				objctStream.Seek (0, SeekOrigin.Begin);
				return (T)formatter.Deserialize (objctStream);
			}
		}
    }
}
