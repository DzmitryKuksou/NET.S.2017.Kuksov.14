using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrixs
{
    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        private T[,] array;

        #region ctors
        public SquareMatrix()
        {
            size = 1;
            array = new T[Size, Size];
        }
        public SquareMatrix(int size)
        {
            CheckSize(size);
            this.size = size;
            array = new T[Size, Size];
        }
        public SquareMatrix(int size, IEnumerable<T> Array) : this(size)
        {
            FillMatrix(Array);
        }
        #endregion
        public void ElementMessage(object sender, ElementEventArgs e)
        {
            Console.WriteLine($"Element was changed in ({e.Row}, {e.Column})");
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    str.Append(array[i, j].ToString() + ' ');
                }
                if (i != array.GetLength(0) - 1) str.Append('\n');
            }
            return str.ToString();
        }
        private void FillMatrix(IEnumerable<T> Array)
        {
            if (Array == null) throw new ArgumentNullException($"{nameof(Array)} is null!");

            int index = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (index < Array.Count()) array[i, j] = Array.ElementAt(index);
                    index++;
                }
            }
        }
       
        protected override void SetElement(T element, int i, int j)
        {
            if (element == null) throw new ArgumentNullException($"{nameof(element)} is null.");
            array[i, j] = element;
        }

    }
}