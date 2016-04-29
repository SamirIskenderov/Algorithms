using System;
using System.Text;

namespace Algorithms.BigNumber
{
	public static class BigNumberDSHelper
	{
		public static bool GetHelpFromPreviousBlocks(BigNumberDS input)
		{
			BigNumberDS current = input;

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

		public static bool GetHelpFromTitleBlock(ref BigNumberDS input, int countOfAttachingBlocks)
		{
			BigNumberDS current = input;

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
				throw new NullReferenceException();
			}
		}

		internal static BigNumberDS AddNewPreviousBlock(BigNumberDS input, uint value, bool isBigPart, bool isPositive)
		{
			BigNumberDS current = input;

			BigNumberDS addingBlock = new BigNumberDS(value, isBigPart, isPositive);

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

		internal static int DeepCount(BigNumberDS input)
		{
			int count = 1;

			BigNumberDS current = input;

			do
			{
				count++;

				current = current.previousBlock;
			} while (current.previousBlock != null);

			return count;
		}

		internal static BigNumberDS GetFractionPart(BigNumberDS input)
		{
			BigNumberDS current = input;
			BigNumberDS output = new BigNumberDS();

			while (!current.isBigPart)
			{
				BigNumberDSHelper.AddNewPreviousBlock(output, current.currentValue, current.isBigPart, current.isPositive);
				current = current.previousBlock;
			}

			return output;
		}

		internal static int GetFractionPartBlocksCount(BigNumberDS input)
		{
			int result = 0;
			BigNumberDS current = input;

			while (current != null && !current.isBigPart)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		internal static BigNumberDS GetIntegerPart(BigNumberDS input)
		{
			BigNumberDS current = input;
			BigNumberDS output = new BigNumberDS();

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

		internal static int GetIntegerPartBlocksCount(BigNumberDS input)
		{
			int result = 0;
			BigNumberDS current = input;

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

		internal static BigNumberDS GetWithoutDot(BigNumberDS input)
		{
			BigNumberDS output = (BigNumberDS)input.Clone();
			BigNumberDS current = output;
			while (current != null)
			{
				current.isBigPart = true;
				current = current.previousBlock;
			}
			return output;
		}

		internal static bool HasIntegerPart(BigNumberDS output)
		{
			BigNumberDS tmp = output;

			while (tmp != null)
			{
				if (tmp.isBigPart)
				{
					return true;
				}
			}

			return false;
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

		internal static BigNumberDS IntegerDivide(BigNumberDS firstMem, BigNumberDS secondMem)
		{
			BigNumberDS result = new BigNumberDS();
			BigNumberDS iterator = (BigNumberDS)secondMem.Clone();

			while (iterator <= firstMem)
			{
				iterator += secondMem;

				result++;
			}

			return result;
		}

		internal static BigNumberDS DivideService(BigNumberDS firstMem, BigNumberDS secondMem, BigNumberDS accuracy)
		{
			if (firstMem == null || secondMem == null)
			{
				throw new ArgumentNullException();
			}
			else if (!firstMem.isPositive || !secondMem.isPositive)
			{
				throw new ArgumentException();
			}
			else if (firstMem == 0)
			{
				throw new Exception("Mathematic is broken.(");
			}
			else if (firstMem == secondMem)
			{
				return BigNumberDS.Create("1");
			}
			else if (secondMem == 0)
			{
				return BigNumberDS.Create("0");
			}

			BigNumberDS currentIntResult, currentLeftOvResult, workingCopy;

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

				//    BigNumberDS divider = BigNumberDS.Create("1");
				//    BigNumberDS multiplier = BigNumberDS.Create("1");

				//    for (BigNumberDS i = BigNumberDS.Create("0"); i < accuracy; i++)
				//    {
				//        divider = divider * BigNumberDS.Create("10");
				//        multiplier = multiplier * BigNumberDS.Create("0,1");
				//    }

				//    workingCopy = divider / workingCopy * multiplier;
				//}
				else
				{
					workingCopy = DivideService(secondMem, firstMem, accuracy - 1);

					BigNumberDS divider = BigNumberDS.Create("1");
					BigNumberDS multiplier = BigNumberDS.Create("1");

					for (BigNumberDS i = BigNumberDS.Create("0"); i < accuracy; i++)
					{
						divider = divider * BigNumberDS.Create("10");
						multiplier = multiplier * BigNumberDS.Create("0,1");
					}

					workingCopy = DivideService(divider, workingCopy, BigNumberDS.Create("0")) * multiplier;
				}
			}

			return workingCopy;
		}

		internal static BigNumberDS IntegerDivideLeftover(BigNumberDS firstMem, BigNumberDS secondMem)
		{
			BigNumberDS result;
			BigNumberDS iterator = (BigNumberDS)secondMem.Clone();

			while (iterator + secondMem <= firstMem)
			{
				iterator += secondMem;
			}

			result = firstMem - iterator;

			return result;
		}

		internal static int[] MakeComparisionMap(BigNumberDS rhs, BigNumberDS lhs)
		{
			int bigPartBlocksCount = BigNumberDSHelper.GetIntegerPartBlocksCount(rhs) > BigNumberDSHelper.GetIntegerPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetIntegerPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetIntegerPartBlocksCount(lhs);

			int smallPartBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(rhs) > BigNumberDSHelper.GetFractionPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetFractionPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetFractionPartBlocksCount(lhs);

			int[] result = new int[bigPartBlocksCount + smallPartBlocksCount];

			BigNumberDS currentFirst = rhs,
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

		internal static int MakeComparisionUnit(BigNumberDS firstMem, BigNumberDS secondMem)
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

		internal static bool[] MakeMap(BigNumberDS input)
		{
			bool[] result = new bool[BigNumberDSHelper.DeepCount(input) + 1];

			BigNumberDS current = input;

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

		internal static StringBuilder MakeTextString(BigNumberDS input, StringBuilder result, bool isEdgeBlock)
		{
			if (input == null && result == null)
			{
				throw new NullReferenceException();
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

		internal static BigNumberDS MoveBy(BigNumberDS tmp, int count)
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
		internal static void TrimStructure(ref BigNumberDS input)
		{
			BigNumberDS current = input;
			bool isEdgeBlock = true;

			BigNumberDS currentEdge;

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