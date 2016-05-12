using Algorithms.BigNumber;
using OurBigRat;
using System;

namespace Algorithms.ConsoleApp
{
	using digit = OurBigDigit;

	internal class Program
	{
		private static void Main()
		{
			bool[] l = new bool[digit.RADIX];
			bool[] r = new bool[digit.RADIX];

			l[3] = true;
			r[2] = true;
			r[4] = true;

			digit o = new digit();
			digit lhs = new digit(l);
			digit rhs = new digit(r);

			Console.WriteLine(OurBigDigitMathHelper.BitsToNumber(l));
			Console.WriteLine(OurBigDigitMathHelper.BitsToNumber(r));

			digit result = OurBigDigitMath.DigitMultiple(lhs, rhs, out o);

			Console.WriteLine(OurBigDigitMathHelper.BitsToNumber(result.Value));

			int a = 2;
		}
	}
}