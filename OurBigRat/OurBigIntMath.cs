﻿using System;
using System.Linq;
namespace OurBigRat
{
	public static class OurBigIntMath
	{
		

		internal static OurBigInt Add(OurBigInt lhs, OurBigInt rhs)
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

			OurBigInt currentFirst;
			OurBigInt currentSecond;

			if (lhs > rhs)
			{
				currentFirst = lhs.Clone();
				currentSecond = rhs.Clone();
			}
			else
			{
				currentFirst = rhs.Clone();
				currentSecond = lhs.Clone();
			}

			OurBigInt result = Add(currentFirst, currentSecond, false);

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}

		private static OurBigInt Add(OurBigInt lhs, OurBigInt rhs, bool addBit)
		{
			if (lhs < rhs)
			{
				throw new ArgumentException("Left memder of addition must be bigger than right one.");
			}

			OurBigInt result = new OurBigInt();

			if (lhs != null && rhs != null)
			{
				OurBigIntMathHelper.BitArraySum(lhs.value, rhs.value, result.value, ref addBit);

				result.previousBlock = OurBigIntMath.Add(lhs.previousBlock, rhs.previousBlock, addBit);
			}
			else if (lhs != null)
			{
				OurBigIntMathHelper.BitArraySum(lhs.value, null, result.value, ref addBit);

				result.previousBlock = OurBigIntMath.Add(lhs.previousBlock, null, addBit);
			}
			else if (addBit)
			{
				result = new OurBigInt(1);
			}

			return result;
		}

		internal static OurBigInt Subtract(OurBigInt lhs, OurBigInt rhs)
		{
			if (lhs < rhs)
			{
				throw new ArgumentException($"{nameof(OurBigInt)} can not be negative");
			}

			throw new NotImplementedException();
		}

		internal static OurBigInt Multiple(OurBigInt lhs, OurBigInt rhs)
		{
			OurBigInt currentLeft = lhs;
			OurBigInt currentRight = rhs;

			OurBigInt result = new OurBigInt();

			foreach (var curRightBit in currentRight)
			{
			}

			throw new NotImplementedException();
		}

		internal static OurBigInt Divide(OurBigInt lhs, OurBigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal static OurBigInt Not(OurBigInt lhs)
		{
			OurBigInt result = new OurBigInt();
			OurBigInt tmp = result;

			OurBigInt lhscopy = lhs;

			while (lhscopy != null)
			{
				for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
				{
					tmp.value[i] = !lhscopy.value[i];
				}

				lhscopy = lhscopy.previousBlock;

				if ((lhscopy != null) && (tmp.previousBlock == null))
				{
					tmp = OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
				}

				tmp = tmp.previousBlock;
			}

			return result;
		}

		internal static OurBigInt Xor(OurBigInt lhs, OurBigInt rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x ^ y);
		}

		internal static OurBigInt Or(OurBigInt lhs, OurBigInt rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x || y);
		}

		internal static OurBigInt And(OurBigInt lhs, OurBigInt rhs)
		{
			return OurBigIntMath.BitwiseOperation(lhs, rhs, (x, y) => x && y);
		}

		private static OurBigInt BitwiseOperation(OurBigInt lhs, OurBigInt rhs, Func<bool, bool, bool> func)
		{
			OurBigInt result = new OurBigInt();

			OurBigInt lhscopy = lhs;
			OurBigInt rhscopy = rhs;
			OurBigInt resultcopy = result;

			while (lhscopy != null)
			{
				for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
				{
					resultcopy.value[i] = func?.Invoke(lhscopy.value[i], rhscopy.value[i]) ?? default(bool);
				}

				lhscopy = lhscopy.previousBlock;
				rhscopy = rhscopy.previousBlock;

				if ((lhscopy != null) &&
						(resultcopy.previousBlock == null))
				{
					resultcopy = OurBigIntMathHelper.AddNewPreviousBlock(resultcopy, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
				}

				resultcopy = resultcopy.previousBlock;
			}

			return result;
		}

		internal static OurBigInt Reminder(OurBigInt lhs, OurBigInt rhs)
		{
			OurBigInt lhscopy = lhs.DeepClone();
			OurBigInt rhscopy = rhs.DeepClone();

			OurBigInt max = null;
			OurBigInt min = null;

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

			while (max > min)
			{
				max -= min;
			}

			return max;
		}

		internal static OurBigInt RightShift(OurBigInt num, int shift)
		{
			OurBigInt result = new OurBigInt();
			OurBigInt tmp = result;
			OurBigInt numcopy = num;

			if (shift < OurBigInt.BOOL_ARRAY_SIZE)
			{
				while (numcopy != null)
				{
					tmp.value = OurBigIntMathHelper.BoolArrayRightShift(numcopy.value, shift);

					numcopy = numcopy.previousBlock;

					if (numcopy != null && tmp.previousBlock == null)
					{
						tmp = OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
					}
				}
			}
			else
			{
				shift -= OurBigInt.BOOL_ARRAY_SIZE;
				numcopy = numcopy.previousBlock;

				if (numcopy == null) // if shift is too big for this number.
				{
					return new OurBigInt();
				}

				result = OurBigIntMath.RightShift(numcopy, shift); // TODO to ask goto?
			}

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}

		internal static OurBigInt LeftShift(OurBigInt num, int shift)
		{
			OurBigInt result = new OurBigInt();
			OurBigInt tmp = result;
			OurBigInt numcopy = num;

			if (shift < OurBigInt.BOOL_ARRAY_SIZE)
			{
				while (numcopy != null)
				{
					tmp.value = OurBigIntMathHelper.BoolArrayLeftShift(numcopy.value, shift);

					numcopy = numcopy.previousBlock;

					if (numcopy != null && tmp.previousBlock == null)
					{
						tmp = OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
					}
				}
			}
			else
			{
				shift -= OurBigInt.BOOL_ARRAY_SIZE;
				numcopy = numcopy.previousBlock;

				if (numcopy == null) // if shift is too big for this number.
				{
					return new OurBigInt();
				}

				result = OurBigIntMath.RightShift(numcopy, shift); // TODO to ask goto?
			}

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}
	}
}