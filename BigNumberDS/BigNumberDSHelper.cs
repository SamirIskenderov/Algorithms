using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using big = Algorithms.BigNumber.BigNumberDS;

namespace Algorithms.BigNumber
{
	public static class BigNumberDSHelper
	{
		public static bool GetHelpFromPreviousBlocks(big input)
		{
			big current = input;

			while (current.previousBlock != null)
			{
				current = current.previousBlock;

				if (current.currentValue > 0)
				{
					current.currentValue--;

					return true;
				}
				else if (current.currentValue == 0)
				{
					current.currentValue = BigNumberDSMath.MAX_ALLOWED_VALUE;
				}
			}

			return false;
		}

		public static bool GetHelpFromTitleBlock(ref big input, int countOfAttachingBlocks)
		{
			big current = input;

			while (current != null && current.currentValue == 0)
			{
				current.currentValue = BigNumberDSMath.MAX_ALLOWED_VALUE;

				current = current.previousBlock;
			}

			if (current != null && current.currentValue > 0)
			{
				current.currentValue--;

				for (int i = 0; i < countOfAttachingBlocks; i++)
				{
					current = input;

					input = new BigNumberDS(BigNumberDSMath.MAX_ALLOWED_VALUE, false, current.isPositive);

					input.previousBlock = current;
				}

				return true;
			}
			else
			{
				throw new ArgumentNullException("Can't get help from title block.");
			}
		}

		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from low order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static IEnumerable<bool> GetNextBit(big num)
		{
			big tmp = num;

			while (tmp != null)
			{
				foreach (var item in GetNextBit(tmp.currentValue).Reverse())
				{
					yield return item;
				}

				tmp = tmp.previousBlock;
			}
		}
		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from top order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		private static IEnumerable<bool> GetNextBit(ulong num)
		{
			ulong div = NextPowerOfTwo(num);
			bool bit;

			num++; // quick fix

			while ((div > 0) || (num > 1))
			{
				if (num > div)
				{
					num -= div;
					bit = true;
				}
				else
				{
					bit = false;
				}

				div /= 2;

				yield return bit;
			}
		}

		public static bool IsPowerOfTwo(ulong num)
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
		public static ulong NextPowerOfTwo(ulong v)
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

		internal static big AddNewPreviousBlock(big input, uint value, bool isBigPart, bool isPositive)
		{
			big current = input;

			big addingBlock = new BigNumberDS(value, isBigPart, isPositive);

			if (current == null)
			{
				return addingBlock;
			}

			while (current.previousBlock != null)
			{
				current.isPositive = isPositive;
				current = current.previousBlock;
			}

			current.previousBlock = addingBlock;

			return input;
		}

		internal static int DeepCount(big input)
		{
			int count = 1;

			big current = input;

			do
			{
				count++;

				current = current.previousBlock;
			} while (current.previousBlock != null);

			return count;
		}

		internal static big DivideService(big firstMem, big secondMem, big accuracy)
		{
			if (firstMem == null || secondMem == null)
			{
				throw new ArgumentNullException("Can't divide numbers");
			}
			else if (!firstMem.isPositive || !secondMem.isPositive)
			{
				throw new ArgumentException();
			}
			else if (firstMem == 0)
			{
				throw new ArgumentException("Mathematic is broken.(");
			}
			else if (firstMem == secondMem)
			{
				return BigNumberDS.Create("1");
			}
			else if (secondMem == 0)
			{
				return BigNumberDS.Create("0");
			}

			big currentIntResult, currentLeftOvResult, workingCopy;

			if (firstMem > secondMem)
			{
				currentIntResult = BigNumberDSHelper.IntegerDivide(firstMem, secondMem);
				currentLeftOvResult = BigNumberDSHelper.IntegerDivideLeftover(firstMem, secondMem);

				if (accuracy < BigNumberDS.Create("1") || currentLeftOvResult == 0)
				{
					workingCopy = currentIntResult;
				}
				else
				{
					workingCopy = currentIntResult + DivideService(currentLeftOvResult, secondMem, accuracy);
				}
			}
			else
			{
				if (accuracy < BigNumberDS.Create("1"))
				{
					workingCopy = BigNumberDS.Create("0");
				}
				//else if (BigNumberDSHelper.IntegerDivideLeftover(secondMem, firstMem) == BigNumberDS.Create("0"))
				//{
				//    workingCopy = BigNumberDSHelper.IntegerDivide(secondMem, firstMem);

				//    big divider = BigNumberDS.Create("1");
				//    big multiplier = BigNumberDS.Create("1");

				//    for (big i = BigNumberDS.Create("0"); i < accuracy; i++)
				//    {
				//        divider = divider * BigNumberDS.Create("10");
				//        multiplier = multiplier * BigNumberDS.Create("0,1");
				//    }

				//    workingCopy = divider / workingCopy * multiplier;
				//}
				else
				{
					workingCopy = DivideService(secondMem, firstMem, accuracy - 1);

					big divider = BigNumberDS.Create("1");
					big multiplier = BigNumberDS.Create("1");

					for (big i = BigNumberDS.Create("0"); i < accuracy; i++)
					{
						divider = divider * BigNumberDS.Create("10");
						multiplier = multiplier * BigNumberDS.Create("0,1");
					}

					workingCopy = DivideService(divider, workingCopy, BigNumberDS.Create("0")) * multiplier;
				}
			}

			return workingCopy;
		}

