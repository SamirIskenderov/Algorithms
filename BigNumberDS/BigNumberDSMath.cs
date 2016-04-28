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
			BigNumberDS currentFirst = firstMem;
			BigNumberDS currentSecond = secondMem;

			if (currentFirst.isPositive ^ currentSecond.isPositive)
			{
				if ((!currentFirst.isPositive && currentFirst.Abs() == currentSecond) ||
					(!currentSecond.isPositive && currentSecond.Abs() == currentFirst))
				{
					return new BigNumberDS("0");
				}
				else if ((!currentFirst.isPositive && currentFirst.Abs() > currentSecond) ||
					(!currentSecond.isPositive && currentSecond.Abs() > currentFirst))
				{
					currentFirst = currentFirst.Invert();
					currentSecond = currentSecond.Invert();

					return Add(currentFirst, currentSecond, null, false).Invert();
				}
			}
			else if (!currentFirst.isPositive)
			{
				currentFirst = currentFirst.Invert();
				currentSecond = currentSecond.Invert();

				return Add(currentFirst, currentSecond, null, false).Invert();
			}

			return Add(currentFirst, currentSecond, null, false);
		}

		internal static BigNumberDS Add(int firstMem, BigNumberDS secondMem)
		    => BigNumberDSMath.Add(secondMem, firstMem);

		internal static BigNumberDS Add(BigNumberDS firstMem, BigNumberDS secondMem, BigNumberDS result, bool addOne)
		{
			BigNumberDS currentFirst;
			BigNumberDS currentSecond;

			int currentSum = 0;
			bool positiveStatus = false;

			if (secondMem != null && (!firstMem.isBigPart || !secondMem.isBigPart))
			{
				if (BigNumberDSHelper.GetFractionPartBlocksCount(firstMem) != BigNumberDSHelper.GetFractionPartBlocksCount(secondMem))
				{
					if (BigNumberDSHelper.GetFractionPartBlocksCount(firstMem) > BigNumberDSHelper.GetFractionPartBlocksCount(secondMem))
					{
						currentFirst = firstMem;
						currentSecond = secondMem;
					}
					else
					{
						currentFirst = secondMem;
						currentSecond = firstMem;
					}

					positiveStatus = currentFirst.isPositive;

					if (result == null)
					{
						result = new BigNumberDS(currentFirst.currentValue, false, positiveStatus);

						currentFirst = currentFirst.previousBlock;

						if (currentFirst == null)
						{
							result = BigNumberDSMath.Add(currentSecond, currentFirst, result, false);
						}
						else
						{
							result = BigNumberDSMath.Add(currentFirst, currentSecond, result, false);
						}
					}
					else
					{
						result = BigNumberDSHelper.AddNewPreviousBlock(result, currentFirst.currentValue, false, positiveStatus);

						currentFirst = currentFirst.previousBlock;

						if (currentFirst == null)
						{
							result = BigNumberDSMath.Add(currentSecond, currentFirst, result, false);
						}
						else
						{
							result = BigNumberDSMath.Add(currentFirst, currentSecond, result, false);
						}
					}
				}
				else
				{
					currentFirst = firstMem;
					currentSecond = secondMem;

					if (result == null)
					{
						if (currentFirst.isPositive ^ currentSecond.isPositive)
						{
							if (!currentFirst.isPositive)
							{
								currentFirst = secondMem;
								currentSecond = firstMem;
							}

							positiveStatus = currentFirst.isPositive;

							if (currentFirst.currentValue < currentSecond.currentValue && BigNumberDSHelper.GetHelpFromPreviousBlocks(currentFirst))
							{
								currentSum = currentFirst.currentValue + MAX_ALLOWED_VALUE - 1 - currentSecond.currentValue;
							}
							else if (currentFirst.currentValue >= currentSecond.currentValue)
							{
								currentSum = currentFirst.currentValue - currentSecond.currentValue;
							}
							else
							{
								currentSum = Math.Abs(currentFirst.currentValue - currentSecond.currentValue);

								positiveStatus = false;
							}
						}
						else
						{
							positiveStatus = currentFirst.isPositive;

							currentSum = currentFirst.currentValue + currentSecond.currentValue;
						}

						if (addOne)
						{
							currentSum++;
							addOne = false;
						}

						if (currentSum > MAX_ALLOWED_VALUE)
						{
							currentSum = currentSum - MAX_ALLOWED_VALUE - 1;
							addOne = true;
						}

						result = new BigNumberDS(currentSum, false, positiveStatus);

						currentFirst = currentFirst.previousBlock;
						currentSecond = currentSecond.previousBlock;

						if (currentFirst == null)
						{
							result = BigNumberDSMath.Add(currentSecond, currentFirst, result, addOne);
						}
						else
						{
							result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);
						}
					}
					else
					{
						if (firstMem.isPositive ^ secondMem.isPositive)
						{
							if (!firstMem.isPositive)
							{
								currentFirst = secondMem;
								currentSecond = firstMem;
							}

							positiveStatus = firstMem.isPositive;

							if (currentFirst.currentValue < currentSecond.currentValue && BigNumberDSHelper.GetHelpFromPreviousBlocks(currentFirst))
							{
								currentSum = currentFirst.currentValue + MAX_ALLOWED_VALUE - 1 - currentSecond.currentValue;
							}
							else if (currentFirst.currentValue >= currentSecond.currentValue)
							{
								currentSum = currentFirst.currentValue - currentSecond.currentValue;
							}
							else
							{
								currentSum = Math.Abs(currentFirst.currentValue - currentSecond.currentValue);

								positiveStatus = false;
							}
						}
						else
						{
							positiveStatus = currentFirst.isPositive;

							currentSum = currentFirst.currentValue + currentSecond.currentValue;
						}

						if (addOne)
						{
							currentSum++;
							addOne = false;
						}

						if (currentSum > MAX_ALLOWED_VALUE)
						{
							currentSum = currentSum - MAX_ALLOWED_VALUE - 1;
							addOne = true;
						}

						result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, false, positiveStatus);

						currentFirst = currentFirst.previousBlock;
						currentSecond = currentSecond.previousBlock;

						if (currentFirst == null)
						{
							result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);
						}
						else
						{
							result = BigNumberDSMath.Add(currentSecond, currentFirst, result, addOne);
						}
					}
				}
			}
			else if (secondMem != null)
			{
				currentFirst = firstMem;
				currentSecond = secondMem;

				if (result == null)
				{
					if (currentFirst.isPositive ^ currentSecond.isPositive)
					{
						if (!currentFirst.isPositive)
						{
							currentFirst = secondMem;
							currentSecond = firstMem;
						}

						positiveStatus = currentFirst.isPositive;

						if (currentFirst.currentValue < currentSecond.currentValue && BigNumberDSHelper.GetHelpFromPreviousBlocks(currentFirst))
						{
							currentSum = currentFirst.currentValue + MAX_ALLOWED_VALUE - 1 - currentSecond.currentValue;
						}
						else if (currentFirst.currentValue >= currentSecond.currentValue)
						{
							currentSum = currentFirst.currentValue - currentSecond.currentValue;
						}
						else
						{
							currentSum = Math.Abs(currentFirst.currentValue - currentSecond.currentValue);

							positiveStatus = false;
						}
					}
					else
					{
						positiveStatus = currentFirst.isPositive;

						currentSum = currentFirst.currentValue + currentSecond.currentValue;
					}

					if (addOne)
					{
						currentSum++;
						addOne = false;
					}

					if (currentSum > MAX_ALLOWED_VALUE)
					{
						currentSum = currentSum - MAX_ALLOWED_VALUE - 1;
						addOne = true;
					}

					result = new BigNumberDS(currentSum, true, positiveStatus);

					currentFirst = currentFirst.previousBlock;
					currentSecond = currentSecond.previousBlock;

					if (currentFirst == null)
					{
						result = BigNumberDSMath.Add(currentSecond, currentFirst, result, addOne);
					}
					else
					{
						result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);
					}
				}
				else
				{
					if (firstMem.isPositive ^ secondMem.isPositive)
					{
						if (!firstMem.isPositive)
						{
							currentFirst = secondMem;
							currentSecond = firstMem;
						}

						positiveStatus = currentFirst.isPositive;

						if (currentFirst.currentValue < currentSecond.currentValue && BigNumberDSHelper.GetHelpFromPreviousBlocks(currentFirst))
						{
							currentSum = currentFirst.currentValue + MAX_ALLOWED_VALUE - 1 - currentSecond.currentValue;
						}
						else if (currentFirst.currentValue >= currentSecond.currentValue)
						{
							currentSum = currentFirst.currentValue - currentSecond.currentValue;
						}
						else
						{
							currentSum = Math.Abs(currentFirst.currentValue - currentSecond.currentValue);

							positiveStatus = false;
						}
					}
					else
					{
						positiveStatus = currentFirst.isPositive;

						currentSum = currentFirst.currentValue + currentSecond.currentValue;
					}

					if (addOne)
					{
						currentSum++;
						addOne = false;
					}

					if (currentSum > MAX_ALLOWED_VALUE)
					{
						currentSum = currentSum - MAX_ALLOWED_VALUE - 1;
						addOne = true;
					}

					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, true, positiveStatus);

					currentFirst = currentFirst.previousBlock;
					currentSecond = currentSecond.previousBlock; // TODO Add null rotate

					if (currentFirst == null)
					{
						result = BigNumberDSMath.Add(currentSecond, currentFirst, result, addOne);
					}
					else
					{
						result = BigNumberDSMath.Add(currentFirst, currentSecond, result, addOne);
					}
				}
			}
			else if (firstMem != null)
			{
				positiveStatus = firstMem.isPositive;

				currentSum = firstMem.currentValue;

				if (addOne)
				{
					currentSum++;
					addOne = false;
				}

				if (currentSum > MAX_ALLOWED_VALUE)
				{
					currentSum = currentSum - MAX_ALLOWED_VALUE - 1;
					addOne = true;
				}

				if (firstMem.previousBlock != null)
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, true, positiveStatus);

					currentFirst = firstMem.previousBlock;

					result = BigNumberDSMath.Add(currentFirst, null, result, addOne);
				}
				else
				{
					result = BigNumberDSHelper.AddNewPreviousBlock(result, currentSum, true, positiveStatus);

					result = BigNumberDSMath.Add(null, null, result, addOne);
				}
			}
			else if (addOne)
			{
				result = BigNumberDSHelper.AddNewPreviousBlock(result, 1, true, result.isPositive);

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

			if ((rhs.currentValue == 0) || (lhs.currentValue == 0))
			{
				return new BigNumberDS("0");
			}

			if (rhs.currentValue == -1)
			{
				return -((BigNumberDS)lhs.Clone());
			}

			if (rhs.currentValue == 1)
			{
				return (BigNumberDS)lhs.Clone();
			}

			if (lhs.currentValue == -1)
			{
				return -((BigNumberDS)rhs.Clone());
			}

			if (lhs.currentValue == 1)
			{
				return (BigNumberDS)rhs.Clone();
			}

			// simple optimization. TODO check, is it an optimization
			if ((rhs.previousBlock == null) && // if rhs is less than MAX_ALLOWED_VALUE
			    (rhs.currentValue < short.MaxValue) &&
			    (BigNumberDSHelper.GetFractionPartBlocksCount(rhs) == 0)) // and has no fraction
			{
				return BigNumberDSMath.Multiple(lhs, (short)rhs.currentValue);
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
			BigNumberDS current = BigNumberDSHelper.GetWithoutDot(rhs);
			BigNumberDS output = new BigNumberDS();

			while (current != null)
			{
				byte[] a = BigNumberDSHelper.IntArrayParse(current.currentValue);
				int k = 0;

				for (int i = 0; i < a.Length; i++)
				{
					BigNumberDS tmp = new BigNumberDS();

					tmp = lhsrough * a[i];

					for (int j = 0; j < k; j++)
					{
						tmp *= 10;
					}

					output += tmp;
					k++;
				}
				current = current.previousBlock;
			}

			BigNumberDSHelper.TrimStructure(ref output);

			return output;
		}

		internal static BigNumberDS Multiple(BigNumberDS lhs, short rhs)
		{
			#region checks

			if ((rhs == 0) || ((lhs.currentValue == 0) && (lhs.previousBlock == null)))
			{
				return new BigNumberDS("0");
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
				output.isPositive ^= isRhsNegative;
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