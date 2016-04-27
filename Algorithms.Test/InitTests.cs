using Algorithms.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Algorithms.Test
{
    public class InitTests
    {
        [TestMethod]
        public void ArrayHaveToBeFillFull()
        {
            const int count = 10;
            List<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                            max: 10,
                            capacity: count,
                            FuncToGetNewRandomElement: Extensions.IListExtensions.CommonRandom.Next);

            if (array.Count != count)
            {
                Assert.Fail();
            }
        }
    }
}