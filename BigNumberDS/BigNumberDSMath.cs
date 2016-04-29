using System;

namespace Algorithms.BigNumber
{
	internal static class BigNumberDSMath
	{
		/// <summary>
		/// Now we works with int.
		/// Int can be from minus to plus 2 000 000 000.
		/// So, if we wanna save full stack of numbers, we have to use range from minus to plus 999 999 999.
		/// </summary>
		internal const int MAX_ALLOWED_VALUE = 999999999;

		internal static BigNumberDS Add(BigNumberDS firstMem, int secondMem)
		{
			if (secondMem == 0)
			{
				return firstMem;
			}

			BigNumberDS big = BigNumberDSHelper.GetIntegerPart(firstMem);

			if (firstMem.currentValue + secondMem > MAX_ALLOWED_VALUE) // TODO overflow fix
			{
				int b = secondMem - MAX_ALLOWED_VALUE - 1;
				firstMem.previousBlock.currentValue += 1; // TODO null ref fix

				if (b > MAX_ALLOWED_VALUE)
				{
					b = b - MAX_ALLOWED_VALUE - 1;
					firstMem.previousBlock.currentValue += 1;
				}

				firstMem.currentValue += b;
			}
			firstMem.currentValue += secondMem;

			return firstMem;
		}

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

			int currentSum = 0;
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
					BigNumberDSHelper.GetHelpFromTitleBlock(ref currentFirst, --secondMemSmallPartBlocksCount);

					currentSum = Math.Abs(BigNumberDSMath.MAX_ALLOWED_VALUE + 1 - currentSecond.currentValue);

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

		internal static BigNumberDS Divide(BigNumberDS lhs, BigNumberDS rhs)
		{
			return null;
		}

		internal static BigNumberDS Divide(BigNumberDS lhs, int rhs)
		{
			return null;
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
					return BigNumberDSMath.Multiple(lhs, (short)rhs.currentValue);
				}
				else
				{
					return BigNumberDSMath.Multiple(lhs, ((short)(-rhs.currentValue)));
				}
			}

			if ((lhs.previousBlock == null) && // if lhs is less than MAX_ALLOWED_VALUE
			    (lhs.currentValue < short.MaxValue) &&
			    (BigNumberDSHelper.GetFractionPartBlocksCount(lhs) == 0)) // and has no fraction
			{
				return BigNumberDSMath.Multiple(rhs, (short)lhs.currentValue);
			}

			#endregion checks

			// column addition

			BigNumberDS lhsrough = BigNumberDSHelper.GetWithoutDot(lhs);
			BigNumberDS rhsrough= BigNumberDSHelper.GetWithoutDot(rhs);
			BigNumberDS output = new BigNumberDS();

				int k = 0;
			while (rhsrough != null)
			{
				byte[] a = BigNumberDSHelper.IntArrayParse(rhsrough.currentValue);

				for (int i = 0; i < a.Length; i++)
				{
					BigNumberDS tmp = lhsrough * a[i];

					tmp = BigNumberDSHelper.MoveBy(tmp, k);

					output += tmp;
					k++;
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

			//BigNumberDS lhsrough = BigNumberDSHelper.GetWithoutDot(lhs);
			//BigNumberDS current = BigNumberDSHelper.GetWithoutDot(rhs);
			//BigNumberDS output = new BigNumberDS();

			//int k = 0;
			//bool start = false;
			//int count = 0;
			//while (current != null)
			//{
			//	byte[] a = BigNumberDSHelper.IntArrayParse(current.currentValue);
			//	for (int i = 0; i < a.Length; i++)
			//	{
			//		BigNumberDS tmp = new BigNumberDS();

			//		if (!start)
			//		{
			//			if (a[i] != 0)
			//			{
			//				start = true;
			//			}
			//			else
			//			{
			//				count++;
			//			}
			//		}

			//		if (start)
			//		{
			//			tmp = lhsrough * a[i];

			//			for (int j = 0; j < k; j++)
			//			{
			//				tmp *= 10;
			//			}

			//			output += tmp;
			//			k++;
			//		}
			//	}
			//	current = current.previousBlock;
			//}

			//if (!rhs.isPositive)
			//{
			//	output = output.Invert();
			//}

			//for (int i = 0; i < count - 1; i++)
			//{
			//	output *= 10;
			//}

			//int fractionRhsBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(rhs);
			//int fractionLhsBlocksCount = BigNumberDSHelper.GetFractionPartBlocksCount(lhs);
			//int trashBlocksCount = 0;
			//current = output;
			//while (current.currentValue == 0 && current.previousBlock != null)
			//{
			//	trashBlocksCount++;
			//	current = current.previousBlock;
			//}

			//if ((fractionRhsBlocksCount != 0) || (fractionLhsBlocksCount != 0))
			//{
			//	// getting copy of output
			//	BigNumberDS biglhs = output;
			//	for (int i = 0; i < fractionRhsBlocksCount + fractionLhsBlocksCount + trashBlocksCount; i++)
			//	{
			//		if (biglhs == null)
			//		{
			//			biglhs = BigNumberDSHelper.AddNewPreviousBlock(output, 0, true, true);
			//		}

			//			biglhs.isBigPart = false;
			//			biglhs = biglhs.previousBlock;
			//	}
			//}

			BigNumberDSHelper.TrimStructure(ref output);

			return output;
		}

		internal static BigNumberDS Multiple(BigNumberDS lhs, short rhs)
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