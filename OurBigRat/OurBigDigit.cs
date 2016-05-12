namespace OurBigRat
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class OurBigDigit
	{
		public const int RADIX = 32;

		public OurBigDigit()
		{
			this.Value = new bool[OurBigDigit.RADIX];
		}

		public OurBigDigit(IEnumerable<bool> v)
		{
			this.Value = v.ToArray();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			byte a = 0;

			sb.Append("[ ");

			foreach (var item in this.Value.Reverse())
			{
				sb.Append(item ? '1' : '0');

				if (a == 3)
				{
					sb.Append(' ');
					a = 0;
				}
				else
				{
					a++;
				}
			}

			sb.Append("] ");

			return sb.ToString();
		}

		public bool[] Value { get; set; }

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