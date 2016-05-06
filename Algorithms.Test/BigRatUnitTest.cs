using OurBigRat;
using System;
using Xunit;

namespace Algorithms.Test
{
	public class BigRatUnitTest
	{
		#region operators

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
		public void OperatorEquallyMustWork(ulong l, ulong r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l == r, lhs == rhs);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 5, 0)]
		[InlineData(1, 2, 0)]
		[InlineData(7, 5, 0)]
		[InlineData(7, 1, 3)]
		[InlineData(12358, 4, 772)]
		[InlineData(987654321, 11, 482253)]
		public void OperatorRightShiftMustWork(ulong l, int r, ulong result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt or = new OurBigInt(result);

			Assert.Equal<OurBigInt>(or, lhs >> r);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(0, 5, 0)]
		[InlineData(1, 2, 4)]
		[InlineData(7, 5, 224)]
		[InlineData(7, 1, 14)]
		[InlineData(12358, 4, 197728)]
		[InlineData(987654321, 11, 2022716049408)]
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
		public void OperatorLessMustWork(uint l, uint r)
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
		public void OperatorLessOrEquallMustWork(uint l, uint r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l <= r, lhs <= rhs);
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
		public void OperatorGreaterMustWork(uint l, uint r)
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
		public void OperatorGreaterOrEquallMustWork(uint l, uint r)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);

			Assert.Equal(l >= r, lhs >= rhs);
		}

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
		public void OperatorAndMustWork(uint l, uint r, uint result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			OurBigInt output = lhs & rhs;
			Assert.Equal<OurBigInt>(res, output);
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
		public void OperatorOrMustWork(uint l, uint r, uint result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal< OurBigInt>(res, lhs | rhs);
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
		public void OperatorXorMustWork(uint l, uint r, uint result)
		{
			OurBigInt lhs = new OurBigInt(l);
			OurBigInt rhs = new OurBigInt(r);
			OurBigInt res = new OurBigInt(result);

			Assert.Equal<OurBigInt>(res, lhs ^ rhs);
		}

		#endregion operators

	}
}
