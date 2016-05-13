namespace BigRat
{
	internal class BigIntMathHelper : Singleton<BigIntMathHelper>
	{
        internal int GetBlocksCount(BigInt input)
        {
            int result = 0;
            BigInt current = input;

            while ((object)current != null)
            {
                result++;

                current = current.previousBlock;
            }

            return result;
        }
    }
}