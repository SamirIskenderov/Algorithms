using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Test
{
	[TestClass]
	public class DividerUnitTest
	{
		[TestMethod]
		public void TestAllDividers()
		{
			Divider divider = new Divider();

			for (long i = 0; i < 2000; i++)
			{
				bool my10 = divider.IsDivBy10(i);
				bool my101 = divider.IsDivBy101(i);
				bool my11 = divider.IsDivBy11(i);
				bool my13 = divider.IsDivBy13(i);
				bool my17 = divider.IsDivBy17(i);
				bool my19 = divider.IsDivBy19(i);
				bool my20 = divider.IsDivBy20(i);
				bool my23 = divider.IsDivBy23(i);
				bool my25 = divider.IsDivBy25(i);
				bool my29 = divider.IsDivBy29(i);
				bool my3 = divider.IsDivBy3(i);
				bool my30 = divider.IsDivBy30(i);
				bool my31 = divider.IsDivBy31(i);
				bool my37 = divider.IsDivBy37(i);
				bool my41 = divider.IsDivBy41(i);
				bool my50 = divider.IsDivBy50(i);
				bool my59 = divider.IsDivBy59(i);
				bool my6 = divider.IsDivBy6(i);
				bool my7 = divider.IsDivBy7(i);
				bool my79 = divider.IsDivBy79(i);
				bool my8 = divider.IsDivBy8(i);
				bool my9 = divider.IsDivBy9(i);

				bool original10 = i % 10 == 0;
				bool original101 = i % 101 == 0;
				bool original11 = i % 11 == 0;
				bool original13 = i % 13 == 0;
				bool original17 = i % 17 == 0;
				bool original19 = i % 19 == 0;
				bool original20 = i % 20 == 0;
				bool original23 = i % 23 == 0;
				bool original25 = i % 25 == 0;
				bool original29 = i % 29 == 0;
				bool original3 = i % 3 == 0;
				bool original30 = i % 30 == 0;
				bool original31 = i % 31 == 0;
				bool original37 = i % 37 == 0;
				bool original41 = i % 41 == 0;
				bool original50 = i % 50 == 0;
				bool original59 = i % 59 == 0;
				bool original6 = i % 6 == 0;
				bool original7 = i % 7 == 0;
				bool original79 = i % 79 == 0;
				bool original8 = i % 8 == 0;
				bool original9 = i % 9 == 0;

				Assert.AreEqual(original10, my10, $"Number {i} at test 10: failed test");
				Assert.AreEqual(original101, my101, $"Number {i} at test 101: failed test");
				Assert.AreEqual(original11, my11, $"Number {i} at test 11: failed test");
				Assert.AreEqual(original13, my13, $"Number {i} at test 13: failed test");
				Assert.AreEqual(original17, my17, $"Number {i} at test 17: failed test");
				Assert.AreEqual(original19, my19, $"Number {i} at test 19: failed test");
				Assert.AreEqual(original20, my20, $"Number {i} at test 20: failed test");
				Assert.AreEqual(original23, my23, $"Number {i} at test 23: failed test");
				Assert.AreEqual(original25, my25, $"Number {i} at test 25: failed test");
				Assert.AreEqual(original29, my29, $"Number {i} at test 29: failed test");
				Assert.AreEqual(original3, my3, $"Number {i} at test 3: failed test");
				Assert.AreEqual(original30, my30, $"Number {i} at test 30: failed test");
				Assert.AreEqual(original31, my31, $"Number {i} at test 31: failed test");
				Assert.AreEqual(original37, my37, $"Number {i} at test 37: failed test");
				Assert.AreEqual(original41, my41, $"Number {i} at test 41: failed test");
				Assert.AreEqual(original50, my50, $"Number {i} at test 50: failed test");
				Assert.AreEqual(original59, my59, $"Number {i} at test 59: failed test");
				Assert.AreEqual(original6, my6, $"Number {i} at test 6: failed test");
				Assert.AreEqual(original7, my7, $"Number {i} at test 7: failed test");
				Assert.AreEqual(original79, my79, $"Number {i} at test 79: failed test");
				Assert.AreEqual(original8, my8, $"Number {i} at test 8: failed test");
				Assert.AreEqual(original9, my9, $"Number {i} at test 9: failed test");
			}
		}
	}
}