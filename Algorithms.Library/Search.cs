using System;
using System.Collections.Generic;

namespace Algorithms.Library
{
    public static class Search
    {
        #region Public Methods

        public static int Binary<T>(IList<T> list, T sreachingItem, bool isPresorted = false)
            where T : IComparable
        {
            if ((!isPresorted) && (!IsArraySorted(list)))
            {
                list = Sort.MergeSort(list);
            }

            return BinarySearch(list, sreachingItem, sorted: true);
        }

        #endregion Public Methods

        #region Private Methods

        private static int BinarySearch<T>(IList<T> list, T sreachingItem, bool sorted)
            where T : IComparable
        {
            int lhs = 0;
            int rhs = list.Count;

            while (lhs < rhs)
            {
                int mid;
                checked
                {
                    mid = (lhs + rhs) / 2;
                }

                if (list[mid].CompareTo(sreachingItem) == 0)
                {
                    return mid;
                }

                if (list[mid].CompareTo(sreachingItem) < 0)
                {
                    rhs = mid;
                }
                else
                {
                    lhs = mid + 1;
                }
            }

            return -1;
        }

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