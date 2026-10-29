using System.Collections.Generic;

namespace _Project.Core.Scripts.Infrastructure
{
    public sealed class ObjectPool<T> where T : class
    {
        private readonly Stack<T> _free = new Stack<T>();
        private readonly HashSet<T> _freeSet = new HashSet<T>(); 

        public int Count => _free.Count;
        
        public void Return(T item)
        {
            if (item == null) return;
            if (!_freeSet.Add(item)) return;

            _free.Push(item);
        }
        
        public bool TryGet(out T item)
        {
            if (_free.Count > 0)
            {
                item = _free.Pop();
                _freeSet.Remove(item);
                return true;
            }

            item = null;
            return false;
        }

        public void Clear()
        {
            _free.Clear();
            _freeSet.Clear();
        }
    }
}