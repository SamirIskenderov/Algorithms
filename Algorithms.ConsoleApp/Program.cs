using Algorithms.BigNumber;
using System;

namespace Algorithms.ConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			BigNumberDS lhs = new BigNumberDS("20000,505");
			BigNumberDS added = new BigNumberDS("10,501");

			Console.WriteLine(added * lhs);
		}
	}
}