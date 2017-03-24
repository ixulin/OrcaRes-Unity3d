using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrcaCore
{
    public enum JsonFieldTypes
    {
        Null,
        UnEditable,
        BindPoint,
        Animation,
        ActEvent,
        HasChildren,
        PackType,
        PreProcess,
        PostProcess,
        ValidFuncs,
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class JsonFieldAttribute:Attribute
    {
        private JsonFieldTypes _fieldType;
        public JsonFieldAttribute(JsonFieldTypes type)
        {
            this._fieldType = type;
        }

        public JsonFieldTypes FieldTypeName
        {
            get{return _fieldType;}
        }

        public static JsonFieldTypes GetFieldFlag(object obj, string proName)
        {
            Type t = obj.GetType();
            foreach (System.Reflection.PropertyInfo p in t.GetProperties())
            {
                if (p.Name == proName)
                {
                    return GetFieldFlag(p);
                }
            }

            return JsonFieldTypes.Null;
        }

        public static JsonFieldTypes GetFieldFlag(System.Reflection.PropertyInfo pro)
        {
            object[] atts = pro.GetCustomAttributes(typeof(JsonFieldAttribute), false);
            if (atts.Length > 0)
            {
                JsonFieldAttribute attr = atts[0] as JsonFieldAttribute;
                return attr.FieldTypeName;
            }
            return JsonFieldTypes.Null;
        }
    }
}
