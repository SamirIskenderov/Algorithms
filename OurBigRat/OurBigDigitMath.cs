namespace OurBigRat
{
	using System;
	using System.Linq;
	using System.Text;
	using digit = OurBigDigit;

	public static class OurBigDigitMath
	{
		internal static digit DigitRightShift(digit digit, int shift)
		{
			digit result = new digit(new bool[digit.Value.Length]);

			for (int j = 0; j < shift; j++)
			{
				for (int i = shift; i < result.Value.Length; i++)
				{
					result.Value[i - shift] = digit.Value[i];
				}
			}

			return result;
		}

		internal static digit DigitLeftShift(digit digit, int shift)
		{
			digit result = new digit(new bool[digit.Value.Length]);

			for (int j = 0; j < shift; j++)
			{
				for (int i = 0; i < result.Value.Length - shift; i++)
				{
					result.Value[i + shift] = digit.Value[i];
				}
			}

			return result;
		}

		/// <summary>
		/// Modifies result aray as sum of two arrays or sum of one bit and array.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <param name="result"></param>
		/// <param name="bitOverflow"></param>
		internal static digit DigitSum(digit lhs, digit rhs, ref bool bitOverflow)
		{
			digit result = null;
			if (lhs.Value.Length == rhs.Value.Length)
			{
				result = new digit(new bool[lhs.Value.Length]);
			}
			else
			{
				result = new digit();
			}

			if (lhs == null ||
			    result == null)
			{
				throw new ArgumentNullException();
			}
			else if ((rhs != null && (lhs.Value.Length != rhs.Value.Length || rhs.Value.Length != result.Value.Length)) ||
			    (lhs.Value.Length != result.Value.Length))
			{
				throw new ArgumentException("Input arrays Value.Length must be equal.");
			}
			if (rhs != null)
			{
				for (int i = 0; i < result.Value.Length; i++)
				{
					result.Value[i] = lhs.Value[i] ^ rhs.Value[i] ^ bitOverflow;
					bitOverflow = (lhs.Value[i] && rhs.Value[i]) || ((lhs.Value[i] || rhs.Value[i]) && bitOverflow);
				}
			}
			else
			{
				for (int i = 0; i < result.Value.Length; i++)
				{
					result.Value[i] = lhs.Value[i] ^ bitOverflow;
					bitOverflow = lhs.Value[i] && bitOverflow;
				}
			}

			return result;
		}

		/// <summary>
		/// Modifies result aray as sum of two arrays or sum of one bit and array.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <param name="result"></param>
		/// <param name="bitOverflow"></param>
		internal static bool[] BitArraySum(bool[] lhs, bool[] rhs, ref bool bitOverflow)
		{
			if (lhs == null || rhs == null)
			{
				throw new ArgumentNullException();
			}

			if (lhs.Length != rhs.Length)
			{
				throw new ArgumentException("Arraies length must be the same");
			}

			bool[] result = new bool[Math.Max(lhs.Length, rhs.Length)];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = lhs[i] ^ rhs[i] ^ bitOverflow;
				bitOverflow = (lhs[i] && rhs[i]) || ((lhs[i] || rhs[i]) && bitOverflow);
			}

			return result;
		}

		internal static digit DigitSubtract(digit lhs, digit rhs)
		{
			if (lhs == null || rhs == null)
			{
				throw new ArgumentNullException();
			}
			else if (lhs.CompareTo(rhs) < 0)
			{
				throw new ArithmeticException("First member can not be less than second one.");
			}
			else if (lhs.CompareTo(rhs) == 0)
			{
				return new digit();
			}

			rhs = OurBigDigitMathHelper.Invert(rhs);

			bool trash = false;

			rhs = OurBigDigitMath.DigitSum(rhs, digit.One, ref trash);

			return OurBigDigitMath.DigitSum(lhs, rhs, ref trash);
		}

		public static string ATS(bool[] arr)
		{
			StringBuilder sb = new StringBuilder();
			byte a = 0;

			sb.Append("[ ");

			foreach (var item in arr.Reverse())
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

		/// <summary>
		/// Modifies result aray as sum of two arrays or sum of one bit and array
		/// </summary>
		/// <param name="m"></param>
		/// <param name="r"></param>
		/// <param name="overflow"></param>
		/// <returns></returns>
		public static digit DigitMultiple(digit m, digit r, out digit overflow)
		{
			if (m.Equals(digit.Zero))
			{
				overflow = new digit();
				return new digit();
			}

			if (r.Equals(digit.Zero))
			{
				overflow = new digit();
				return new digit();
			}

			digit mcopy = OurBigDigitMathHelper.Trim(m);
			digit mminuscopy = OurBigDigitMathHelper.UnaryMinus(mcopy);
			digit rcopy = OurBigDigitMathHelper.Trim(r);

			int x = mcopy.Value.Length;
			int y = rcopy.Value.Length;

			int length = x + y + 1;
			bool[] A = new bool[length];
			bool[] S = new bool[length];
			bool[] P = new bool[length];

			for (int i = y + 1; i < x + y + 1; i++)
			{
				A[i] = mcopy.Value[i - y - 1];
				S[i] = mminuscopy.Value[i - y - 1];
			}

			for (int i = 0; i < x; i++)
			{
				P[i + 1] = rcopy.Value[i];
			}

			for (int i = 0; i < y; i++)
			{
				bool last = P[0];
				bool penult = P[1];
				bool trash = false;

				if (!penult && last) // 01
				{
					P = OurBigDigitMath.BitArraySum(P, A, ref trash);
				}

				if (penult && !last) // 10
				{
					P = OurBigDigitMath.BitArraySum(P, S, ref trash);
				}

				if (trash)
				{
					bool[] tmp = new bool[P.Length];
					tmp[0] = true;
					OurBigDigitMath.BitArraySum(P, tmp, ref trash);
				}

				P = OurBigDigitMathHelper.BoolArrayRightShift(P, 1);
			}

			P = OurBigDigitMathHelper.BoolArrayRightShift(P, 1);

			digit result = new digit(P);

			overflow = new digit(OurBigDigitMathHelper.BoolArrayRightShift(P, digit.RADIX));
			if (!overflow.Equals(digit.Zero))
			{

			//overflow = OurBigDigitMath.DigitSubtract(overflow, digit.One);
			}
			return result;
		}

		internal static bool[] BoolArraySubtract(bool[] lhs, bool[] rhs)
		{
			rhs = OurBigDigitMathHelper.UnaryMinus(rhs);

			bool trash = false;

			return OurBigDigitMath.BitArraySum(lhs, rhs, ref trash);
		}
	}
}