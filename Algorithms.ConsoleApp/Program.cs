﻿using Algorithms.BigNumber;
using System;

namespace Algorithms.ConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			BigNumberDS num1 = new BigNumberDS("1234567890,0123456789");
			BigNumberDS num2 = new BigNumberDS("9876,54321");
			BigNumberDS num3 = new BigNumberDS("0,5");

			Console.WriteLine(num1 * num2 * num3);
		}
	}
}