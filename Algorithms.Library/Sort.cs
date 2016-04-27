namespace Algorithms.Library
{
	using Extensions;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class Sort
	{
		public static void Bogosort<T>(ref IList<T> arr)
		    where T : IComparable
		{
			if (arr == null)
			{
				throw new ArgumentNullException("Array is null");
			}

			while (!IsArraySorted(arr))
			{
				arr = arr.Shuffle(Common.rand).ToList();
			}
		}

		public static void BubbleSort<T>(IList<T> array)
			    where T : IComparable
		{
			if (array == null)
			{
				throw new ArgumentNullException("Array is null");
			}

			for (int i = 0; i < array.Count; ++i)
			{
				for (int j = 0; j < array.Count; j++)
				{
					if (array[i].CompareTo(array[j]) < 0)
					{
						T tmp = array[i];
						array[i] = array[j];
						array[j] = tmp;
					}
				}
			}
		}

		public static void InsertSort<T>(IList<T> arr)
		    where T : IComparable
		{
			if (arr == null)
			{
				throw new ArgumentNullException("Arr is null");
			}

			InsertSort(arr, 0, arr.Count);
		}

		public static IList<T> MergeSort<T>(IList<T> array)
		    where T : IComparable
		{
			#region nullchecks

			if (array == null)
			{
				throw new ArgumentNullException("Array is null");
			}

			#endregion nullchecks

			if (array.Count == 1)
			{
				return array;
			}

			int cap = array.Count / 2;

			IList<T> lhs_array = new List<T>(cap);

			for (int i = 0; i < array.Count / 2; i++)
			{
				lhs_array.Add(array[i]);
			}

			cap = array.Count % 2 == 0 ? array.Count / 2 : array.Count / 2 + 1;

			IList<T> rhs_array = new List<T>(cap);

			for (int i = 0; i < cap; i++)
			{
				rhs_array.Add(array[(array.Count / 2) + i]);
			}

			lhs_array = MergeSort(lhs_array).ToList();
			rhs_array = MergeSort(rhs_array).ToList();

			return Merge(lhs_array, rhs_array);
		}

		public static void PancakeSort<T>(IList<T> arr, int cutoffValue = 2)
		    where T : IComparable
		{
			if (arr == null)
			{
				throw new ArgumentNullException("Array is null");
			}
			if (arr.Count < cutoffValue)
			{
				return;
			}

			for (int i = arr.Count - 1; i >= 0; --i)
			{
				int pos = i;
				// Find position of max number between beginning and i
				for (int j = 0; j < i - 1; j++)
				{
					if (arr[j].CompareTo(arr[pos]) > 0)
					{
						pos = j;
					}
				}

				// is it in the correct position already?
				if (pos == i)
				{
					continue;
				}

				// is it at the beginning of the array? If not flip array section so it is
				if (pos != 0)
				{
					Flip(arr, pos + 1);
				}

				// Flip array section to get max number to correct position
				Flip(arr, i + 1);
			}
		}

		public static void QuickBubbleSort<T>(IList<T> array)
					    where T : IComparable
		{
			if (array == null)
			{
				throw new ArgumentNullException("Array is null");
			}

			for (int i = 0; i < array.Count - 1; ++i)
			{
				for (int j = i; j >= 0; --j)
				{
					if (array[j].CompareTo(array[j + 1]) < 0)
					{
						T tmp = array[j];
						array[j] = array[j + 1];
						array[j + 1] = tmp;
					}
				}
			}
		}

		public static void QuickSort<T>(IList<T> arr, uint cutoffValue = 9)
		    where T : IComparable
		{
			if (arr == null)
			{
				throw new ArgumentNullException("Arr is null");
			}

			if (cutoffValue <= 0)
			{
				throw new ArgumentException("Cutoff value must be greater that zero");
			}

			QuickSort(arr, 0, arr.Count - 1, cutoffValue);
			InsertSort(arr);
		}

		private static void Flip<T>(IList<T> arr, int n)
		    where T : IComparable
		{
			for (int i = 0; i < n; i++)
			{
				--n;
				T tmp = arr[i];
				arr[i] = arr[n];
				arr[n] = tmp;
			}
		}

		private static void InsertSort<T>(IList<T> arr, int left, int right)
			    where T : IComparable
		{
			int i = 0;
			int j = 0;
			T tmp;
			for (i = left + 1; i < right; i++)
			{
				for (j = i; j > 0; j--)
				{
					if (arr[j - 1].CompareTo(arr[j]) < 0)
					{
						break;
					}
					tmp = arr[j - 1];
					arr[j - 1] = arr[j];
					arr[j] = tmp;
				}
			}
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

		private static IList<T> Merge<T>(IList<T> lhs_array, IList<T> rhs_array)
					    where T : IComparable
		{
			int rhs = 0;
			int lhs = 0;
			List<T> merged = new List<T>(lhs_array.Count + rhs_array.Count);

			for (int i = 0; i < merged.Capacity; i++)
			{
				if (rhs > rhs_array.Count - 1)
				{
					merged.Add(lhs_array[lhs]);
					lhs++;
					continue;
				}

				if (lhs > lhs_array.Count - 1)
				{
					merged.Add(rhs_array[rhs]);
					rhs++;
					continue;
				}

				if (lhs_array[lhs].CompareTo(rhs_array[rhs]) > 0)
				{
					merged.Add(rhs_array[rhs]);
					rhs++;
				}
				else
				{
					merged.Add(lhs_array[lhs]);
					lhs++;
				}
			}

			return merged;
		}

		public static void SelectionSort<T>(IList<T> arr)
		    where T : IComparable
		{
			if (arr == null)
			{
				throw new ArgumentNullException("Array is null");
			}
			for (int i = 0; i < arr.Count - 1; i++)
			{
				int min = i;

				for (int j = i + 1; j < arr.Count; j++)
				{
					if (arr[j].CompareTo(arr[min]) < 0)
					{
						min = j;
					}
				}

				T tmp = arr[i];
				arr[i] = arr[min];
				arr[min] = tmp;
			}
		}

		private static void QuickSort<T>(IList<T> arr, int left, int right, uint cutoffValue)
		    where T : IComparable
		{
			/*
				     * quicksort with mid divider
				     */

			T mid;
			T foo;
			int l = left;
			int r = right;

			//finding good divider
			mid = arr[arr.Count / 2];

			// sorting
			while (l <= r)
			{
				while ((arr[l].CompareTo(mid) < 0) && (l < right))
				{
					l++;
				}

				while ((mid.CompareTo(arr[r]) > 0) && (r > left))
				{
					r--;
				}

				if (l <= r)
				{
					foo = arr[l];
					arr[l] = arr[r];
					arr[r] = foo;
					l++;
					r--;
				}
			}

			if (Math.Abs(r - left) > cutoffValue)
			{
				QuickSort(arr, left, r, cutoffValue);
			}
			if (Math.Abs(l - right) > cutoffValue)
			{
				QuickSort(arr, l, right, cutoffValue);
			}
		}
	}
}