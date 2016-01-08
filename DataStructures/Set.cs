using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Set<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public Set()
        {
        }

        public Set(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public void Add(T item)
        {
            if (Contains(item))
            {
                throw new InvalidOperationException("Item already exists in Set");
            }
            _items.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public Set<T> Union(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Intersection(Set<T> other)
        {
            Set<T> result = new Set<T>();

            foreach (T item in _items)
            {
                if (other._items.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Difference(Set<T> other)
        {
            Set<T> result = new Set<T>(_items);

            foreach (T item in other._items)
            {
                result.Remove(item);
            }
            return result;
        }

        public Set<T> SymmetricDifference(Set<T> other)
        {
            Set<T> union = Union(other);
            Set<T> intersection = Intersection(other);

            return union.Difference(intersection);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
