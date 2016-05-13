namespace BigRat
{
	using System;

	internal class BigIntMath : Singleton<BigIntMath>
	{
		private BigIntMathHelper mathHelper = BigIntMathHelper.Instance;
		private BigIntMath() { }

        internal BigInt Add(BigInt lhs, BigInt rhs)
        {
            return
        }

        internal BigInt Add(BigInt lhs, BigInt rhs, BigInt result, bool addOne)
		{
			throw new NotImplementedException();

            if (firstMem == null && secondMem == null && result == null)
            {
                throw new ArgumentNullException("Can't add two numbers");
            }
            else if (firstMem != null &&
                secondMem != null &&
                (!firstMem.isPositive ||
                firstMem < secondMem))
            {
                throw new ArgumentException("4ot ne srabotalo");
            }

            big currentFirst;
            big currentSecond;

            uint currentSum = 0;
            bool isBigPart;

            currentFirst = firstMem == null ?
                null :
                (big)firstMem.Clone();
            currentSecond = secondMem == null ?
                null :
                (big)secondMem.Clone();

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

                    currentSum = (uint)Math.Abs(BigNumberDSMath.MAX_ALLOWED_VALUE + 1 - currentSecond.currentValue);

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
                    result = new big(currentSum, false, true);
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
                    result = new big(currentSum, isBigPart, true);
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

		internal BigInt Divide(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Multiple(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Reminder(BigInt lhs, BigInt rhs)
		{
			throw new NotImplementedException();
		}

		internal BigInt Subtract(BigInt lhs, BigInt one)
		{
			throw new NotImplementedException();
		}
	}
}