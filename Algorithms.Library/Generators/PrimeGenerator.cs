using System.Collections.Generic;

namespace Algorithms.Library
{
    public class PrimeGenerator
    {
        /// <summary>
        /// Linear sieve of Eratosthenes
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        public IList<long> GetFirst(long N)
        {
            IList<long> output = new List<long>(64);
            long[] lp = new long[N];

            for (int i = 2; i < N; i++)
            {
                if (lp[i] == 0)
                {
                    lp[i] = i;
                    output.Add(i);
                }

                for (int j = 0; j < output.Count; j++)
                {
                    if ((output[j] <= lp[i]) &&
                        (output[j] * i <= N - 1))
                    {
                        lp[output[j] * i] = output[j];
                    }
                }
            }

            return output;
        }
    }
}