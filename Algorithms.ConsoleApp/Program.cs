using Algorithms.BigNumber;
using System;

namespace Algorithms.ConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9");

			Console.WriteLine(lhs * added);
		}
	}
}