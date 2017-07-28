using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibbonachy_s
{
    public static class Fibbonachy
    {
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
