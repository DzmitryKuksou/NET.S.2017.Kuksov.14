using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrixs
{
    public abstract class AbstractMatrix<T>:IEquatable<AbstractMatrix<T>>
    {
        protected int size;
        public int Size => size;
        public event EventHandler<ElementEventArgs> NewElement = delegate { };
        public void ChangeElement(T element, int i, int j)
        {
            SetElement(element, i - 1, j - 1);
            NewElement.Invoke(this, new ElementEventArgs(i, j));
        }
        protected abstract T GetElement(int i, int j);
        protected abstract void SetElement(T element, int i, int j);

        protected void CheckSize(int size)
        {
            if (size <= 0) throw new ArgumentException();
        }
        public T this[int i, int j]
        {
            get
            {
                return GetElement(--i, --j);
            }
        }
        public bool Equals(AbstractMatrix<T> other)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (Size != other.Size) return false;
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    if (!this[i, j].Equals(other[i, j])) return false;
                }
            }
            return true;
        }
    }
}
