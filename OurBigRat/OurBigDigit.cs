namespace OurBigRat
{
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
	}
}