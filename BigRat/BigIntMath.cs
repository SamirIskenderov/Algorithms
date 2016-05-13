namespace BigRat
{
	using System;

	internal class BigIntMath : Singleton<BigIntMath>
	{
		private BigIntMathHelper mathHelper = BigIntMathHelper.Instance;

		private BigIntMath()
		{
		}

		internal BigInt Add(BigInt bigInt, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Divide(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Multiple(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Reminder(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Subtract(BigInt lhs, BigInt one)
		{
			throw new NotImplementedException();
		}
	}
}