namespace OurBigRat
{
	using System;
	using System.Collections.Generic;
	using digit = OurBigDigit;

	public static class OurBigDigitMathHelper
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

		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		internal static bool IsPowerOfTwo(int num)
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

		internal static int NextPowerOfTwo(int v)
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

		internal static digit Trim(digit m)
		{
			digit result = new digit();
			;
			bool found = false;

			for (int i = m.Value.Length - 1; i >= 0; i--)
			{
				if ((!found) && (m.Value[i]))
				{
					found = true;
				}

				if (found)
				{
					result.Value[i] = m.Value[i];
				}
			}

			return result;
		}

		internal static digit UnaryMinus(digit num)
		{
			digit one = new digit();
			one.Value[0] = true;

			bool trash = false;
			return OurBigDigitMath.DigitSum(OurBigDigitMathHelper.Invert(num), one, ref trash);
		}

		internal static bool[] UnaryMinus(bool[] num)
		{
			bool[] one = new bool[num.Length];
			one[0] = true;

			bool trash = false;
			return OurBigDigitMath.BitArraySum(OurBigDigitMathHelper.Invert(num), one, ref trash);
		}

		internal static digit Invert(digit input)
		{
			digit result = new digit();

			for (int i = 0; i < digit.RADIX; i++)
			{
				result.Value[i] = !input.Value[i];
			}

			return result;
		}

		internal static bool[] Invert(bool[] input)
		{
			bool[] result = new bool[input.Length];

			for (int i = 0; i < digit.RADIX; i++)
			{
				result[i] = !input[i];
			}

			return result;
		}

		internal static bool[] BoolArrayRightShift(bool[] arr, int shift)
		{
			bool[] result = new bool[arr.Length];

			for (int j = 0; j < shift; j++)
			{
				for (int i = shift; i < result.Length; i++)
				{
					result[i - shift] = arr[i];
				}
			}

			return result;
		}
	}
}