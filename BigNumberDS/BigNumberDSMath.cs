﻿using System;

namespace Algorithms.BigNumber
{
	internal static class BigNumberDSMath
	{
		/// <summary>
		/// Now we works with int.
		/// Int can be from minus to plus 2 000 000 000.
		/// So, if we wanna save full stack of numbers, we have to use range from minus to plus 999 999 999.
		/// </summary>
		internal const uint MAX_ALLOWED_VALUE = uint.MaxValue - 1;

		internal static BigNumberDS Add(BigNumberDS firstMem, int secondMem)
			=> firstMem + BigNumberDS.Create(secondMem.ToString());

		internal static BigNumberDS Add(BigNumberDS firstMem, BigNumberDS secondMem)
		{
			BigNumberDS currentFirst;
			BigNumberDS currentSecond;

			if (firstMem.isPositive ^ secondMem.isPositive)
			{
				if (firstMem.Abs() == secondMem.Abs())
				{
					return BigNumberDS.Create("0");
				}
				else if (!firstMem.isPositive)
				{
					if (firstMem.Abs() > secondMem)
					{
						currentFirst = ((BigNumberDS)firstMem.Clone()).Invert();
						currentSecond = ((BigNumberDS)secondMem.Clone()).Invert();

						return Add(currentFirst, currentSecond, null, false).Invert();
					}
					else
					{
						currentFirst = ((BigNumberDS)secondMem.Clone());
						currentSecond = ((BigNumberDS)firstMem.Clone());

						return Add(currentFirst, currentSecond, null, false);
					}
				}
				else
				{
					if (secondMem.Abs() > firstMem)
					{
						currentFirst = ((BigNumberDS)secondMem.Clone()).Invert();
						currentSecond = ((BigNumberDS)firstMem.Clone()).Invert();

						return Add(currentFirst, currentSecond, null, false).Invert();
					}
					else
					{
						currentFirst = ((BigNumberDS)firstMem.Clone());
						currentSecond = ((BigNumberDS)secondMem.Clone());

						return Add(currentFirst, currentSecond, null, false);
					}
				}
			}
			else if (!firstMem.isPositive)
			{
				if (firstMem.Abs() > secondMem.Abs())
				{
					currentFirst = ((BigNumberDS)firstMem.Clone()).Invert();
					currentSecond = ((BigNumberDS)secondMem.Clone()).Invert();
				}
				else
				{
					currentFirst = ((BigNumberDS)secondMem.Clone()).Invert();
					currentSecond = ((BigNumberDS)firstMem.Clone()).Invert();
				}

				return Add(currentFirst, currentSecond, null, false).Invert();
			}
			else
			{
				if (firstMem.Abs() > secondMem.Abs())
				{
					currentFirst = ((BigNumberDS)firstMem.Clone());
					currentSecond = ((BigNumberDS)secondMem.Clone());
				}
				else
				{
					currentFirst = ((BigNumberDS)secondMem.Clone());
					currentSecond = ((BigNumberDS)firstMem.Clone());
				}

				return Add(currentFirst, currentSecond, null, false);
			}
		}

		internal static BigNumberDS Exponentiation(BigNumberDS lhs, BigNumberDS rhs)
		{
			if ((lhs == 0) && (rhs <= 0))
			{
				throw new ArgumentException($"Can't up zero to the power {rhs}: power has to be less zero.");
			}

			return null;
		}

		internal static BigNumberDS Add(int firstMem, BigNumberDS secondMem)
		    => BigNumberDSMath.Add(secondMem, firstMem);

		internal static BigNumberDS Add(BigNumberDS firstMem, BigNumberDS secondMem, BigNumberDS result, bool addOne)
		{
			if (firstMem == null && secondMem == null && result == null)
			{
				throw new NullReferenceException();
			}
			else if (firstMem != null &&
			    secondMem != null &&
			    (!firstMem.isPositive ||
			    firstMem < secondMem))
			{
				throw new ArgumentException();
			}

			BigNumberDS currentFirst;
			BigNumberDS currentSecond;

			uint currentSum = 0;
			bool isBigPart;

			currentFirst = firstMem == null ?
			    null :
			    (BigNumberDS)firstMem.Clone();
			currentSecond = secondMem == null ?
			    null :
			    (BigNumberDS)secondMem.Clone();

			int firstMemSmallPartBlocksCount = currentFirst == null ?
			    0 :
			    BigNumberDSHelper.GetFractionPartBlocksCount(currentFirst),
				secondMemSmallPartBlocksCount = currentSecond == null ?
				0 :
				BigNumberDSHelper.GetFractionPartBlocksCount(currentSecond);

			if (firstMem != null && secondMem != null &&
			    (!currentFirst.isBigPart || !currentSecond.isBigPart) &&
			    (firstMemSmallPartBlocksCount != secondMemSmallPartBlocksCount))
			{
				#region Adding numbers with different count of blocks in small part

				if (firstMemSmallPartBlocksCount > secondMemSmallPartBlocksCount)
				{
					currentSum = currentFirst.currentValue;

					currentFirst = currentFirst.previousBlock;
				}
				else if (currentSecond.isPositive)
				{
					currentSum = currentSecond.currentValue;

					currentSecond = currentSecond.previousBlock;
				}
				else
				{
					BigNumberDSHelper.GetHelpFromTitleBlock(ref currentFirst, secondMemSmallPartBlocksCount - firstMemSmallPartBlocksCount - 1);

					currentSum = (uint) Math.Abs(BigNumberDSMath.MAX_ALLOWED_VALUE + 1 - currentSecond.currentValue);

					currentSecond = currentSecond.previousBlock;
				}

				if (addOne)
				{
					currentSum++;

					addOne = false;
				}

				if (currentSum > BigNumberDSMath.MAX_ALLOWED_VALUE)
				{
					addOne = true;

					currentSum = currentSum - BigNumberDSMath.MAX_ALLOWED_VALUE - 1;
				}

				if (result == null)
				{
					result = new BigNumberDS(currentSum, false, true);
				}
				else
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, false, true);
				}