		internal static big GetFractionPart(big input)
		{
			big current = input;
			big output = new BigNumberDS();

			while (!current.isBigPart)
			{
				BigNumberDSHelper.AddNewPreviousBlock(output, current.currentValue, current.isBigPart, current.isPositive);
				current = current.previousBlock;
			}

			return output;
		}

		internal static int GetFractionPartBlocksCount(big input)
		{
			int result = 0;
			big current = input;

			while (current != null && !current.isBigPart)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		internal static big GetIntegerPart(big input)
		{
			big current = input;
			big output = new BigNumberDS();

			while (!current.isBigPart)
			{
				current = current.previousBlock;
			}

			while (current.previousBlock != null)
			{
				BigNumberDSHelper.AddNewPreviousBlock(output, current.currentValue, current.isBigPart, current.isPositive);

				current = current.previousBlock;
			}

			BigNumberDSHelper.AddNewPreviousBlock(output, current.currentValue, current.isBigPart, current.isPositive);

			return output;
		}

		internal static int GetIntegerPartBlocksCount(big input)
		{
			int result = 0;
			big current = input;

			while (current != null && !current.isBigPart)
			{
				current = current.previousBlock;
			}

			while (current != null)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		/// <summary>
		/// Define, how many zeros must add to block, formed from input number
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		internal static int GetNumberOfZeroesPrefix(uint input)
		{
			if (input == 0)
			{
				return 8;
			}

			int count = 0;

			while (input > 0)
			{
				input /= 10;
				count++;
			}

			count = 9 - count;

			return count;
		}

		internal static big GetWithoutDot(big input)
		{
			big output = (BigNumberDS)input.Clone();
			big current = output;
			while (current != null)
			{
				current.isBigPart = true;
				current = current.previousBlock;
			}
			return output;
		}

		internal static bool HasIntegerPart(big output)
		{
			big tmp = output;

			while (tmp != null)
			{
				if (tmp.isBigPart)
				{
					return true;
				}
			}

			return false;
		}

		internal static int GetBlocksCount(big input)
		{
			int result = 0;
			big current = input;

			while (current != null)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		/// <summary>
		/// Make byte array from number.
		/// F.e., 12465 will be {1,2,4,6,5}.
		/// </summary>
		/// <param name="number"></param>
		/// <param name="isIgnoreLastNulls"></param>
		/// <returns></returns>
		internal static byte[] IntArrayParse(uint number, bool isIgnoreLastNulls = false)
		{
			int size = 1;

			while (number >= Math.Pow(10, size))
			{
				size++;
			}

			if (isIgnoreLastNulls)
			{
				string str = number.ToString();
				int j = str.Length - 1;
				while (str[j] == '0')
				{
					j--;
					continue;
				}
				size = j;
			}

			byte[] output = new byte[size];
			const int decade = 10;
			int i = 0;

			while (number >= 1)
			{
				if (i > output.Length - 1)
				{
					break;
				}
				output[i] = (byte)(number % decade);
				i++;
				number /= decade;
			}

			return output;
		}

		internal static big IntegerDivide(big firstMem, big secondMem)
		{
			big result = new BigNumberDS();
			big iterator = (BigNumberDS)secondMem.Clone();

			while (iterator <= firstMem)
			{
				iterator += secondMem;

				result++;
			}

			return result;
		}

		internal static big IntegerDivideLeftover(big firstMem, big secondMem)
		{
			big result;
			big iterator = (BigNumberDS)secondMem.Clone();

			while (iterator + secondMem <= firstMem)
			{
				iterator += secondMem;
			}

			result = firstMem - iterator;

			return result;
		}

		internal static int[] MakeComparisionMap(big rhs, big lhs)
		{
			int bigPartBlocksCount = BigNumberDSHelper.GetIntegerPartBlocksCount(rhs) > BigNumberDSHelper.GetIntegerPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetIntegerPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetIntegerPartBlocksCount(lhs);

			int smallPartBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(rhs) > BigNumberDSHelper.GetFractionPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetFractionPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetFractionPartBlocksCount(lhs);

			int[] result = new int[bigPartBlocksCount + smallPartBlocksCount];

			big currentFirst = rhs,
			    currentSecond = lhs;

			for (int i = result.Length - 1; i >= 0; i--)
			{
				int compare = BigNumberDSHelper.GetFractionPartBlocksCount(currentFirst).CompareTo(BigNumberDSHelper.GetFractionPartBlocksCount(currentSecond));

				if (compare > 0)
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(currentFirst, null);
					currentFirst = currentFirst.previousBlock;
				}
				else if (compare < 0)
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(null, currentSecond);
					currentSecond = currentSecond.previousBlock;
				}
				else
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(currentFirst, currentSecond);
					currentFirst = currentFirst.previousBlock;
					currentSecond = currentSecond.previousBlock;
				}
			}

			return result;
		}

