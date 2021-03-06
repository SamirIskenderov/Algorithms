﻿using OurBigRat;
using System;
using System.Linq;
using Xunit;

namespace Algorithms.Test
{
	public class BigRatUnitTest
	{
		#region operators

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 0)]
		[InlineData(1, 0, 0)]
		[InlineData(1, 1, 1)]
		[InlineData(123, 321, 65)]
		[InlineData(78945, 1321, 1057)]
		[InlineData(1232, 0, 0)]
		[InlineData(0, 87455, 0)]
		[InlineData(85231, 589742, 19630)]
		[InlineData(10001, 5452, 1280)]
		[InlineData(123456789123, 4045432065982464, 53888483328)]
		public void OperatorAndMustWork(ulong l, ulong r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs & rhs);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(12, 12)]
		[InlineData(156, 874)]
		[InlineData(321, 99984)]
		[InlineData(0, 123456)]
		[InlineData(654231, 987)]
		[InlineData(695451, 6512)]
		[InlineData(18446744073709551615, 0)]
		[InlineData(18446744073709551615, 12355121578451)]
		[InlineData(18446744073709551615, 18446744073709551615)]
		public void OperatorEquallyMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l == r, lhs == rhs);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(12, 12)]
		[InlineData(156, 874)]
		[InlineData(321, 99984)]
		[InlineData(0, 123456)]
		[InlineData(654231, 987)]
		[InlineData(695451, 6512)]
		[InlineData(123456789123, 4045432065982464)]
		[InlineData(4045432065982464, 123456789123)]
		[InlineData(4045432065982464, 4045432065982464)]
		public void OperatorGreaterMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l > r, lhs > rhs);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(12, 12)]
		[InlineData(156, 874)]
		[InlineData(321, 99984)]
		[InlineData(0, 123456)]
		[InlineData(654231, 987)]
		[InlineData(695451, 6512)]
		[InlineData(123456789123, 4045432065982464)]
		[InlineData(4045432065982464, 123456789123)]
		[InlineData(4045432065982464, 4045432065982464)]
		public void OperatorGreaterOrEquallMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l >= r, lhs >= rhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 5, 0)]
		[InlineData(1, 2, 4)]
		[InlineData(7, 5, 224)]
		[InlineData(7, 1, 14)]
		[InlineData(12358, 4, 197728)]
		[InlineData(987654321, 11, 2022716049408)]
		[InlineData(123456789123, 15, 4045432065982464)]
		public void OperatorLeftShiftMustWork(ulong l, int r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt or = new OurBigInt(result);

			Assert.Equal<OurBigInt>(or, lhs << r);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(12, 12)]
		[InlineData(156, 874)]
		[InlineData(321, 99984)]
		[InlineData(0, 123456)]
		[InlineData(654231, 987)]
		[InlineData(695451, 6512)]
		[InlineData(123456789123, 4045432065982464)]
		[InlineData(4045432065982464, 123456789123)]
		[InlineData(4045432065982464, 4045432065982464)]
		public void OperatorLessMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l < r, lhs < rhs);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(12, 12)]
		[InlineData(156, 874)]
		[InlineData(321, 99984)]
		[InlineData(0, 123456)]
		[InlineData(654231, 987)]
		[InlineData(695451, 6512)]
		[InlineData(123456789123, 4045432065982464)]
		[InlineData(4045432065982464, 123456789123)]
		[InlineData(4045432065982464, 4045432065982464)]
		public void OperatorLessOrEquallMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l <= r, lhs <= rhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(1, 0, 1)]
		[InlineData(1, 1, 0)]
		[InlineData(12, 12, 0)]
		[InlineData(654231, 987, 653244)]
		[InlineData(65421574231, 213451235, 65208122996)]
		[InlineData(18446744073709551615, 18446744073709551615, 0)]
		public void OperatorMinusMustWork(ulong l, ulong r, ulong res)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt result = new OurBigInt(res);

			Assert.Equal<OurBigInt>(result, lhs - rhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 0)]
		[InlineData(123, 0, 0)]
		[InlineData(5, 2, 10)]
		[InlineData(987, 456, 450072)]
		[InlineData(9223372036854775807, 2, 18446744073709551614)]
		public void TmpDigitTest(ulong l, ulong r, ulong result)
		{
			foreach (var item in OurBigDigitMathHelper.GetNextBit(l))
			{
				Console.Write(item);
			}

			bool[] larr = OurBigDigitMathHelper.GetNextBit(l).ToArray();
			bool[] rarr = OurBigDigitMathHelper.GetNextBit(r).ToArray();
			bool[] resultarr = OurBigDigitMathHelper.GetNextBit(result).ToArray();

			OurBigDigit lhs = new OurBigDigit(larr);
			OurBigDigit rhs = new OurBigDigit(rarr);
			OurBigDigit res = new OurBigDigit(resultarr);

			OurBigDigit trash = new OurBigDigit();

			Assert.Equal<OurBigDigit>(res, OurBigDigitMath.DigitMultiple(lhs, rhs, out trash));
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 1)]
		[InlineData(123, 123, 246)]
		[InlineData(9875, 622210, 632085)]
		[InlineData(123456789123, 987654321987, 1111111111110)]
		[InlineData(123456789123, 13982164348, 137438953471)]
		[InlineData(4045432065982464, 458167561388031, 4503599627370495)]
		public void OperatorMultiplicativePlusMustWork(ulong l, ulong r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs + rhs);
		}

		[Theory]
		[InlineData(45, 7, 3)]
		//[InlineData(12589995, 2, 1)]
		//[InlineData(12589994, 2, 0)]
		[InlineData(123456789123, 987654, 39123)]
		[InlineData(987654321987, 123456789123, 9003)]
		public void OperatorMultiplicativeReminderMustWork(ulong l, ulong r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs % rhs);
		}

		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(123, 4)]
		[InlineData(123456789, 10760938)]
		[InlineData(123456789123, 13982164348)]
		[InlineData(4045432065982464, 458167561388031)]
		public void OperatorNotMustWork(ulong l, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, !lhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 1)]
		[InlineData(1, 0, 1)]
		[InlineData(1, 1, 1)]
		[InlineData(123, 321, 379)]
		[InlineData(78945, 1321, 79209)]
		[InlineData(1232, 0, 1232)]
		[InlineData(0, 87455, 87455)]
		[InlineData(85231, 589742, 655343)]
		[InlineData(10001, 5452, 14173)]
		[InlineData(123456789123, 4045432065982464, 4045501634288259)]
		public void OperatorOrMustWork(ulong l, ulong r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs | rhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 5, 0)]
		[InlineData(1, 2, 0)]
		[InlineData(7, 5, 0)]
		[InlineData(7, 1, 3)]
		[InlineData(12358, 4, 772)]
		[InlineData(987654321, 11, 482253)]
		[InlineData(123456789123, 12345, 0)]
		[InlineData(123456789123456789, 50, 109)]
		public void OperatorRightShiftMustWork(ulong l, int r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt or = new OurBigInt(result);

			Assert.Equal<OurBigInt>(or, lhs >> r);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 1, 1)]
		[InlineData(1, 0, 1)]
		[InlineData(1, 1, 0)]
		[InlineData(123, 321, 314)]
		[InlineData(78945, 1321, 78152)]
		[InlineData(1232, 0, 1232)]
		[InlineData(0, 87455, 87455)]
		[InlineData(85231, 589742, 635713)]
		[InlineData(10001, 5452, 12893)]
		[InlineData(123456789123, 4045432065982464, 4045447745804931)]
		public void OperatorXorMustWork(ulong l, ulong r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs ^ rhs);
		}

		#endregion operators

		#region funcs

		[Theory]
		[InlineData(0)]
		[InlineData(123)]
		[InlineData(9875)]
		[InlineData(123456789123)]
		[InlineData(4045432065982464)]
		public void MethodDeepCloneMustWork(ulong v)
		{
			OurBigInt num = new OurBigInt(v);

			Assert.Equal<OurBigInt>(num, num.DeepClone());
		}

		#endregion funcs
	}
}