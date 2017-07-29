using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    public class Set<T> : IEnumerable<T>, IEquatable<Set<T>>, ICloneable where T : class
    {
        /// <summary>
        /// fields
        /// </summary>
        private T[] stored;
        private int count;
        /// <summary>
        /// property
        /// </summary>
        public int Count => count;
        /// <summary>
        /// c-or
        /// </summary>
        public Set()
        {
            stored = new T[20];
        }
        /// <summary>
        /// c-or
        /// </summary>
        /// <param name="size">size</param>
        public Set(int size)
        {
            if (size < 1) throw new ArgumentException();
            stored = new T[size];
            count = 0;
        }
        /// <summary>
        /// c-or
        /// </summary>
        /// <param name="set">set</param>
        public Set(IEnumerable<T> set)
        {
            if (set == null) throw new ArgumentException();
            foreach (var element in set)
                Add(element);
        }
        /// <summary>
        /// adding el
        /// </summary>
        /// <param name="element">element</param>
        public void Add(T element)
        {
            if (count == stored.Length) Resize();
            stored[count++] = element;
        }
        /// <summary>
        /// removing el
        /// </summary>
        /// <param name="element">element</param>
        public void Remove(T element)
        {
            for (int i = 0; i < Count; i++)
            {
                if (stored[i] == element)
                {
                    for (int j = i; j < Count - 1; i++)
                    {
                        stored[j] = stored[j + 1];
                    }
                    stored[count - 1] = default(T);
                    break;
                }
            }
        }
        /// <summary>
        /// checking on equality
        /// </summary>
        /// <param name="set">set</param>
        /// <returns>true or false</returns>
        public bool Equals(Set<T> set)
        {
            if (set == null) throw new ArgumentNullException();
            if (Count != count) return false;
            int i = 0;
            foreach (var el in set)
            {
                if (el != stored[i]) return false;
                i++;
            }
            return true;
        }
        /// <summary>
        ///  returns Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return stored[i];
        }
        /// <summary>
        /// Intersection of sets
        /// </summary>
        /// <param name="lhs">first set</param>
        /// <param name="rhs">second set</param>
        /// <returns>set</returns>
        public static Set<T> Intersection(Set<T> lhs, Set<T> rhs)
        {
            Set<T> newSet = new Set<T>();
            foreach (var element in lhs)
                if (rhs.Contains(element)) newSet.Add(element);
            return newSet;

        }
        /// <summary>
        /// Association of sets
        /// </summary>
        /// <param name="lhs">first set</param>
        /// <param name="rhs">second set</param>
        /// <returns>set</returns>
        public static Set<T> Association(Set<T> lhs, Set<T> rhs)
        {
            Set<T> newSet = new Set<T>(lhs);
            foreach (var element in rhs)
                if (!newSet.Contains(element)) newSet.Add(element);
            return newSet;
        }
        /// <summary>
        /// checking on contains el
        /// </summary>
        /// <param name="el">element</param>
        /// <returns>true or false</returns>
        public bool Contains(T el)
        {
            for (int i = 0; i < Count; i++)
                if (el == stored[i]) return true;
            return false;
        }
        /// <summary>
        /// converting to string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Count; i++) 
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
        /// <summary>
        /// resizing
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
        /// <summary>
        /// cloning
        /// </summary>
        /// <returns>set</returns>
        object ICloneable.Clone()
        {
            return new Set<T>(this);
        }
        /// <summary>
        /// returning enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}