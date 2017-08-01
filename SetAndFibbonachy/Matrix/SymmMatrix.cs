using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrixs
{
    public class SymmetricMatrix<T> : AbstractMatrix<T>
    {
        #region fields
        private T[][] array;
        #endregion

        #region c-tors
        public SymmetricMatrix()
        {
            array = new T[Size][];
            for (int i = 0; i < Size; i++)
            {
                array[i] = new T[i + 1];
            }
        }
        public SymmetricMatrix(int size)
        {
            CheckSize(size);
            this.size = size;
            array = new T[Size][];
            for (int i = 0; i < Size; i++)
            {
                array[i] = new T[i + 1];
            }
        }
        public SymmetricMatrix(int size, IEnumerable<T> Array) : this(size)

        {
            if (array == null) throw new ArgumentNullException();
            FillMatrix(Array);
        }
        #endregion

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (j <= i)
                        str.Append(array[i][j].ToString() + ' ');
                    else
                        str.Append(array[j][i].ToString() + ' ');
                }
            }
            return str.ToString();
        }
        public void NewElementMessage(object sender, ElementEventArgs e)
        {
            Console.WriteLine($"element was changed at position ({e.Row}, {e.Column})");
        }
        private void FillMatrix(IEnumerable<T> Array)
        {
            if (Array == null) throw new ArgumentNullException($"{nameof(Array)} is null!");
            int Index = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (Index < Array.Count()) array[i][j] = Array.ElementAt(Index);
                    Index++;
                }
            }
        }
        protected override T GetElement(int i, int j)
        {
            return (j <= i) ? array[i][j] : array[j][i];
        }

        protected override void SetElement(T element, int i, int j) 
        {
            if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null!");
            if (j <= i)
                array[i][j] = element;
            else
                array[j][i] = element;
        }
    }
}