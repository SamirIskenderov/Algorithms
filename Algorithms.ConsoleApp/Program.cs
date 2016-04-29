using Algorithms.BigNumber;

namespace Algorithms.ConsoleApp
{
	internal class Program
	{
		private static void Main()
		{
			BigNumberDS b1 = BigNumberDS.Create("123456789123");
			BigNumberDS b2 = BigNumberDS.Create("987654321987");

			System.Console.WriteLine(b1 & b2);
		}
	}
}