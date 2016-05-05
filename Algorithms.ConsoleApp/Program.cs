using Algorithms.BigNumber;
using OurBigRat;
using System;

namespace Algorithms.ConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			uint a = 15621;
			uint b = 1488; 

			OurBigInt oa = new OurBigInt(a);
			OurBigInt ob = new OurBigInt(b);

			uint r = a >> 1;
			Console.WriteLine(r);
			OurBigInt or = oa >> 1;
			Console.WriteLine(or);
		}
	}
}