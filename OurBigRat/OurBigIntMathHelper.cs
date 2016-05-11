using System;
using System.Collections.Generic;
using System.Linq;

namespace OurBigRat
{
	public static class OurBigIntMathHelper
	{
		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from top order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static IEnumerable<bool> GetNextBit(ulong num)
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
		}

		internal static bool[] BoolArrayRightShift(bool[] arr, int shift)
		{
			bool[] result = new bool[arr.Length];

			for (int j = 0; j < shift; j++)
			{
				for (int i = shift; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
				{
					result[i - shift] = arr[i];
				}
			}

			return result;
		}

		internal static bool[] BoolArrayLeftShift(bool[] arr, int shift)
		{
			bool[] result = new bool[arr.Length];

			for (int j = 0; j < shift; j++)
			{
				for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE - shift; i++)
				{
					result[i + shift] = arr[i];
				}
			}

			return result;
		}

		/// <summary>
		/// If input has zero blocks in end of a fraction part or in start of integer part, this func will remove it.
		/// F.e., 0000000001230,000123000000 will be 00000123,000123000.
		/// </summary>
		/// <param name="input"></param>
		internal static void TrimStructure(ref OurBigInt input)
		{
			OurBigInt current = input;
			OurBigInt currentEdge;

			while (current != null && current.previousBlock != null)
			{
				if (current.previousBlock.value.All(x => x == false)) // if all elements of collection are zeros.
				{
					currentEdge = current;

					while (current.previousBlock != null)
					{
						if (current.previousBlock.value.Any(x => x == true))
						{
							break;
						}
						else if (current.previousBlock.previousBlock == null)
						{
							currentEdge.previousBlock = null;
							return;
						}

						current = current.previousBlock;
					}
				}

				current = current.previousBlock;
			}
		}

		/// <summary>
		/// Modifies result aray as sum of two arrays or sum of one bit and array.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <param name="result"></param>
		/// <param name="bitOverflow"></param>
		internal static void BitArraySum(bool[] lhs, bool[] rhs, bool[] result, ref bool bitOverflow)
		{
			if (lhs == null ||
			    result == null)
			{
				throw new ArgumentNullException();
			}
			else if ((rhs != null && (lhs.Length != rhs.Length || rhs.Length != result.Length)) ||
			    (lhs.Length != result.Length))
			{
				throw new ArgumentException("Input arrays length must be equal.");
			}
			if (rhs != null)
			{
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = lhs[i] ^ rhs[i] ^ bitOverflow;
					bitOverflow = (lhs[i] && rhs[i]) || ((lhs[i] || rhs[i]) && bitOverflow);

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
				}
			}
			else
			{
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = lhs[i] ^ bitOverflow;
					bitOverflow = lhs[i] && bitOverflow;
				}
			}
		}

		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		public static ulong BitsToNumber(bool[] bits)
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

			OurBigIntMathHelper.TrimStructure(ref addingBlock);

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

		internal static void AddNTrueFilledBlocks(OurBigInt input, int n)
		{
			if (n < 0)
			{
				throw new ArgumentException("Number of adding blocks can not be negative.");
			}
			else if (input == null)
			{
				throw new ArgumentNullException();
			}

			OurBigInt current = input;

			while (current.previousBlock != null)
			{
				current = current.previousBlock;
			}

			bool[] addingArr = new bool[OurBigInt.BOOL_ARRAY_SIZE];

			for (int i = 0; i < addingArr.Length; i++)
			{
				addingArr[i] = true;
			}

			while (n != 0)
			{
				current.previousBlock = new OurBigInt(addingArr);

				current = current.previousBlock;

				n--;
			}
		}

		internal static void TrimByBlocksCount(OurBigInt input, int n)
		{
			if (n <= 0)
			{
				throw new ArgumentException("Number of blocks can not be negative or zero.");
			}
			else if (input == null)
			{
				throw new ArgumentNullException();
			}

			OurBigInt current = input;

			n--;

			while (n != 0)
			{
				current = current.previousBlock;

				n--;
			}

			current.previousBlock = null;
		}
	}
}