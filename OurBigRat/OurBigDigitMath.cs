namespace OurBigRat
{
	using System;

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
			digit result = new digit();

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
		/// Modifies result aray as sum of two arrays or sum of one bit and array
		/// </summary>
		/// <param name="m"></param>
		/// <param name="r"></param>
		/// <param name="overflow"></param>
		/// <returns></returns>
		public static digit DigitMultiple(digit m, digit r, out digit overflow)
		{
			overflow = new digit();

			digit mcopy = OurBigDigitMathHelper.Trim(m);
			digit mminuscopy = OurBigDigitMathHelper.UnaryMinus(mcopy);
			digit rcopy = OurBigDigitMathHelper.Trim(r);

			int x = mcopy.Value.Length;
			int y = rcopy.Value.Length;

			int length = x + y + 1;
			digit A = new digit(new bool[length]);
			digit S = new digit(new bool[length]);
			digit P = new digit(new bool[length]);

			for (int i = y + 1; i < x + y + 1; i++)
			{
				A.Value[i] = mcopy.Value[i - y - 1];
				S.Value[i] = mminuscopy.Value[i - y - 1];
			}

			for (int i = 0; i < x + 1; i++)
			{
				P.Value[i + 1] = rcopy.Value[i];
			}

			for (int i = 0; i < y; i++)
			{
				bool last = P.Value[0];
				bool penult = P.Value[1];

				if (!penult && last) // 01
				{
					bool trash = false;
					P = OurBigDigitMath.DigitSum(P, A, ref trash);
				}

				if (penult && !last) // 10
				{
					bool trash = false;
					P = OurBigDigitMath.DigitSum(P, S, ref trash);
				}

				P = OurBigDigitMath.DigitRightShift(P, 1);
			}

			P = OurBigDigitMath.DigitRightShift(P, 1);

			return P;
		}
	}
}