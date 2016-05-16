namespace Algorithms.BigRat
{
	using System;
	using bigint = BigInt;

	internal class BigIntMath : Singleton<BigIntMath>
	{
		private readonly BigIntMathHelper mathHelper = BigIntMathHelper.Instance;

		private BigIntMath()
		{
		}

		internal bigint Add(bigint bigint, bigint rhs)
		{
			throw new NotImplementedException();
		}

		internal bigint Divide(bigint lhs, bigint rhs)
		{
			throw new NotImplementedException();
		}

		internal bigint Multiple(bigint lhs, bigint rhs)
		{
			bigint result = new bigint();

			int lBlockCount = mathHelper.GetBlocksCount(lhs);
			int rBlockCount = mathHelper.GetBlocksCount(rhs);

			bigint min = null;
			bigint max = null;

			if (lBlockCount < rBlockCount)
			{
				min = lhs.DeepClone();
				max = rhs.DeepClone();
			}
			else
			{
				max = lhs.DeepClone();
				min = rhs.DeepClone();
			}

			bigint current = result;

			while (max != null)
			{
				long aspt = (long)max.value * (long)min.value;

				if (aspt > uint.MaxValue)
				{
					result.value = uint.MaxValue;

					bigint over = new bigint
					{
						value = (uint)(aspt - uint.MaxValue)
					};

					max += over;
				}
				else
				{
					result.value = (uint)aspt;
				}

				max = max.previousBlock;
				min = min.previousBlock;

				if (max != null)
				{
					result.previousBlock = new bigint();
					result = result.previousBlock;
				}
			}

			return result;
		}

		internal bigint Reminder(bigint lhs, bigint rhs)
		{
			throw new NotImplementedException();
		}

		internal bigint Subtract(bigint lhs, bigint rhs)
		{
			bigint result = new bigint();

			int lBlockCount = mathHelper.GetBlocksCount(lhs);
			int rBlockCount = mathHelper.GetBlocksCount(rhs);

			bigint min = null;
			bigint max = null;

			if (lBlockCount < rBlockCount)
			{
				min = lhs.DeepClone();
				max = rhs.DeepClone();
			}
			else
			{
				max = lhs.DeepClone();
				min = rhs.DeepClone();
			}

			bigint current = result;

			while (max != null)
			{
				long aspt = max.value - min.value;

				if (aspt < uint.MinValue)
				{
					aspt += uint.MaxValue;
					max.previousBlock.value -= uint.MaxValue;
				}

				result.value = (uint)aspt;

				max = max.previousBlock;
				min = min.previousBlock;

				if (max != null)
				{
					result.previousBlock = new bigint();
					result = result.previousBlock;
				}
			}

			return result;
		}
	}
}