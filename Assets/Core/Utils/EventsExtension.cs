using System;

namespace Orca
{
    public delegate void Action<T, T2, T3, T4, T5>(T obj, T2 obj2, T3 obj3, T4 obj4, T5 obj5);
    public static class EventsExtension
    {
        public static void SafeInvoke(this Action evt)
        {
            if (evt != null)
                evt();
        }

        public static void SafeInvoke<T>(this Action<T> evt, T arg)
        {
            if (evt != null)
                evt(arg);
        }
        public static void SafeInvoke<T, T2>(this Action<T, T2> evt, T arg, T2 arg2)
        {
            if (evt != null)
                evt(arg, arg2);
        }
        public static void SafeInvoke<T, T2, T3>(this Action<T, T2, T3> evt, T arg, T2 arg2, T3 arg3)
        {
            if (evt != null)
                evt(arg, arg2, arg3);
        }
        public static void SafeInvoke<T, T2, T3, T4>(this Action<T, T2, T3, T4> evt, T arg, T2 arg2, T3 arg3, T4 arg4)
        {
            if (evt != null)
                evt(arg, arg2, arg3, arg4);
        }

        public static void SafeInvoke<T, T2, T3, T4, T5>(this Action<T, T2, T3, T4, T5> evt, T arg, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (evt != null)
                evt(arg, arg2, arg3, arg4, arg5);
        }


        public static T SafeInvoke<T>(this Func<T> evt)
        {
            if (evt != null)
                return evt();
            else
                return default(T);
        }
        public static T2 SafeInvoke<T, T2>(this Func<T, T2> evt, T arg)
        {
            if (evt != null)
                return evt(arg);
            else
                return default(T2);
        }
        public static T3 SafeInvoke<T, T2, T3>(this Func<T, T2, T3> evt, T arg, T2 arg2)
        {
            if (evt != null)
                return evt(arg, arg2);
            else
                return default(T3);
        }
        public static T4 SafeInvoke<T, T2, T3, T4>(this Func<T, T2, T3, T4> evt, T arg, T2 arg2, T3 arg3)
        {
            if (evt != null)
                return evt(arg, arg2, arg3);
            else
                return default(T4);
        }
        public static T5 SafeInvoke<T, T2, T3, T4, T5>(this Func<T, T2, T3, T4, T5> evt, T arg, T2 arg2, T3 arg3, T4 arg4)
        {
            if (evt != null)
                return evt(arg, arg2, arg3, arg4);
            else
                return default(T5);
        }
    }

}
