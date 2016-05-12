namespace OurBigRat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using bigint = OurBigInt;

	internal class OurBigDigit
	{
		internal const int RADIX = 32;

		internal OurBigDigit()
		{
			this.Value = new bool[OurBigDigit.RADIX];
		}

		internal OurBigDigit(IEnumerable<bool> v)
		{
			this.Value = v.ToArray();
		}

		internal bool[] Value { get; set; }

        internal int CompareTo(OurBigDigit input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            for (int i = RADIX - 1; i >= 0; i--)
            {
                if ((this.Value[i] ^ input.Value[i]))
                {
                    if (this.Value[i])
                    {
                        return 1;
                    }

                    return -1;
                }
            }

            return 0;
        }
    }
}