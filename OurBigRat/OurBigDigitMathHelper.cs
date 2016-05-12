namespace OurBigRat
{
	using System;
	using System.Collections.Generic;
	using digit = OurBigDigit;
	using bigint = OurBigInt;

	internal static class OurBigDigitMathHelper
	{
		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from top order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		public static IEnumerable<bool> GetNextBit(ulong num)
		{
			if (num == 0)
			{
				yield return false;
			}

			while (num != 0)
			{
				if (num % 2 == 1)
				{
					num = (num - 1) / 2;

					yield return true;
				}
				else
				{
					num = num / 2;

					yield return false;
				}
			}
		}

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
		internal static void DigitSum(digit lhs, digit rhs, digit result, ref bool bitOverflow)
		{
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
		}

		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		public static ulong BitsToNumber(bool[] bits)
		{
			ulong div = 1;
			ulong result = 0;

			for (int i = 0; i < bits.Length; i++)
			{
				if (bits[i])
				{
					result += div;
				}

				div *= 2;
			}

			return result;
		}

		/// <summary>
		/// Compute next highest power of 2, f.e. for 114 it returns 128
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		internal static ulong NextPowerOfTwo(ulong v)
		{
			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;

			return v;
		}
	}
}