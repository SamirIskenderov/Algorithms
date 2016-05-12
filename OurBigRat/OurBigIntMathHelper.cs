namespace OurBigRat
{
	using System;
	using System.Linq;
	using digit = OurBigDigit;
	using bigint = OurBigInt;

	public static class OurBigIntMathHelper
	{
		/// <summary>
		/// If input has zero blocks in end of a fraction part or in start of integer part, this func will remove it.
		/// F.e., 0000000001230,000123000000 will be 00000123,000123000.
		/// </summary>
		/// <param name="input"></param>
		internal static void TrimStructure(ref bigint input)
		{
			bigint current = input;
			bigint currentEdge;

			while (current != null && current.previousBlock != null)
			{
				if (current.previousBlock.digit.Value.All(x => !x)) // if all elements of collection are zeros.
				{
					currentEdge = current;

					while (current.previousBlock != null)
					{
						if (current.previousBlock.digit.Value.Any(x => x))
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

		internal static int GetBlocksCount(bigint input)
		{
			int result = 0;
			bigint current = input;

			while ((object)current != null)
			{
				result++;

				current = current.previousBlock;
			}

			return result;
		}

		internal static bigint AddNewPreviousBlock(bigint result, bool[] currentSum)
		{
			bigint current = result;

			bigint addingBlock = new bigint(currentSum);

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

		internal static void AddNTrueFilledBlocks(bigint input, int n)
		{
			if (n < 0)
			{
				throw new ArgumentException("Number of adding blocks can not be negative.");
			}
			else if (input == null)
			{
				throw new ArgumentNullException();
			}

			bigint current = input;

			while (current.previousBlock != null)
			{
				current = current.previousBlock;
			}

			digit digit = new digit(new bool[digit.RADIX]);

			for (int i = 0; i < digit.Value.Length; i++)
			{
				digit.Value[i] = true;
			}

			while (n != 0)
			{
				current.previousBlock = new bigint(digit);

				current = current.previousBlock;

				n--;
			}
		}

		internal static void TrimByBlocksCount(bigint input, int n)
		{
			if (n <= 0)
			{
				throw new ArgumentException("Number of blocks can not be negative or zero.");
			}
			else if (input == null)
			{
				throw new ArgumentNullException();
			}

			bigint current = input;

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