namespace Algorithms.BigRat
{
    internal class BigIntMathHelper
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