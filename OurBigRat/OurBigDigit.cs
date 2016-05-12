namespace OurBigRat
{
	using System.Collections.Generic;
	using bigint = OurBigInt;

	internal class OurBigDigit
	{
		public OurBigDigit()
		{
			this.Value = new bool[bigint.BOOL_ARRAY_SIZE];
		}

		public OurBigDigit(IList<bool> v)
		{
			this.Value = v;
		}

		internal IList<bool> Value { get; set; }
	}
}