		internal static int MakeComparisionUnit(big firstMem, big secondMem)
		{
			// CompareTo ?

			if (firstMem == null && secondMem == null)
			{
				return 0;
			}
			else if (firstMem == null)
			{
				return -1;
			}
			else if (secondMem == null)
			{
				return 1;
			}
			else if (firstMem.currentValue > secondMem.currentValue)
			{
				return 1;
			}
			else if (firstMem.currentValue < secondMem.currentValue)
			{
				return -1;
			}

			return 0;
		}

		internal static bool[] MakeMap(big input)
		{
			bool[] result = new bool[BigNumberDSHelper.DeepCount(input) + 1];

			big current = input;

			bool smallToBigCross = false;

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = true;

				if (!current.isBigPart)
				{
					smallToBigCross = true;
				}

				if (result.Length != i + 1)
				{
					current = current.previousBlock;
				}

				if (current.isBigPart && smallToBigCross)
				{
					smallToBigCross = false;
					i++;
					result[i] = false;
				}
			}

			return result;
		}

		internal static StringBuilder MakeTextString(big input, StringBuilder result, bool isEdgeBlock)
		{
			if (input == null && result == null)
			{
				throw new ArgumentNullException("Can't make text string");
			}
			else if (result == null)
			{
				result = new StringBuilder();
			}

			int postfixZeroesIterator = 0;
			int iterator = 0;

			if (input != null && !input.isBigPart)
			{
				if (isEdgeBlock)
				{
					iterator = BigNumberDSHelper.GetNumberOfZeroesPrefix(input.currentValue);

					while (input.currentValue % 10 == 0)
					{
						input.currentValue = input.currentValue / 10;
						postfixZeroesIterator++;
					}

					isEdgeBlock = false;
				}
				else
				{
					iterator = BigNumberDSHelper.GetNumberOfZeroesPrefix(input.currentValue);
				}

				result.Insert(0, input.currentValue);

				for (int i = 0; i < iterator; i++)
				{
					result.Insert(0, "0");
				}

				input = input.previousBlock;

				if (input == null)
				{
					result.Insert(0, "0,");
				}
				else if (input.isBigPart)
				{
					result.Insert(0, ",");
				}

				result = BigNumberDSHelper.MakeTextString(input, result, isEdgeBlock);
			}
			else if (input != null && input.previousBlock != null)
			{
				result.Insert(0, input.currentValue);

				iterator = BigNumberDSHelper.GetNumberOfZeroesPrefix(input.currentValue);

				for (int i = 0; i < iterator; i++)
				{
					result.Insert(0, "0");
				}

				input = input.previousBlock;

				result = BigNumberDSHelper.MakeTextString(input, result, false);
			}
			else if (input != null)
			{
				result.Insert(0, input.currentValue);

				input = input.previousBlock;

				result = BigNumberDSHelper.MakeTextString(input, result, false);
			}

			return result;
		}

		internal static big MoveBy(big tmp, int count)
		{
			if (count == 0)
			{
				return tmp;
			}

			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					tmp *= 10;
				}
			}
			else
			{
				for (int i = 0; i < -count; i++)
				{
					tmp /= 10;
				}
			}

			return tmp;
		}

		/// <summary>
		/// If input has zero blocks in end of a fraction part or in start of integer part, this func will remove it.
		/// F.e., 0000000001230,000123000000 will be 00000123,000123000.
		/// </summary>
		/// <param name="input"></param>
		internal static void TrimStructure(ref big input)
		{
			big current = input;
			bool isEdgeBlock = true;

			big currentEdge;

			while (current != null && !current.isBigPart)
			{
				isEdgeBlock = isEdgeBlock && current.currentValue == 0;

				if (isEdgeBlock)
				{
					input = current.previousBlock;
				}

				current = current.previousBlock;
			}

			while (current != null && current.previousBlock != null)
			{
				if (current.previousBlock.currentValue == 0)
				{
					currentEdge = current;

					while (current.previousBlock != null)
					{
						if (current.previousBlock.currentValue != 0)
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
	}
}