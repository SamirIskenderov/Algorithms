namespace OurBigRat
{
	using System;
	using System.Linq;
	using System.Text;

	public class OurBigDigit
	{
		public const int RADIX = 4;

		public OurBigDigit()
		{
			this.Value = new bool[OurBigDigit.RADIX];
		}

		public OurBigDigit(bool[] v) : this()
		{
			for (int i = 0; i < Math.Min(OurBigDigit.RADIX, v.Length); i++)
			{
				this.Value[i] = v[i];
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			OurBigDigit p = obj as OurBigDigit;
			if (p == null)
			{
				return false;
			}

			return this.CompareTo(p) == 0;
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

		private static readonly OurBigDigit zero = new OurBigDigit();

		public static OurBigDigit Zero
		{
			get
			{
				return zero;
			}
		}

		private static readonly OurBigDigit one = new OurBigDigit(new bool[] { true });

		public static OurBigDigit One
		{
			get
			{
				return one;
			}
		}

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