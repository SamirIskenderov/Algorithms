﻿using System;
using System.Text;

namespace Algorithms.BigNumber
{
	public static class BigNumberDSHelper
	{
		/// <summary>
		/// Make byte array from number.
		/// F.e., 12465 will be {1,2,4,6,5}.
		/// </summary>
		/// <param name="number"></param>
		/// <param name="isIgnoreLastNulls"></param>
		/// <returns></returns>
		internal static byte[] IntArrayParse(int number, bool isIgnoreLastNulls = false)
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
		internal static int GetNumberOfZeroesPrefix(int input)
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

                    iterator = iterator - postfixZeroesIterator;

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

        //internal static BigNumberDS Divide(BigNumberDS firstMem, BigNumberDS secondMem, BigNumberDS )
        //{

        //}

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

        internal static BigNumberDS IntegerDivideLeftover(BigNumberDS firstMem, BigNumberDS secondMem)
        {
            BigNumberDS result;
            BigNumberDS iterator = (BigNumberDS)secondMem.Clone();

            while (iterator < firstMem)
            {
                iterator += secondMem;
            }

            result = firstMem - iterator;

            return result;
        }
    }
}