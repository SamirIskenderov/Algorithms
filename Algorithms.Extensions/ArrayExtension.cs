﻿namespace Algorithms.Extensions
{
	public static class ArrayExtension
	{
		public static int[] Parse(this int[] arr, int number)
		{
			int[] output = new int[10];

			int decade = 10;
			int i = 0;
			while (number > 1)
			{
				output[i] = number % decade;
				i++;
				number /= decade;
				//decade *= 10;
			}

			return output;
		}
	}
}