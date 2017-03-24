using System;
using System.Collections.Generic;

namespace OrcaCore
{
    public class MemoryPool<T> where T : class
    {
        public MemoryPool(int maxSize = 10)
        {
            _maxSize = maxSize;
        }
        public T Alloc()
        {
            T t = null;
            if (_objs.Count > 0)
                t = _objs.Dequeue();
            return t;
        }
        public bool Free(T t)
        {
            if (_objs.Count < _maxSize)
            {
                _objs.Enqueue(t);
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            _objs.Clear();
        }
        public int Count { get { return _objs.Count; } }
        private Queue<T> _objs = new Queue<T>();
        private int _maxSize = 10;
    }
}