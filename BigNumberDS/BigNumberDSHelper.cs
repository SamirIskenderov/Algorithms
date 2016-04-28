using System;

namespace Algorithms.BigNumber
{
	internal static class BigNumberDSHelper
	{
		internal static int[] IntArrayParse(int number, bool isIgnoreLastNulls = false)
		{
			int size = 1;
			while (number > Math.Pow(10, size))
			{
				size++;
			}
			if (isIgnoreLastNulls)
			{
				string str = number.ToString();
				for (int j = str.Length - 1; j >= 0; j--)
				{
					while (str[j] == '0')
					{
						j--;
						continue;
					}
					size = j;
					break;
				}
			}
			int[] output = new int[size];
			int decade = 10;
			int i = 0;
			while (number > 1)
			{
				if (i > output.Length - 1)
				{
					break;
				}
				output[i] = number % decade;
				i++;
				number /= decade;
			}
			return output;
		}

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
			}

			return false;
		}

        public static bool GetHelpFromTitleBlock(BigNumberDS input)
        {
            if (input.currentValue > 0)
            {
                input.currentValue--;

                return true;
            }
            return false;
        }

        public static void TrimStructure(ref BigNumberDS input)
		{
			BigNumberDS current = input;
			bool isEdgeBlock = true;

			BigNumberDS currentEdge;

			while (!current.isBigPart)
			{
				isEdgeBlock = isEdgeBlock && current.currentValue == 0;

				if (isEdgeBlock)
				{
					input = current.previousBlock;
				}

				current = current.previousBlock;
			}

			isEdgeBlock = false;

			while (current.previousBlock != null)
			{
				if (current.previousBlock.currentValue == 0)
				{
					currentEdge = current;

					while (current.previousBlock != null)
					{
						if (current.previousBlock.previousBlock == null)
						{
							currentEdge.previousBlock = null;
							return;
						}
						else if (current.previousBlock.currentValue != 0)
						{
							break;
						}

						current = current.previousBlock;
					}
				}

				current = current.previousBlock;
			}
		}

		internal static BigNumberDS AddNewPreviousBlock(BigNumberDS input, int value, bool isBigPart, bool isPositive)
		{
			BigNumberDS current = input;

			BigNumberDS addingBlock = new BigNumberDS(value, isBigPart, isPositive);

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

		internal static BigNumberDS GetBigPart(BigNumberDS input)
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

		internal static int GetBigPartBlocksCount(BigNumberDS input)
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

		internal static int GetNumberOfZeroesPrefix(int input)
		{
			int result = 9 - input.ToString().Length;

			return result;
		}

		internal static BigNumberDS GetSmallPart(BigNumberDS input)
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

		internal static int GetSmallPartBlocksCount(BigNumberDS input)
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

		internal static int MakeComparisionUnit(BigNumberDS firstMem, BigNumberDS secondMem)
		{
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

				if (i + 1 != result.Length)
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

		internal static BigNumberDS WithoutDot(BigNumberDS input)
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

		internal static int[] MakeComparisionMap(BigNumberDS rhs, BigNumberDS lhs)
		{
			int bigPartBlocksCount = BigNumberDSHelper.GetBigPartBlocksCount(rhs) > BigNumberDSHelper.GetBigPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetBigPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetBigPartBlocksCount(lhs);

			int smallPartBlocksCount = BigNumberDSHelper.GetSmallPartBlocksCount(rhs) > BigNumberDSHelper.GetSmallPartBlocksCount(lhs) ?
			    BigNumberDSHelper.GetSmallPartBlocksCount(rhs) :
			    BigNumberDSHelper.GetSmallPartBlocksCount(lhs);

			int[] result = new int[bigPartBlocksCount + smallPartBlocksCount];

			BigNumberDS currentFirst = rhs,
			    currentSecond = lhs;

			for (int i = result.Length - 1; i >= 0; i--)
			{
				if (BigNumberDSHelper.GetSmallPartBlocksCount(currentFirst) > BigNumberDSHelper.GetSmallPartBlocksCount(currentSecond))
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(currentFirst, null);
				}
				else if (BigNumberDSHelper.GetSmallPartBlocksCount(currentFirst) < BigNumberDSHelper.GetSmallPartBlocksCount(currentSecond))
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(null, currentSecond);
				}
				else
				{
					result[i] = BigNumberDSHelper.MakeComparisionUnit(currentFirst, currentSecond);
				}

				if (BigNumberDSHelper.GetSmallPartBlocksCount(currentFirst) > BigNumberDSHelper.GetSmallPartBlocksCount(currentSecond))
				{
					currentFirst = currentFirst.previousBlock;
				}
				else if (BigNumberDSHelper.GetSmallPartBlocksCount(currentFirst) < BigNumberDSHelper.GetSmallPartBlocksCount(currentSecond))
				{
					currentSecond = currentSecond.previousBlock;
				}
				else
				{
					currentFirst = currentFirst.previousBlock;
					currentSecond = currentSecond.previousBlock;
				}
			}

			return result;
		}
	}
}