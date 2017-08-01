using System;
using System.Collections.Generic;
// нет сборки System.Numerics

namespace Fibbonachy_s
{
    public static class Fibbonachy
    {
        /// <summary>
        /// sequence
        /// </summary>
        /// <param name="y">order</param>
        /// <returns>array</returns>
        public static IEnumerable<int> FibbonachyNumbers(int y)
        {
            int first = 0;
            int second = 1;
            while (second < y || first < y)  
            {
                yield return first;
                second += first;
                yield return second;
                first += second;
            }

        }
    }
}
