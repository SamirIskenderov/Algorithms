namespace OurBigRat
{
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

		internal static digit Trim(digit m)
		{
			digit result = null;
			bool found = false;

			for (int i = m.Value.Length - 1; i >= 0; i--)
			{
				if ((!found) && (m.Value[i]))
				{
					found = true;
					result = new digit(new bool[i + 1]);
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
			bool[] arr = new bool[digit.RADIX];
			arr[0] = true;

			digit one = new digit(arr);
			bool trash = false;
			return OurBigDigitMath.DigitSum(OurBigDigitMathHelper.Invert(num), one, ref trash);
		}

		internal static digit Invert(digit input)
		{
			digit result = new digit();

			for (int i = 0; i < OurBigDigit.RADIX; i++)
			{
				result.Value[i] = !input.Value[i];
			}

			return result;
		}
	}
}