				result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);

				#endregion Adding numbers with different count of blocks in small part
			}
			else if (currentSecond != null)
			{
				#region Adding numbers middle blocks

				isBigPart = currentFirst.isBigPart;

				if (!currentSecond.isPositive)
				{
					if (currentFirst.currentValue >= currentSecond.currentValue)
					{
						currentSum = currentFirst.currentValue - currentSecond.currentValue;

						currentFirst = currentFirst.previousBlock;
						currentSecond = currentSecond.previousBlock;
					}
					else
					{
						BigNumberDSHelper.GetHelpFromPreviousBlocks(currentFirst);

						currentSum = currentFirst.currentValue + BigNumberDSMath.MAX_ALLOWED_VALUE + 1 - currentSecond.currentValue;

						currentFirst = currentFirst.previousBlock;
						currentSecond = currentSecond.previousBlock;
					}
				}
				else
				{
					currentSum = currentFirst.currentValue + currentSecond.currentValue;

					currentFirst = currentFirst.previousBlock;
					currentSecond = currentSecond.previousBlock;
				}

				if (addOne)
				{
					currentSum++;

					addOne = false;
				}

				if (currentSum > BigNumberDSMath.MAX_ALLOWED_VALUE)
				{
					addOne = true;

					currentSum = currentSum - BigNumberDSMath.MAX_ALLOWED_VALUE - 1;
				}

				if (result == null)
				{
					result = new BigNumberDS(currentSum, isBigPart, true);
				}
				else
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, isBigPart, true);
				}

				result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);

				#endregion Adding numbers middle blocks
			}
			else if (currentFirst != null)
			{
				#region Adding numbers final blocks

				isBigPart = currentFirst.isBigPart;

				currentSum = currentFirst.currentValue;

				if (addOne)
				{
					currentSum++;

					addOne = false;
				}

				if (currentSum > BigNumberDSMath.MAX_ALLOWED_VALUE)
				{
					addOne = true;

					currentSum = currentSum - BigNumberDSMath.MAX_ALLOWED_VALUE - 1;
				}

				if (currentFirst.previousBlock != null)
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, isBigPart, true);

					currentFirst = currentFirst.previousBlock;

					result = BigNumberDSMath.Add(currentFirst, null, result, addOne);
				}
				else
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, isBigPart, true);

					result = BigNumberDSMath.Add(null, null, result, addOne);
				}

				#endregion Adding numbers final blocks
			}
			else if (addOne)
			{
				result = BigNumberDSHelper.AddNewPreviousBlock(result, 1, true, true);

				result = BigNumberDSMath.Add(null, null, result, false);
			}

			return result;
		}

		internal static BigNumberDS Or(BigNumberDS lhs, BigNumberDS rhs)
		{
			return null;
		}

		internal static BigNumberDS And(BigNumberDS lhs, BigNumberDS rhs)
		{
			return null;
		}

		internal static BigNumberDS Divide(BigNumberDS lhs, BigNumberDS rhs, BigNumberDS accuracy = null)
		{
			if (accuracy == null)
			{
				accuracy = BigNumberDS.DivisionAccuracy;
			}

			return BigNumberDSHelper.DivideService(lhs, rhs, accuracy);
		}

		internal static BigNumberDS Divide(BigNumberDS lhs, int rhs)
		{
			return BigNumberDSMath.Divide(lhs, BigNumberDS.Create(rhs.ToString()));
		}

		internal static BigNumberDS Multiple(BigNumberDS lhs, BigNumberDS rhs)
		{
			#region checks

			if ((rhs == 0) || (lhs == 0))
			{
				return BigNumberDS.Create("0");
			}

			if (rhs == -1)
			{
				return -((BigNumberDS)lhs.Clone());
			}

			if (rhs == 1)
			{
				return (BigNumberDS)lhs.Clone();
			}

			if (lhs == -1)
			{
				return -((BigNumberDS)rhs.Clone());
			}

			if (lhs == 1)
			{
				return (BigNumberDS)rhs.Clone();
			}

			// simple optimization. TODO check, is it an optimization
			if ((rhs.previousBlock == null) && // if rhs is less than MAX_ALLOWED_VALUE
			    (rhs.currentValue < short.MaxValue) &&
			    (BigNumberDSHelper.GetFractionPartBlocksCount(rhs) == 0)) // and has no fraction
			{
				if (rhs.isPositive)
				{
					return BigNumberDSMath.Multiple(lhs, (byte)rhs.currentValue);
				}
				else
				{
					return BigNumberDSMath.Multiple(lhs, ((byte)(-rhs.currentValue)));
				}
			}

			if ((lhs.previousBlock == null) && // if lhs is less than MAX_ALLOWED_VALUE
			    (lhs.currentValue < short.MaxValue) &&
			    (BigNumberDSHelper.GetFractionPartBlocksCount(lhs) == 0)) // and has no fraction
			{
				return BigNumberDSMath.Multiple(rhs, (byte)lhs.currentValue);
			}

			#endregion checks

			// column addition

			BigNumberDS lhsrough = BigNumberDSHelper.GetWithoutDot(lhs);
			BigNumberDS rhsrough = BigNumberDSHelper.GetWithoutDot(rhs);
			BigNumberDS output = new BigNumberDS();

			int k = 0;
			while (rhsrough != null)
			{
				byte[] a = BigNumberDSHelper.IntArrayParse(rhsrough.currentValue);

				int firstZeros = BigNumberDSHelper.GetNumberOfZeroesPrefix(rhsrough.currentValue);

				for (int i = 0; i < a.Length; i++)
				{
					BigNumberDS tmp = lhsrough * a[i];

					tmp = BigNumberDSHelper.MoveBy(tmp, k);

					output += tmp;
					k++;
				}

				if (firstZeros != 0)
				{
					k += firstZeros;
				}

				rhsrough = rhsrough.previousBlock;
			}

			if (!rhs.isPositive)
			{
				output = output.Invert();
			}

			int fractionRhsBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(rhs);
			int fractionLhsBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(lhs);

			if ((fractionRhsBlocksCount != 0) || (fractionLhsBlocksCount != 0))
			{
				// getting copy of output
				BigNumberDS biglhs = output;
				for (int i = 0; i < fractionRhsBlocksCount + fractionLhsBlocksCount; i++)
				{
					biglhs.isBigPart = false;
					biglhs = biglhs.previousBlock;
				}
			}

			if (!output.isBigPart)
			{
				BigNumberDSHelper.AddNewPreviousBlock(output, 0, true, output.isPositive);
			}

			BigNumberDSHelper.TrimStructure(ref output);

			return output;
		}

		internal static BigNumberDS Multiple(BigNumberDS lhs, byte rhs)
		{
			#region checks

			if ((rhs == 0) || ((lhs.currentValue == 0) && (lhs.previousBlock == null)))
			{
				return BigNumberDS.Create("0");
			}
			if (rhs == -1)
			{
				return -((BigNumberDS)lhs.Clone());
			}

			if (rhs == 1)
			{
				return (BigNumberDS)lhs.Clone();
			}

			if (lhs.currentValue == -1)
			{
				return new BigNumberDS(rhs, true, rhs > 0);
			}

			if (lhs.currentValue == 1)
			{
				return new BigNumberDS(rhs, true, rhs > 0);
			}

			#endregion checks

			BigNumberDSHelper.TrimStructure(ref lhs);

			int smallBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(lhs);

			BigNumberDS output = new BigNumberDS();

			BigNumberDS biglhs = BigNumberDSHelper.GetWithoutDot(lhs);

			for (int i = 0; i < Math.Abs(rhs); i++)
			{
				output += biglhs;
			}

			bool isRhsNegative = rhs < 0;
			if (isRhsNegative)
			{
				output = output.Invert();
			}

			// move dot to the right position
			if (smallBlocksCount != 0)
			{
				// getting copy of output
				biglhs = output;
				for (int i = 0; i < smallBlocksCount; i++)
				{
					biglhs.isBigPart = false;
					biglhs = biglhs.previousBlock;
				}
			}

			return output;
		}

		internal static BigNumberDS Subtract(BigNumberDS lhs, BigNumberDS rhs)
		{
			return BigNumberDSMath.Add(lhs, -rhs);
		}

		internal static BigNumberDS Subtract(BigNumberDS lhs, int rhs)
		{
			return BigNumberDSMath.Add(lhs, -rhs);
		}
	}
}