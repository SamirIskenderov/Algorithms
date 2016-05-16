namespace Algorithms.ConsoleApp
{
	using BigRat;
	using OurBigRat;
	using System;
	using System.Linq;
	using bigint = BigRat.BigInt;


	internal class Program
	{
		private static void Main()
		{
			bigint a = new bigint(uint.MaxValue);
			bigint b = a * 2;
			Console.WriteLine(a);
			Console.WriteLine(b);
		}
	}
}