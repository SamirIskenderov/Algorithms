﻿using System;
using System.Linq;

namespace OurBigRat
{
	internal static class OurBigIntMath
	{
		internal static OurBigInt Add(OurBigInt lhs, OurBigInt rhs)
		{
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

			return Add(currentFirst, currentSecond, false);
		}

		internal static OurBigInt Add(OurBigInt lhs, OurBigInt rhs, bool addBit)
		{
			if (lhs == null)
			{
				throw new ArgumentNullException();
			}
			else if (lhs < rhs)
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
			throw new NotImplementedException();
		}

		internal static OurBigInt Divide(OurBigInt lhs, OurBigInt rhs)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Bit !
		/// </summary>
		/// <param name="lhs"></param>
		/// <returns></returns>
		internal static OurBigInt Not(OurBigInt lhs)
		{
			throw new NotImplementedException();
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
			}

			return result;
		}

		/// <summary>
		/// ==
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		internal static bool Equally(OurBigInt lhs, OurBigInt rhs)
		{
			if (OurBigIntMathHelper.GetBlocksCount(lhs) != OurBigIntMathHelper.GetBlocksCount(rhs))
			{
				return false;
			}

			OurBigInt lhscopy = lhs;
			OurBigInt rhscopy = rhs;

			while ((object)lhscopy != null)
			{
				if (!lhscopy.value.SequenceEqual(rhscopy.value))
				{
					return false;
				}

				lhscopy = lhscopy.previousBlock;
				rhscopy = rhscopy.previousBlock;
			}

			return true;
		}

		internal static bool IsLess(OurBigInt lhs, OurBigInt rhs)
		{
			int lhsBlockCount = OurBigIntMathHelper.GetBlocksCount(lhs);
			int rhsBlockCount = OurBigIntMathHelper.GetBlocksCount(rhs);

			if (lhsBlockCount < rhsBlockCount)
			{
				return true;
			}

			if (lhsBlockCount > rhsBlockCount)
			{
				return false;
			}

			OurBigInt lhscopy = lhs;
			OurBigInt rhscopy = rhs;

			OurBigInt tmp = new OurBigInt();

			while (lhscopy != null)
			{
				for (int i = OurBigInt.BOOL_ARRAY_SIZE - 1; i >= 0; i--)
				{
					if (lhscopy.value[i] ^ rhscopy.value[i])
					{
						return rhscopy.value[i];
					}
				}

				lhscopy = lhscopy.previousBlock;
				rhscopy = rhscopy.previousBlock;
			}

			return false;
		}

		internal static OurBigInt RightShift(OurBigInt lhs, int rhs)
		{
			OurBigInt lhscopy = lhs;

			OurBigInt result = new OurBigInt();
			OurBigInt tmp = result;

			for (int j = 0; j < rhs; j++)
			{
				while (lhscopy != null)
				{
					for (int i = OurBigInt.BOOL_ARRAY_SIZE - 1; i > 0; i--)
					{
						tmp.value[i - 1] = lhscopy.value[i];
					}

					tmp.value[OurBigInt.BOOL_ARRAY_SIZE - 1] = false;

					lhscopy = lhscopy.previousBlock;
					if (tmp.previousBlock == null)
					{
						tmp = OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
					}
					tmp = tmp.previousBlock;
				}
			}

			return result;
		}

		internal static OurBigInt LeftShift(OurBigInt lhs, int rhs)
		{
			throw new NotImplementedException();
		}
	}
}