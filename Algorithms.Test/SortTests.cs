using Algorithms.Extensions;
using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Test
{
    [TestClass]
    public class SortTests
    {
        #region Public Methods

        [TestMethod]
        public void IsArraySortedByBogosort()
        {
            const int count = 10;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.Bogosort<int>(ref array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByBubbleSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.BubbleSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByInsertSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.InsertSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByMergeSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);

            array = Sort.MergeSort<int>(array).ToList();

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByPancakeSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.PancakeSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByQuickBubbleSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.QuickBubbleSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedByQuickSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.QuickSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        [TestMethod]
        public void IsArraySortedBySelectionSort()
        {
            const int count = 1000;
            IList<int> array = new List<int>(count);
            array.SetWithRandomElements(min: -10,
                    max: 10,
                    capacity: count,
                    FuncToGetNewRandomElement: Common.Random.Next);
            Sort.SelectionSort<int>(array);

            Assert.AreEqual(true, IsArraySorted<int>(array));
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsArraySorted<T>(IList<T> arr)
            where T : IComparable
        {
            return IsArraySortedByAcending(arr) || IsArraySortedByDecending(arr);
        }

        private static bool IsArraySortedByAcending<T>(IList<T> arr)
                                                    where T : IComparable
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                if (arr[i].CompareTo(arr[i + 1]) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsArraySortedByDecending<T>(IList<T> arr)
            where T : IComparable
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                if (arr[i].CompareTo(arr[i + 1]) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion Private Methods
    }
}