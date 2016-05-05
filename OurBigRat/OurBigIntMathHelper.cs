using System.Collections.Generic;

namespace OurBigRat
{
	internal static class OurBigIntMathHelper
	{
		/// <summary>
		/// Return number as a bit collection.
		/// Collection starts from top order bit.
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		internal static IEnumerable<bool> GetNextBit(ulong num)
		{
			ulong div = NextPowerOfTwo(num);
			bool bit;

			num++; // quick fix

			while ((div > 0) || (num > 1))
			{
				if (num > div)
				{
					num -= div;
					bit = true;
				}
				else
				{
					bit = false;
				}

				div /= 2;

				yield return bit;
			}
		}

		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		internal static ulong BitsToNumber(bool[] bits)
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