using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sets
{
    public class Set<T> : ISet<T>, ICloneable where T : class
    {
        #region fields 
        /// <summary>
        /// fields
        /// </summary>     
        private T[] stored;
        private int count;
        public bool IsReadOnly => false;
        #endregion

        #region properties
        /// <summary>
        /// property
        /// </summary>
        public int Count => count;
        #endregion
        #region c-tors
        /// <summary>
        /// c-tor
        /// </summary>
        public Set()
        {
            count = 0;
            stored = new T[20];
        }
        /// <summary>
        /// C-tor with parameter
        /// </summary>
        /// <param name="size">size</param>
        public Set(int size)
        {
            if (size < 1) throw new ArgumentException($"{nameof(size)} is less than one!");
            stored = new T[size];
            count = 0;
        }

        /// <summary>
        /// C-or
        /// </summary>
        /// <param name="collection">collection of set</param>
        public Set(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException($"{nameof(collection)} is null!");
            foreach (var item in collection)
            {
                Add(item);
            }
        }
        #endregion

        /// <summary>
        /// Enumerator
        /// </summary>
        /// <returns>IEnumerator<T></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return stored[i];
            }
        }

        /// <summary>
        /// Adds an element
        /// </summary>
        /// <param name="item">Element</param>
        /// <returns>True or false</returns>
        public bool Add(T element)
        {
            if (element == null) throw new ArgumentNullException($"{nameof(element)} is null!");
            if (Contains(element)) return false;
            if (Count == stored.Length) Resize();
            stored[count++] = element;
            return true;
        }
        /// <summary>
        /// Removing element
        /// </summary>
        /// <param name="item">Element</param>
        /// <returns>true or false</returns>
        public bool Remove(T element)
        {
            if (!Contains(element)) return false;
            int index = 0;
            for (int i = 0; i < Count; i++)
            {
                if (stored[i] == element) index = i;
            }
            stored[index] = stored[Count - 1];
            stored[Count - 1] = default(T);
            count--;
            return true;
        }

        /// <summary>
        /// Checking 
        /// </summary>
        /// <param name="item">Element</param>
        /// <returns>True or false</returns>
        public bool Contains(T element)
        {
            for (int i = 0; i < count; i++)
            {
                if (stored[i] == element) return true;
            }
            return false;
        }

        /// <summary>
        /// Clearing.
        /// </summary>
        public void Clear()
        {
            count = 0;
            stored = new T[20];
        }
        /// <summary>
        /// intersection
        /// </summary>
        /// <param name="other">set</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");
            foreach (var item in other)
            {
                if (!Contains(item))
                    Remove(item);
            }
        }
        /// <summary>
        /// expecting
        /// </summary>
        /// <param name="other">set</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");

            foreach (var el in other)
            {
                Remove(el);
            }
        }
        /// <summary>
        /// Union with other set
        /// </summary>
        /// <param name="other">set</param>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");
            foreach (var el in other)
            {
                Add(el);
            }
        }
        /// <summary>
        /// Symmetric Set
        /// </summary>
        /// <param name="other">Other collection.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");
            foreach (var el in other)
            {
                if (Contains(el))Remove(el);
                else Add(el);
            }
        }

        /// <summary>
        ///  checking, super set
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
            if (Count < other.Count()) return false;
            foreach (var item in other)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }
        /// <summary>
        /// checking subset
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
            if (Count > other.Count()) return false;

            for (int i = 0; i < Count; i++)
            {
                if (!other.Contains(stored[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// Over laps
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");
            foreach (var el in stored)
            {
                if (other.Contains(el)) return true;
            }
            return false;
        }
        /// <summary>
        /// checking proper subset
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null!");
            return IsSubsetOf(other) && count > other.Count();
        }
        /// <summary>
        /// set equals
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
            if (other == this) return true;
            if (Count != other.Count()) return false;
            return IsSupersetOf(other) && IsSubsetOf(other);
        }
        /// <summary>
        /// proper set
        /// </summary>
        /// <param name="other">set</param>
        /// <returns>true or false</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException($"{nameof(other)} is null");
            return IsSupersetOf(other) && count < other.Count();
        }
        /// <summary>
        /// copy to array
        /// </summary>
        /// <param name="array">Array.</param>
        /// <param name="arrayIndex">index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException($"{nameof(array)} is null!");
            if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentException($"{nameof(arrayIndex)} is invalid!");
            for (int i = arrayIndex; i < array.Length; i++)
            {
                if (i - arrayIndex > Count) return;
                array[i] = stored[i - arrayIndex];
            }
        }
        /// <summary>
        /// Returns in format string.
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                str.Append(stored[i].ToString());
            }
            return str.ToString();
        }
        /// <summary>
        /// cloning
        /// </summary>
        /// <returns>new set</returns>
        public Set<T> Clone()
        {
            return new Set<T>(this);
        }
        object ICloneable.Clone()
        {
            return new Set<T>(this);
        }
        void ICollection<T>.Add(T item) => Add(item);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <summary>
        /// resizeng
        /// </summary>
        private void Resize()
        {
            T[] empty = new T[stored.Length + 20];
            for (int i = 0; i < stored.Length; i++)
            {
                empty[i] = stored[i];
            }
            stored = empty;
        }
    }
}