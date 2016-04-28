using Algorithms.BigNumber;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace Algorithms.Test
{
	[TestClass]
	public class BigNumberDSUnitTest
	{
		#region ctor

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CtorFromEmptyStringMustThrowArgNullExc()
		{
			BigNumberDS num = new BigNumberDS(string.Empty);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CtorFromNullMustThrowArgNullExc()
		{
			BigNumberDS num = new BigNumberDS(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CtorFromSpaceStringMustThrowArgNullExc()
		{
			BigNumberDS num = new BigNumberDS(new string(' ', 15));
		}

		[TestMethod]
		public void CtorMustThrowExcVer1()
		{
			BigNumberDS num0 = new BigNumberDS();
		}

		[TestMethod]
		public void CtorMustThrowExcVer2()
		{
			BigNumberDS num1 = new BigNumberDS("12");
		}

		[TestMethod]
		public void CtorMustThrowExcVer3()
		{
			BigNumberDS num2 = new BigNumberDS("0,12");
		}

		[TestMethod]
		public void CtorMustThrowExcVer4()
		{
			BigNumberDS num3 = new BigNumberDS("12,12");
		}

		[TestMethod]
		public void CtorMustThrowExcVer5()
		{
			BigNumberDS num4 = new BigNumberDS(",12");
		}

		#endregion ctor

		[TestMethod]
		public void BigNumberWithNullsAmidMustNotThrowItOut()
		{
			BigNumberDS obj = new BigNumberDS("10000000000000000000000000000000000000000000000000000000000000001,100000000000000000000000000000000000000000001");

			Assert.AreEqual("10000000000000000000000000000000000000000000000000000000000000001,100000000000000000000000000000000000000000001", obj.ToString());
		}

		#region add

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("12");
			BigNumberDS rhs = new BigNumberDS("12");
			BigNumberDS result = new BigNumberDS("24");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("12,12");
			BigNumberDS rhs = new BigNumberDS("12,12");
			BigNumberDS result = new BigNumberDS("24,24");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,12");
			BigNumberDS rhs = new BigNumberDS("0,12");
			BigNumberDS result = new BigNumberDS("0,24");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer4()
		{
			BigNumberDS lhs = /*    */ new BigNumberDS("12121212412,12121212412");
			BigNumberDS rhs = /*    */ new BigNumberDS("12121212412,12121212412");
			BigNumberDS result = /* */ new BigNumberDS("24242424824,24242424824");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer5()
		{
			BigNumberDS lhs = /*    */ new BigNumberDS("99595461231975613216879412315,00203033540056462120697460");
			BigNumberDS rhs = /*            */ new BigNumberDS("495120297894120548450,98120954509841650747165095020905");
			BigNumberDS result = /* */ new BigNumberDS("99595461727095911110999960765,98323988049898112867862555020905");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer6()
		{
			BigNumberDS lhs = /*                                       */ new BigNumberDS("5401009724016,15400001240000000000000000000000000000000000000001");
			BigNumberDS rhs = /*    */ new BigNumberDS("540150000000000000000000000000000005401009766196,12121212412");
			BigNumberDS result = /* */ new BigNumberDS("540150000000000000000000000000000010802019490212,27521213652000000000000000000000000000000000000001");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer7()
		{
			BigNumberDS lhs = /*    */ new BigNumberDS(",12");
			BigNumberDS rhs = /*    */ new BigNumberDS(",12");
			BigNumberDS result = /* */ new BigNumberDS(",24");

			Assert.IsTrue(result == lhs + rhs);
		}

		[TestMethod]
		public void AddTwoNumbersMustAddItCorrectlyVer8()
		{
			BigNumberDS lhs = /*    */ new BigNumberDS("-0,123456789");
			BigNumberDS rhs = /*    */ new BigNumberDS("4");
			BigNumberDS result = /* */ new BigNumberDS("3,876543211");

			Assert.IsTrue(result == lhs + rhs);
		}

		#endregion add

		#region operators

		#region unary+

		[TestMethod]
		public void OperatorUnapyPlusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("123");

			Assert.AreEqual(+lhs, rhs);
		}

		[TestMethod]
		public void OperatorUnapyPlusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123456789123");

			Assert.AreEqual(+lhs, rhs);
		}

		[TestMethod]
		public void OperatorUnapyPlusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("0,123456789");

			Assert.AreEqual(+lhs, rhs);
		}

		[TestMethod]
		public void OperatorUnapyPlusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123456789123,123456789");

			Assert.AreEqual(+lhs, rhs);
		}

		#endregion unary+

		#region unary-

		[TestMethod]
		public void OperatorUnapyMinusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("-123");

			Assert.AreEqual(rhs, -lhs);
		}

		[TestMethod]
		public void OperatorUnapyMinusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("-123456789123456789123");

			Assert.AreEqual(rhs, -lhs);
		}

		[TestMethod]
		public void OperatorUnapyMinusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("0,123456789");

			Assert.AreEqual(rhs, -lhs);
		}

		[TestMethod]
		public void OperatorUnapyMinusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("-123456789123456789123,123456789");

			Assert.AreEqual(rhs, -lhs);
		}

		#endregion unary-

		#region pre/postfix+

		[TestMethod]
		public void OperatorPostfixPlusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("119");
			BigNumberDS rhs = new BigNumberDS("120");
			lhs++;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPostfixPlusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("124");
			lhs++;
			Assert.AreEqual(lhs, rhs);
		}

		[TestMethod]
		public void OperatorPostfixPlusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123456789124");
			lhs++;
			Assert.AreEqual(lhs, rhs);
		}

		[TestMethod]
		public void OperatorPostfixPlusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("1,123456789");
			lhs++;
			Assert.AreEqual(lhs, rhs);
		}

		[TestMethod]
		public void OperatorPostfixPlusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123456789124,123456789");
			lhs++;
			Assert.AreEqual(lhs, rhs);
		}

		[TestMethod]
		public void OperatorPrefixPlusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("119");
			BigNumberDS rhs = new BigNumberDS("120");
			++lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixPlusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("124");
			++lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixPlusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123456789124");
			++lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixPlusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("1,123456789");
			++lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixPlusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123456789124,123456789");
			++lhs;
			Assert.AreEqual(rhs, lhs);
		}

		#endregion pre/postfix+

		#region pre/postfix-

		[TestMethod]
		public void OperatorPostfixMinusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("110");
			BigNumberDS rhs = new BigNumberDS("109");
			lhs--;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPostfixMinusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("122");
			lhs--;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPostfixMinusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123456789122");
			lhs--;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPostfixMinusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("-1,123456789");
			lhs--;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPostfixMinusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123456789122,123456789");
			lhs--;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixMinusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("110");
			BigNumberDS rhs = new BigNumberDS("109");
			--lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixMinusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("122");
			--lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixMinusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123456789122");
			--lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixMinusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("-1,123456789");
			--lhs;
			Assert.AreEqual(rhs, lhs);
		}

		[TestMethod]
		public void OperatorPrefixMinusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123456789122,123456789");
			--lhs;
			Assert.AreEqual(rhs, lhs);
		}

		#endregion pre/postfix-

		#region binary+

		[TestMethod]
		public void OperatorMultiplicativePlusMustBeAssociative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("91206951361651,321981314981");
			BigNumberDS rhs = new BigNumberDS("1123,123");

			Assert.AreEqual((lhs + mhs) + rhs, rhs + (mhs + lhs));
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustBeCommutative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual(lhs + rhs, rhs + lhs);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustBeDistributive()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("100");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual(mhs * (lhs + rhs), mhs * lhs + mhs * rhs);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("132");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("4");
			BigNumberDS rhs = new BigNumberDS("127");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("4");
			BigNumberDS rhs = new BigNumberDS("123456789123456789127");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("123456789123456789132");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("4");
			BigNumberDS rhs = new BigNumberDS("4,123456789");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("11");
			BigNumberDS rhs = new BigNumberDS("11,123456789");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("4");
			BigNumberDS rhs = new BigNumberDS("123456789123456789127,123456789");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("123456789123456789132,123456789");

			Assert.AreEqual(rhs, lhs + added);
		}

		[TestMethod]
		public void OperatorMultiplicativePlusMustWorkWithNullCorrectly()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreEqual(lhs, lhs + 0);
		}

		#endregion binary+

		#region binary-

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeAbsAssociative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("91206951361651,321981314981");
			BigNumberDS rhs = new BigNumberDS("1123,123");

			Assert.AreEqual(((lhs - mhs) - rhs).Abs(), (rhs - (mhs - lhs)).Abs());
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeAbsCommutative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual((lhs - rhs).Abs(), (rhs - lhs).Abs());
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeDistributive()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("100");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual(mhs * (lhs - rhs), mhs * lhs - mhs * rhs);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeInverseElementCorrect()
		{
			BigNumberDS obj = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreNotEqual(obj + obj, obj - (-obj));
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeNonAssociative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("91206951361651,321981314981");
			BigNumberDS rhs = new BigNumberDS("1123,123");

			Assert.AreNotEqual((lhs - mhs) - rhs, rhs - (mhs - lhs));
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustBeNonCommutative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreNotEqual(lhs - rhs, rhs - lhs);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("114");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("2");
			BigNumberDS rhs = new BigNumberDS("121");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("123456789123456789114");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("2");
			BigNumberDS rhs = new BigNumberDS("123456789123456789121");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("4");
			BigNumberDS rhs = new BigNumberDS("-3,876543211");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("11");
			BigNumberDS rhs = new BigNumberDS("-10,876543211");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("2");
			BigNumberDS rhs = new BigNumberDS("123456789123456789121,123456789");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("123456789123456789114,123456789");

			Assert.AreEqual(rhs, lhs - added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMinusMustWorkWithNullCorrectly()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreEqual(lhs, lhs - 0);
		}

		#endregion binary-

		#region binary*

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustBeAssociative()
		{
			BigNumberDS lhs = new BigNumberDS("123456789189123,123453456789123");
			BigNumberDS mhs = new BigNumberDS("9120361651,321984981");
			BigNumberDS rhs = new BigNumberDS("1123,123");

			Assert.AreEqual((lhs * mhs) * rhs, rhs * (mhs * lhs));
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustBeCommutative()
		{
			BigNumberDS lhs = /* */ new BigNumberDS("123456789189123,123453456789123");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual(lhs * rhs, rhs * lhs);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustBeDistributive()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");
			BigNumberDS mhs = new BigNumberDS("100");
			BigNumberDS rhs = /*            */ new BigNumberDS("1123,123");

			Assert.AreEqual(mhs * (lhs + rhs), mhs * lhs + mhs * rhs);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("1107");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-1107");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("1111111102111111102107");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-1111111102111111102107");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("1,111111101");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-1,111111101");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("1111111102111111102108,111111101");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-1111111102111111102108,111111101");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer8()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9,123456789");
			BigNumberDS rhs = new BigNumberDS("1126352680876543201873,5020587268");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer9()
		{
			BigNumberDS lhs = /*   */ new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = /*                    */ new BigNumberDS("-9,98765");
			BigNumberDS rhs = /* */ new BigNumberDS("-1233043199888893199885,56399319865585");

			Assert.AreEqual(rhs, lhs * added);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkWithMinusOneCorrectly()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreEqual(-lhs, lhs * -1);
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkWithNullCorrectly()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreEqual("0", (lhs * 0).ToString());
		}

		[TestMethod]
		public void OperatorMultiplicativeMultipleMustWorkWithOneCorrectly()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789123456789123");

			Assert.AreEqual(lhs, lhs * 1);
		}

		#endregion binary*

		#region binary/

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("13");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-13");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("13717421013717421013");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-13717421013717421013");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("0");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("0");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("9");
			BigNumberDS rhs = new BigNumberDS("13717421013717421013");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");
			BigNumberDS added = new BigNumberDS("-9");
			BigNumberDS rhs = new BigNumberDS("-13717421013717421013");

			Assert.AreEqual(rhs, lhs / added);
		}

		[TestMethod]
		public void OperatorMultiplicativeDividerOnDividingByZeroMustThrowDivByZeroExc()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123456789123,123456789");

			var result = lhs / 0;
		}

		#endregion binary/

		#region operator==

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("123");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("-123");
			BigNumberDS rhs = new BigNumberDS("-123");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123");
			BigNumberDS rhs = new BigNumberDS("123456789123");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123");
			BigNumberDS rhs = new BigNumberDS("-123456789123");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("3,123456789");
			BigNumberDS rhs = new BigNumberDS("3,123456789");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("-3,123456789");
			BigNumberDS rhs = new BigNumberDS("-3,123456789");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123,123456789");

			Assert.AreEqual(true, lhs == rhs);
		}

		[TestMethod]
		public void OperatorEqualatyMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("-123456789123,123456789");

			Assert.AreEqual(true, lhs == rhs);
		}

		#endregion operator==

		#region operator!=

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("121");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("-123");
			BigNumberDS rhs = new BigNumberDS("-113");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123");
			BigNumberDS rhs = new BigNumberDS("12345679123");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer3()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123");
			BigNumberDS rhs = new BigNumberDS("-12345789123");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer4()
		{
			BigNumberDS lhs = new BigNumberDS("3,123456789");
			BigNumberDS rhs = new BigNumberDS("3,12345789");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer5()
		{
			BigNumberDS lhs = new BigNumberDS("-3,123456789");
			BigNumberDS rhs = new BigNumberDS("-3,12345789");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer6()
		{
			BigNumberDS lhs = new BigNumberDS("123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123,12356789");

			Assert.AreEqual(true, lhs != rhs);
		}

		[TestMethod]
		public void OperatorNonEqualatyMustWorkCorrectlyVer7()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("-12345678923,123456789");

			Assert.AreEqual(true, lhs != rhs);
		}

		#endregion operator!=

		#region gethashcode

		[TestMethod]
		public void HashCodesFromDifferentObjectMustNotBeSame()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789123,123456789");

			Assert.AreNotEqual(lhs.GetHashCode(), rhs.GetHashCode());
		}

		[TestMethod]
		public void HashCodesFromEqualsObjectMustBeSame()
		{
			BigNumberDS lhs = new BigNumberDS("-123456789123,123456789");
			BigNumberDS rhs = new BigNumberDS("-123456789123,123456789");

			Assert.AreEqual(lhs.GetHashCode(), rhs.GetHashCode());
		}

		[TestMethod]
		public void HashCodesFromObjectMustBeSame()
		{
			BigNumberDS obj = new BigNumberDS("-123456789123,123456789");

			Assert.AreEqual(obj.GetHashCode(), obj.GetHashCode());
		}

		#endregion gethashcode

		#region equals

		[TestMethod]
		public void EqualsMustBeAssociativeVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS mhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("123");

			Assert.AreEqual(lhs.Equals(mhs) && mhs.Equals(rhs), lhs.Equals(rhs));
		}

		[TestMethod]
		public void EqualsMustBeAssociativeVer1()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS mhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("0,123456789");

			Assert.AreEqual(lhs.Equals(mhs) && mhs.Equals(rhs), lhs.Equals(rhs));
		}

		[TestMethod]
		public void EqualsMustBeAssociativeVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789,123456789");
			BigNumberDS mhs = new BigNumberDS("123456789,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789,123456789");

			Assert.AreEqual(lhs.Equals(mhs) && mhs.Equals(rhs), lhs.Equals(rhs));
		}

		[TestMethod]
		public void EqualsMustBeCommutativeVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("123");

			Assert.AreEqual(lhs.Equals(rhs), rhs.Equals(lhs));
		}

		[TestMethod]
		public void EqualsMustBeCommutativeVer1()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("0,123456789");

			Assert.AreEqual(lhs.Equals(rhs), rhs.Equals(lhs));
		}

		[TestMethod]
		public void EqualsMustBeCommutativeVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789,123456789");

			Assert.AreEqual(lhs.Equals(rhs), rhs.Equals(lhs));
		}

		[TestMethod]
		public void EqualsToItselfMustReturnTrueVer0()
		{
			BigNumberDS obj = new BigNumberDS("123");

			Assert.AreEqual(true, obj.Equals(obj));
		}

		[TestMethod]
		public void EqualsToItselfMustReturnTrueVer1()
		{
			BigNumberDS obj = new BigNumberDS("0,123456789");

			Assert.AreEqual(true, obj.Equals(obj));
		}

		[TestMethod]
		public void EqualsToItselfMustReturnTrueVer2()
		{
			BigNumberDS obj = new BigNumberDS("123456789,123456789");

			Assert.AreEqual(true, obj.Equals(obj));
		}

		[TestMethod]
		public void EqualsToNullMustReturnFalse()
		{
			BigNumberDS obj = new BigNumberDS("123");

			Assert.AreEqual(false, obj.Equals(null));
		}

		#endregion equals

		#region operatorless/greater

		[TestMethod]
		public void OperatorGreaterMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("123");
			BigNumberDS rhs = new BigNumberDS("122");

			Assert.AreEqual(true, lhs > rhs);
		}

		[TestMethod]
		public void OperatorGreaterMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("0,123456789");
			BigNumberDS rhs = new BigNumberDS("0,023456789");

			Assert.AreEqual(true, lhs > rhs);
		}

		[TestMethod]
		public void OperatorGreaterMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456789,123456789");
			BigNumberDS rhs = new BigNumberDS("123456788,123456789");

			Assert.AreEqual(true, lhs > rhs);
		}

		[TestMethod]
		public void OperatorLessMustWorkCorrectlyVer0()
		{
			BigNumberDS lhs = new BigNumberDS("122");
			BigNumberDS rhs = new BigNumberDS("123");

			Assert.AreEqual(true, lhs < rhs);
		}

		[TestMethod]
		public void OperatorLessMustWorkCorrectlyVer1()
		{
			BigNumberDS lhs = new BigNumberDS("0,023456789");
			BigNumberDS rhs = new BigNumberDS("0,123456789");

			Assert.AreEqual(true, lhs < rhs);
		}

		[TestMethod]
		public void OperatorLessMustWorkCorrectlyVer2()
		{
			BigNumberDS lhs = new BigNumberDS("123456788,123456789");
			BigNumberDS rhs = new BigNumberDS("123456789,123456789");

			Assert.AreEqual(true, lhs < rhs);
		}

		#endregion operatorless/greater

		#endregion operators

		#region tostring

		[TestMethod]
		public void ToStringNegativeVer0()
		{
			BigNumberDS obj = new BigNumberDS("-123");

			Assert.AreEqual("-123", obj.ToString());
		}

		[TestMethod]
		public void ToStringNegativeVer1()
		{
			BigNumberDS obj = new BigNumberDS("-0,123456");

			Assert.AreEqual("-0,123456", obj.ToString());
		}

		[TestMethod]
		public void ToStringNegativeVer2()
		{
			BigNumberDS obj = new BigNumberDS("-123456789123456,12345678912345679");

			Assert.AreEqual("-123456789123456,12345678912345679", obj.ToString());
		}

		[TestMethod]
		public void ToStringPositiveVer0()
		{
			BigNumberDS obj = new BigNumberDS("123");

			Assert.AreEqual("123", obj.ToString());
		}

		[TestMethod]
		public void ToStringPositiveVer1()
		{
			BigNumberDS obj = new BigNumberDS("0,123456");

			Assert.AreEqual("0,123456", obj.ToString());
		}

		[TestMethod]
		public void ToStringPositiveVer2()
		{
			BigNumberDS obj = new BigNumberDS("123456789123456,12345678912345679");

			Assert.AreEqual("123456789123456,12345678912345679", obj.ToString());
		}

        #endregion tostring

        [TestMethod]
        public void TrimStructurePositiveVer1()
        {
            BigNumberDS obj = new BigNumberDS("21002000000000000000000");
            BigNumberDSHelper.TrimStructure(ref obj);

            Assert.AreEqual("21002000000000000000000", obj.ToString());
        }
    }
}