﻿using System;
using System.Collections.Generic;

namespace OurBigRat
{
	internal static class OurBigIntMathHelper
	{
		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from top order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		internal static IEnumerable<bool> GetNextBit(ulong num)
		{
            if (num == 0)
            {
                yield return false;
            }

            while (num != 0)
            {
                if (num % 2 == 1)
                {
                    num = (num - 1) / 2;

                    yield return true;
                }
                else
                {
                    num = num / 2;

                    yield return false;
                }
            }

			//ulong div = NextPowerOfTwo(num);
			//bool bit;

			//	num++; // quick fix

			//while ((div > 0) || (num > 1))
			//{
			//	if (num >= div)
			//	{
			//		num -= div;
			//		bit = true;
			//	}
			//	else
			//	{
			//		bit = false;
			//	}

			//	div /= 2;

			//	yield return bit;
			//}
		}

        internal static void BitArraySum(bool[] lhs, bool[] rhs, bool[] result, out bool bitOverflow)
        {
            if (lhs == null ||
                rhs == null ||
                result == null)
            {
                throw new ArgumentNullException();
            }
            else if (lhs.Length != rhs.Length ||
                rhs.Length != result.Length)
            {
                throw new ArgumentException("Input arrays length must be equal.");
            }

            bitOverflow = false;
            bool addBit = false;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = lhs[i] ^ rhs[i] ^ addBit;
                addBit = (lhs[i] && rhs[i]) || ((lhs[i] || rhs[i]) && addBit);

                //if (lhs[i] && rhs[i])
                //{
                //    result[i] = false || addBit;
                //    addBit = true;
                //}
                //else if (lhs[i] ^ rhs[i])
                //{
                //    result[i] = true && !addBit;
                //}
                //else
                //{
                //    result[i] = addBit;
                //    addBit = false;
                //}

                if (i == OurBigInt.BOOL_ARRAY_SIZE - 1)
                {
                    bitOverflow = addBit;
                }
            }
        }

		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		internal static ulong BitsToNumber(bool[] bits)
		{
			ulong div = 1;
			ulong result = 0;

			for (int i = 0; i < bits.Length; i++)
			{
				if (bits[i])
				{
					result += div;
				}

				div *= 2;
			}

			return result;
		}

		/// <summary>
		/// Compute next highest power of 2, f.e. for 114 it returns 128
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		internal static ulong NextPowerOfTwo(ulong v)
		{
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;

			return v;
		}

		internal static int GetBlocksCount(OurBigInt input)
		{
			int result = 0;
			OurBigInt current = input;

			while ((object)current != null)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		internal static OurBigInt AddNewPreviousBlock(OurBigInt result, bool[] currentSum)
		{
			OurBigInt current = result;

			OurBigInt addingBlock = new OurBigInt(currentSum);

			if (current == null)
			{
				return addingBlock;
			}

			while (current.previousBlock != null)
			{
				current = current.previousBlock;
			}

			current.previousBlock = addingBlock;

			return result;
		}
	}
}