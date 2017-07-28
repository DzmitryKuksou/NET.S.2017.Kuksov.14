using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fibbonachy_s;

namespace NUnit.FibbonachyTests1
{
    [TestFixture]
    public class TestFibbonachy
    {
        [TestCase(5, ExpectedResult = new int[] { 0, 1, 1, 2, 3, 5 })]
        [TestCase(20, ExpectedResult = new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 })]
        public IEnumerable<int> Test_FibbonachyNumbers(int y)
        {
            return Fibbonachy.FibbonachyNumbers(y);
        }
    }
}
