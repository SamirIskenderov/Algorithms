using System;
using System.Collections.Generic;

namespace Algorithms.Extensions
{
	public static class IListExtensions
	{
		public static void SetWithRandomElements<T>(this IList<T> array, T min, T max, int capacity, Func<T, T, T> FuncToGetNewRandomElement)
		    where T : IConvertible
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array), "Array is null");
			}

			if (FuncToGetNewRandomElement == null)
			{
				throw new ArgumentNullException(nameof(FuncToGetNewRandomElement), "Function to get new ramndom element is null.");
			}

			for (int i = 0; i < capacity; ++i)
			{
				array.Add(FuncToGetNewRandomElement(min, max));
			}
		}

		public static void Show<T>(this IList<T> array)
		    where T : IConvertible
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array), "Array is null");
			}

			foreach (var item in array)
			{
				Console.WriteLine(item + "  ");
			}
		}
	}
}