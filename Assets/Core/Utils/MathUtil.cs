using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Orca
{
    public class MathUtil
    {
        public static bool InCircle(Vector3 pos, Vector3 center, float radius )
        {
            pos.y = 0;
            center.y = 0;
            return (pos - center).sqrMagnitude <= radius * radius;
        }

        public static Vector3 GetDir( Vector3 end, Vector3 start )
        {
            return end - start;
        }

        public static bool IsEqual(float f1, float f2)
        {
            return System.Math.Abs(f1 - f2) < 0.00001f;
        }

        public static bool IsEqualXZ(Vector3 v1, Vector3 v2)
        {
            return IsEqual(v1.x, v2.x) && IsEqual(v1.z, v2.z);
        }

        public static bool IsEqual(Vector3 v1, Vector3 v2)
        {
            return IsEqual(v1.x, v2.x) && IsEqual(v1.y, v2.y) && IsEqual(v1.z, v2.z);
        }

        public static bool PosEqualXZ( Vector3 pos1, Vector3 pos2 )
        {
            pos1.y = 0;
            pos2.y = 0;
            return (pos1 - pos2).magnitude < 0.2f;
        }

        public static bool PosEqual(Vector3 pos1, Vector3 pos2)
        {
            return (pos1 - pos2).magnitude < 0.2f;
        }


        public static float GetDisXZ( Vector3 pos1, Vector3 pos2 )
        {
            pos1.y = 0;
            pos2.y = 0;
            return (pos1 - pos2).magnitude;
        }

        public static float GetDis(Vector3 pos1, Vector3 pos2)
        {
            return (pos1 - pos2).magnitude;
        }

        public static bool Arrived( Vector3 currpos, Vector3 lastPos, Vector3 targetPos)
        {
            Vector3 ab = targetPos - lastPos;
            if( ab.sqrMagnitude <= 0.0001f )
            {
                return true;
            }
            Vector3 ac = currpos - lastPos;
            float dot = Vector3.Dot(ab, ac);
            dot = dot / ac.magnitude;
            if( dot > 0 && dot < ac.magnitude )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
