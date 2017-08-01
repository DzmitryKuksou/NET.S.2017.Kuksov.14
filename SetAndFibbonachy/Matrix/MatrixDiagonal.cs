using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrixs;

namespace MatrixDiagonal
{
    public class DiagonalMatrix<T> : AbstractMatrix<T>
    {
        #region fields
        private T[] array;
        #endregion

        #region c-tor
        public DiagonalMatrix()
        {
            array = new T[Size];
        }
        public DiagonalMatrix(int size)
        {
            CheckSize(size);
            this.size = size;
            array = new T[Size];
        }

        public DiagonalMatrix(int size, IEnumerable<T> inputArray) : this(size)
        {
            FillMatrix(inputArray);
        }
        #endregion
        public void NewElementMessage(object sender, ElementEventArgs e)
        {
            Console.WriteLine($"Element was changed in {e} at position ({e.Row}, {e.Column})");
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i == j)
                        result.Append(array[i].ToString() + ' ');
                    else
                        result.Append(default(T).ToString() + ' ');
                }
                if (i != Size - 1) result.Append('\n');
            }
            return result.ToString();
        }

        #region private & protected methods
        private void FillMatrix(IEnumerable<T> inputArray)
        {
            if (ReferenceEquals(inputArray, null)) throw new ArgumentNullException($"{nameof(inputArray)} is null.");

            int inputArrayIndex = 0;
            for (int i = 0; i < Size; i++)
            {
                if (inputArrayIndex < inputArray.Count()) array[i] = inputArray.ElementAt(inputArrayIndex);
                inputArrayIndex++;
            }
        }

        protected override T GetElement(int i, int j)
        {
            return (i != j) ? default(T) : array[i];
        }

        protected override void SetElement(T element, int i, int j)
        {
            if (i != j) throw new ArgumentException();
            if (element == null) throw new ArgumentNullException($"{nameof(element)} is null!");
            array[i] = element;
        }
        #endregion
    }
}