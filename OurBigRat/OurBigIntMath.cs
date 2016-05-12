﻿namespace OurBigRat
{
	using System;
	using bigint = OurBigInt;

	public static class OurBigIntMath
	{
		internal static bigint Add(bigint lhs, bigint rhs)
		{
			if ((lhs == null) || (rhs == null))
			{
				return null;
			}

			if (lhs == 0)
			{
				return rhs;
			}

			if (rhs == 0)
			{
				return lhs;
			}

			bigint currentFirst;
			bigint currentSecond;

			if (lhs > rhs)
			{
				currentFirst = lhs.DeepClone();
				currentSecond = rhs.DeepClone();
			}
			else
			{
				currentFirst = rhs.DeepClone();
				currentSecond = lhs.DeepClone();
			}

			bigint result = Add(currentFirst, currentSecond, false);

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}

		private static bigint Add(bigint lhs, bigint rhs, bool addBit)
		{
			if (lhs < rhs)
			{
				throw new ArgumentException("Left memder of addition must be bigger than right one.");
			}

			bigint result = new bigint();

			if (lhs != null && rhs != null)
			{
				OurBigDigitMathHelper.DigitSum(lhs.digit, rhs.digit, result.digit, ref addBit);

				result.previousBlock = OurBigIntMath.Add(lhs.previousBlock, rhs.previousBlock, addBit);
			}
			else if (lhs != null)
			{
				OurBigDigitMathHelper.DigitSum(lhs.digit, null, result.digit, ref addBit);

				result.previousBlock = OurBigIntMath.Add(lhs.previousBlock, null, addBit);
			}
			else if (addBit)
			{
				result = new bigint(1);
			}

			return result;
		}

		internal static bigint Subtract(bigint lhs, bigint rhs)
		{
			if (lhs == null || rhs == null)
			{
				throw new ArgumentNullException();
			}
			else if (lhs < rhs)
			{
				throw new ArgumentException("Second member of substraction can not be bigger than first one.");
			}
			else if (lhs == rhs)
			{
				return new bigint(0);
			}

			bigint firstMember = lhs.DeepClone();
			bigint secondMember = !rhs.DeepClone();

			secondMember = secondMember + 1;

			int firstMemberBlockCount = OurBigIntMathHelper.GetBlocksCount(firstMember);
			int secondMemberBlockCount = OurBigIntMathHelper.GetBlocksCount(secondMember);

			if (firstMemberBlockCount > secondMemberBlockCount)
			{
				OurBigIntMathHelper.AddNTrueFilledBlocks(secondMember, firstMemberBlockCount - secondMemberBlockCount);
			}

			bigint result = secondMember + firstMember;

			OurBigIntMathHelper.TrimByBlocksCount(result, firstMemberBlockCount);

			return result;
		}

		internal static bigint Multiple(bigint lhs, bigint rhs)
		{
			bigint currentLeft = lhs;
			bigint currentRight = rhs;

			bigint result = new bigint();

			result = OurBigIntMath.BoothsMultiplication(lhs, rhs);

			return result;
		}

		private static bigint BoothsMultiplication(bigint lhs, bigint rhs)
		{
			ulong x = (uint)OurBigIntMathHelper.GetBlocksCount(lhs) * bigint.BOOL_ARRAY_SIZE;
			ulong y = (uint)OurBigIntMathHelper.GetBlocksCount(rhs) * bigint.BOOL_ARRAY_SIZE;

			bigint A = lhs.DeepClone();
			bigint S = lhs.DeepClone();
			bigint P = rhs.DeepClone();

			return null;
		}

		private static bigint BoothsMultiplication(bigint a, bigint s, bigint p, ulong x, ulong y)
		{
			throw new NotImplementedException();
		}

		internal static bigint Divide(bigint lhs, bigint rhs)
		{
			throw new NotImplementedException();
		}

		internal static bigint Not(bigint num)
		{
			bigint result = new bigint();
			bigint tmp = result;

			bigint lhscopy = num;

			while (lhscopy != null)
			{
				for (int i = 0; i < bigint.BOOL_ARRAY_SIZE; i++)
				{
					tmp.digit.Value[i] = !lhscopy.digit.Value[i];
				}

				lhscopy = lhscopy.previousBlock;

				if ((lhscopy != null) && (tmp.previousBlock == null))
				{
					OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[bigint.BOOL_ARRAY_SIZE]);
				}

				tmp = tmp.previousBlock;
			}

			return result;
		}

		internal static bigint Xor(bigint lhs, bigint rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x ^ y);
		}

		internal static bigint Or(bigint lhs, bigint rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x || y);
		}

		internal static bigint And(bigint lhs, bigint rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x && y);
		}

		private static bigint BitwiseOperation(bigint lhs, bigint rhs, Func<bool, bool, bool> func)
		{
			bigint result = new bigint();

			bigint lhscopy = lhs;
			bigint rhscopy = rhs;
			bigint resultcopy = result;

			while (lhscopy != null)
			{
				for (int i = 0; i < bigint.BOOL_ARRAY_SIZE; i++)
				{
					resultcopy.digit.Value[i] = func?.Invoke(lhscopy.digit.Value[i], rhscopy.digit.Value[i]) ?? default(bool);
				}

				lhscopy = lhscopy.previousBlock;
				rhscopy = rhscopy.previousBlock;

				if ((lhscopy != null) &&
						(resultcopy.previousBlock == null))
				{
					resultcopy = OurBigIntMathHelper.AddNewPreviousBlock(resultcopy, new bool[bigint.BOOL_ARRAY_SIZE]);
				}

				resultcopy = resultcopy.previousBlock;
			}

			return result;
		}

		internal static bigint Reminder(bigint lhs, bigint rhs)
		{
			bigint lhscopy = lhs.DeepClone();
			bigint rhscopy = rhs.DeepClone();

			bigint max = null;
			bigint min = null;

			if (lhscopy > rhscopy)
			{
				max = lhscopy;
				min = rhscopy;
			}
			else
			{
				min = lhscopy;
				max = rhscopy;
			}

			while (max >= min)
			{
				max -= min;
			}

			return max;
		}

		internal static bigint RightShift(bigint num, int shift)
		{
			#region optimization

			if (num == 0)
			{
				return new bigint();
			}

			#endregion optimization

			bigint result = new bigint();
			bigint tmp = result;
			bigint numcopy = num;

			if (shift < bigint.BOOL_ARRAY_SIZE)
			{
				while (numcopy != null)
				{
					tmp.digit = OurBigDigitMathHelper.DigitRightShift(numcopy.digit, shift);

					numcopy = numcopy.previousBlock;

					if (numcopy != null && tmp.previousBlock == null)
					{
						tmp = OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[bigint.BOOL_ARRAY_SIZE]);
					}
				}
			}
			else
			{
				shift -= bigint.BOOL_ARRAY_SIZE;
				numcopy = numcopy.previousBlock;

				if (numcopy == null) // if shift is too big for this number.
				{
					return new bigint();
				}

				result = OurBigIntMath.RightShift(numcopy, shift); // TODO to ask goto?
			}

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}

		internal static bigint LeftShift(bigint input, int shift)
		{
			bigint result = new bigint();
			bigint currentResult = result;
			bigint currentInput = input;

			int globalStep = 0;
			int globalStepsCount = OurBigIntMathHelper.GetBlocksCount(input) * bigint.BOOL_ARRAY_SIZE + shift;
			int currentResultPos = 0;
			int currentInputPos = 0;

			while (globalStep < shift)
			{
				currentResult.digit.Value[currentResultPos] = false;

				currentResultPos++;
				globalStep++;

				if (currentResultPos == bigint.BOOL_ARRAY_SIZE)
				{
					currentResult.previousBlock = new bigint();

					currentResult = currentResult.previousBlock;

					currentResultPos = 0;
				}
			}

			while (globalStep != globalStepsCount)
			{
				currentResult.digit.Value[currentResultPos] = currentInput.digit.Value[currentInputPos];

				currentInputPos++;
				currentResultPos++;
				globalStep++;

				if (currentResultPos == bigint.BOOL_ARRAY_SIZE)
				{
					currentResult.previousBlock = new bigint();

					currentResult = currentResult.previousBlock;

					currentResultPos = 0;
				}

				if (currentInputPos == bigint.BOOL_ARRAY_SIZE)
				{
					currentInput = currentInput.previousBlock;

					currentInputPos = 0;
				}
			}

			return result;
		}
	}
}