using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Algorithms.Extensions;

namespace Algorithms.Test
{
    [TestClass]
    public class SearchUnitTest
    {
        [TestMethod]
        public void OverflowOfIndexsersTest()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Extensions.IListExtensions.CommonRandom.Next);
        }
    }
}
