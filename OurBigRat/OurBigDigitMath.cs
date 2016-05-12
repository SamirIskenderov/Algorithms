﻿namespace OurBigRat
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
				for (int i = shift; i < digit.RADIX; i++)
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
				for (int i = 0; i < digit.RADIX - shift; i++)
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

            bool[] unoArray = new bool[digit.RADIX];

            unoArray[digit.RADIX - 1] = true;

            bool trash = false;

            rhs = OurBigDigitMath.DigitSum(rhs, new digit(unoArray), ref trash);

            return OurBigDigitMath.DigitSum(lhs, rhs, ref trash);
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

			int x = m.Value.Length;
			int y = r.Value.Length;

			int length = x + y + 1;
			digit A = new digit(new bool[length]);
			digit S = new digit(new bool[length]);
			digit P = new digit(new bool[length]);

			for (int i = 0; i < x; i++)
			{
				A.Value[i] = m.Value[i];
				S.Value[i] = !m.Value[i];
			}

			for (int i = x; i < x + y; i++)
			{
				P.Value[i] = r.Value[i - x];
			}

			for (int i = 0; i < y; i++)
			{
				bool last = P.Value[length - 1];
				bool penult = P.Value[length - 2];

				if ((!penult && !last) ||
					(penult && last)) // 00 or 11
				{
					continue;
				}

				if (!penult && last) // 01
				{
					bool nothing = false;
					P = OurBigDigitMath.DigitSum(P, A, ref nothing);
				}
				else // 10
				{
					bool nothing = false;
					P = OurBigDigitMath.DigitSum(P, S, ref nothing);
				}

				P = OurBigDigitMath.DigitRightShift(P, 1);
			}

			P = OurBigDigitMath.DigitRightShift(P, 1);

			return P;
		}
	}
}