using System;

namespace Algorithms.Library
{
	public class Calc
	{
		// awfull. wtf is wrong with big number's libs?
		public string Pi()
		{
			int steps = 100;

			//BigDecimal pi = new BigDecimal();

			//for (int i = 0; i < steps; i++)
			//{
			//	pi += Math.Pow(16.0, -i) * (4.0 / (8.0 * i + 1.0) - 2.0 / (8.0 * i + 4.0) - 1.0 / (8.0 * i + 5.0) - 1.0 / (8.0 * i + 6.0));
			//}

			return "a";

			//return pi.ToString();
		}

		public string FibonacciNumber(int number)
		{
			double sqrt5 = Math.Sqrt(5);
			double phi = (sqrt5 + 1) / 2;
			return Math.Round(Math.Pow(phi, number) / sqrt5).ToString();
		}
	}
}