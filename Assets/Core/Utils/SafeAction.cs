using System;

namespace Orca
{
    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction
    {
        Action act;

        public static implicit operator Action(SafeAction a)
        {
            return a.act;
        }

        public static implicit operator SafeAction(Action a)
        {
            SafeAction res = new SafeAction();
            res.act = a;
            return res;
        }
        public static SafeAction operator +(SafeAction a, Action b)
        {
            a.act -= b;
            SafeAction res = new SafeAction();
            res.act = a.act + b;

            return res;
        }

        public static SafeAction operator -(SafeAction a, Action b)
        {
            SafeAction res = new SafeAction();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke()
        {
            act.SafeInvoke();
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction<T>
    {
        Action<T> act;

        public static implicit operator Action<T>(SafeAction<T> a)
        {
            return a.act;
        }

        public static implicit operator SafeAction<T>(Action<T> a)
        {
            SafeAction<T> res = new SafeAction<T>();
            res.act = a;
            return res;
        }
        public static SafeAction<T> operator +(SafeAction<T> a, Action<T> b)
        {
            a.act -= b;
            SafeAction<T> res = new SafeAction<T>();
            res.act = a.act + b;

            return res;
        }

        public static SafeAction<T> operator -(SafeAction<T> a, Action<T> b)
        {
            SafeAction<T> res = new SafeAction<T>();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke(T param)
        {
            act.SafeInvoke(param);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction<T, T2>
    {
        Action<T, T2> act;

        public static implicit operator Action<T, T2>(SafeAction<T, T2> a)
        {
            return a.act;
        }

        public static implicit operator SafeAction<T, T2>(Action<T, T2> a)
        {
            SafeAction<T, T2> res = new SafeAction<T, T2>();
            res.act = a;
            return res;
        }

        public static SafeAction<T, T2> operator +(SafeAction<T, T2> a, Action<T, T2> b)
        {
            a.act -= b;
            SafeAction<T, T2> res = new SafeAction<T, T2>();
            res.act = a.act + b;

            return res;
        }

        public static SafeAction<T, T2> operator -(SafeAction<T, T2> a, Action<T, T2> b)
        {
            SafeAction<T, T2> res = new SafeAction<T, T2>();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke(T param, T2 param2)
        {
            act.SafeInvoke(param, param2);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction<T, T2, T3>
    {
        Action<T, T2, T3> act;

        public static implicit operator Action<T, T2, T3>(SafeAction<T, T2, T3> a)
        {
            return a.act;
        }

        public static implicit operator SafeAction<T, T2, T3>(Action<T, T2, T3> a)
        {
            SafeAction<T, T2, T3> res = new SafeAction<T, T2, T3>();
            res.act = a;
            return res;
        }
        public static SafeAction<T, T2, T3> operator +(SafeAction<T, T2, T3> a, Action<T, T2, T3> b)
        {
            a.act -= b;
            SafeAction<T, T2, T3> res = new SafeAction<T, T2, T3>();
            res.act = a.act + b;

            return res;
        }

        public static SafeAction<T, T2, T3> operator -(SafeAction<T, T2, T3> a, Action<T, T2, T3> b)
        {
            SafeAction<T, T2, T3> res = new SafeAction<T, T2, T3>();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke(T param, T2 param2, T3 param3)
        {
            act.SafeInvoke(param, param2, param3);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction<T, T2, T3, T4>
    {
        Action<T, T2, T3, T4> act;

        public static implicit operator Action<T, T2, T3, T4>(SafeAction<T, T2, T3, T4> a)
        {
            return a.act;
        }
        
        public static implicit operator SafeAction<T, T2, T3, T4>(Action<T, T2, T3, T4> a)
        {
            SafeAction<T, T2, T3, T4> res = new SafeAction<T, T2, T3, T4>();
            res.act = a;
            return res;
        }
        
        public static SafeAction<T, T2, T3, T4> operator +(SafeAction<T, T2, T3, T4> a, Action<T, T2, T3, T4> b)
        {
            a.act -= b;
            SafeAction<T, T2, T3, T4> res = new SafeAction<T, T2, T3, T4>();
            res.act = a.act + b;
            return res;
        }

        public static SafeAction<T, T2, T3, T4> operator -(SafeAction<T, T2, T3, T4> a, Action<T, T2, T3, T4> b)
        {
            SafeAction<T, T2, T3, T4> res = new SafeAction<T, T2, T3, T4>();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke(T param, T2 param2, T3 param3, T4 param4)
        {
            act.SafeInvoke(param, param2, param3,param4);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeAction<T, T2, T3, T4, T5>
    {
        Action<T, T2, T3, T4, T5> act;

        public static implicit operator Action<T, T2, T3, T4, T5>(SafeAction<T, T2, T3, T4, T5> a)
        {
            return a.act;
        }

        public static implicit operator SafeAction<T, T2, T3, T4, T5>(Action<T, T2, T3, T4, T5> a)
        {
            SafeAction<T, T2, T3, T4, T5> res = new SafeAction<T, T2, T3, T4, T5>();
            res.act = a;
            return res;
        }
        public static SafeAction<T, T2, T3, T4, T5> operator +(SafeAction<T, T2, T3, T4, T5> a, Action<T, T2, T3, T4, T5> b)
        {
            a.act -= b;
            SafeAction<T, T2, T3, T4, T5> res = new SafeAction<T, T2, T3, T4, T5>();
            res.act = a.act + b;
            return res;
        }

        public static SafeAction<T, T2, T3, T4, T5> operator -(SafeAction<T, T2, T3, T4, T5> a, Action<T, T2, T3, T4, T5> b)
        {
            SafeAction<T, T2, T3, T4, T5> res = new SafeAction<T, T2, T3, T4, T5>();
            res.act = a.act - b;
            return res;
        }

        public void SafeInvoke(T param, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            act.SafeInvoke(param, param2, param3, param4, param5);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeFunc<T>
    {
        Func<T> act;

        public static implicit operator Func<T>(SafeFunc<T> a)
        {
            return a.act;
        }
        
        public static implicit operator SafeFunc<T>(Func<T> a)
        {
            SafeFunc<T> res = new SafeFunc<T>();
            res.act = a;
            return res;
        }
        public static SafeFunc<T> operator +(SafeFunc<T> a, Func<T> b)
        {
            a.act -= b;
            SafeFunc<T> res = new SafeFunc<T>();
            res.act = a.act + b;

            return res;
        }

        public static SafeFunc<T> operator -(SafeFunc<T> a, Func<T> b)
        {
            SafeFunc<T> res = new SafeFunc<T>();
            res.act = a.act - b;
            return res;
        }

        public T SafeInvoke()
        {
            return act.SafeInvoke();
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeFunc<T, T2>
    {
        Func<T, T2> act;

        public static implicit operator Func<T, T2>(SafeFunc<T, T2> a)
        {
            return a.act;
        }
        public static implicit operator SafeFunc<T, T2>(Func<T, T2> a)
        {
            SafeFunc<T, T2> res = new SafeFunc<T, T2>();
            res.act = a;
            return res;
        }
        public static SafeFunc<T, T2> operator +(SafeFunc<T, T2> a, Func<T, T2> b)
        {
            a.act -= b;
            SafeFunc<T, T2> res = new SafeFunc<T, T2>();
            res.act = a.act + b;

            return res;
        }

        public static SafeFunc<T, T2> operator -(SafeFunc<T, T2> a, Func<T, T2> b)
        {
            SafeFunc<T, T2> res = new SafeFunc<T, T2>();
            res.act = a.act - b;
            return res;
        }

        public T2 SafeInvoke(T param)
        {
            return act.SafeInvoke(param);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeFunc<T, T2, T3>
    {
        Func<T, T2, T3> act;

        public static implicit operator Func<T, T2, T3>(SafeFunc<T, T2, T3> a)
        {
            return a.act;
        }
        
        public static implicit operator SafeFunc<T, T2, T3>(Func<T, T2, T3> a)
        {
            SafeFunc<T, T2, T3> res = new SafeFunc<T, T2, T3>();
            res.act = a;
            return res;
        }
        public static SafeFunc<T, T2, T3> operator +(SafeFunc<T, T2, T3> a, Func<T, T2, T3> b)
        {
            a.act -= b;
            SafeFunc<T, T2, T3> res = new SafeFunc<T, T2, T3>();
            res.act = a.act + b;

            return res;
        }

        public static SafeFunc<T, T2, T3> operator -(SafeFunc<T, T2, T3> a, Func<T, T2, T3> b)
        {
            SafeFunc<T, T2, T3> res = new SafeFunc<T, T2, T3>();
            res.act = a.act - b;
            return res;
        }

        public T3 SafeInvoke(T param, T2 param2)
        {
            return act.SafeInvoke(param, param2);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeFunc<T, T2, T3, T4>
    {
        Func<T, T2, T3, T4> act;

        public static implicit operator Func<T, T2, T3, T4>(SafeFunc<T, T2, T3, T4> a)
        {
            return a.act;
        }
        
        public static implicit operator SafeFunc<T, T2, T3, T4>(Func<T, T2, T3, T4> a)
        {
            SafeFunc<T, T2, T3, T4> res = new SafeFunc<T, T2, T3, T4>();
            res.act = a;
            return res;
        }
        public static SafeFunc<T, T2, T3, T4> operator +(SafeFunc<T, T2, T3, T4> a, Func<T, T2, T3, T4> b)
        {
            a.act -= b;
            SafeFunc<T, T2, T3, T4> res = new SafeFunc<T, T2, T3, T4>();
            res.act = a.act + b;

            return res;
        }

        public static SafeFunc<T, T2, T3, T4> operator -(SafeFunc<T, T2, T3, T4> a, Func<T, T2, T3, T4> b)
        {
            SafeFunc<T, T2, T3, T4> res = new SafeFunc<T, T2, T3, T4>();
            res.act = a.act - b;
            return res;
        }

        public T4 SafeInvoke(T param, T2 param2, T3 param3)
        {
            return act.SafeInvoke(param, param2, param3);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }

    /// <summary>
    /// 能避免重复挂事件的默认委托
    /// </summary>
    public struct SafeFunc<T, T2, T3, T4, T5>
    {
        Func<T, T2, T3, T4, T5> act;

        public static implicit operator Func<T, T2, T3, T4, T5>(SafeFunc<T, T2, T3, T4, T5> a)
        {
            return a.act;
        }
        
        public static implicit operator SafeFunc<T, T2, T3, T4, T5>(Func<T, T2, T3, T4, T5> a)
        {
            SafeFunc<T, T2, T3, T4, T5> res = new SafeFunc<T, T2, T3, T4, T5>();
            res.act = a;
            return res;
        }
        public static SafeFunc<T, T2, T3, T4, T5> operator +(SafeFunc<T, T2, T3, T4, T5> a, Func<T, T2, T3, T4, T5> b)
        {
            a.act -= b;
            SafeFunc<T, T2, T3, T4, T5> res = new SafeFunc<T, T2, T3, T4, T5>();
            res.act = a.act + b;

            return res;
        }

        public static SafeFunc<T, T2, T3, T4, T5> operator -(SafeFunc<T, T2, T3, T4, T5> a, Func<T, T2, T3, T4, T5> b)
        {
            SafeFunc<T, T2, T3, T4, T5> res = new SafeFunc<T, T2, T3, T4, T5>();
            res.act = a.act - b;
            return res;
        }

        public T5 SafeInvoke(T param, T2 param2, T3 param3, T4 param4)
        {
            return act.SafeInvoke(param, param2, param3, param4);
        }

        public bool IsNull
        {
            get
            {
                return act == null;
            }
        }

        public void Dispose()
        {
            act = null;
        }
    }
}
