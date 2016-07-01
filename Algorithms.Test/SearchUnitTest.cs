using Algorithms.Extensions;
using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                    funcToGetNewRandomElement: Common.Random.Next);
        }
    }
}