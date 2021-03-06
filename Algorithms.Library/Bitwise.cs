﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library
{
	public static class Bitwise
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

		/// <summary>
		/// Can't work with zero
		/// </summary>
		/// <param name="num"></param>
		/// <returns></returns>
		internal static bool IsPowerOfTwo(ulong num)
			=> (num & (num - 1)) == 0;

		/// <summary>
		/// Create new number from bit stream
		/// </summary>
		/// <param name="bits">Input stream</param>
		/// <returns></returns>
		public static ulong BitsToNumber(bool[] bits)
		{
			if (bits == null)
			{
				return 0;
			}

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

		/// <summary>
		/// Returns new array as sum of two arrays without overflow.
		/// </summary>
		/// <param name="lhs">Left array must have the same length as right array</param>
		/// <param name="rhs">Right array must have the same length as left array</param>
		/// <returns>New array with same length as parents have</returns>
		internal static bool[] BitArraySum(bool[] lhs, bool[] rhs)
		{
			bool bitOverflow = false;

			if (lhs == null || rhs == null)
			{
				throw new ArgumentNullException("Array is null");
			}

			if (lhs.Length != rhs.Length)
			{
				throw new ArgumentException("Arraies length must be the same");
			}

			bool[] result = new bool[lhs.Length];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = lhs[i] ^ rhs[i] ^ bitOverflow;
				bitOverflow = (lhs[i] && rhs[i]) || ((lhs[i] || rhs[i]) && bitOverflow);
			}

			return result;
		}

		/// <summary>
		/// Make -(array) in twos-complement
		/// </summary>
		/// <param name="array">Input array</param>
		/// <returns>New array with same length as parent has in twos-complement</returns>
		internal static bool[] UnaryMinus(bool[] array)
		{
			bool[] one = new bool[array.Length];
			one[0] = true;

			return BitArraySum(Invert(array), one);
		}

		/// <summary>
		/// Invert (0 -> 1 and 1 -> 0) all bits in input array
		/// </summary>
		/// <param name="input"></param>
		/// <returns>New array with same length as parent has</returns>
		internal static bool[] Invert(bool[] input)
		{
			bool[] result = new bool[input.Length];

			for (int i = 0; i < input.Length; i++)
			{
				result[i] = !input[i];
			}

			return result;
		}

		/// <summary>
		/// Modifies result array as multiple of two input arrays without overflow
		/// Method uses Booth's multiplication algorithm
		/// </summary>
		/// <param name="m">Left array must have the same length as right array</param>
		/// <param name="r">Right array must have the same length as left array</param>
		/// <returns>New array with same length as parents have</returns>
		public static bool[] BitArrayMultiple(bool[] m, bool[] r)
		{
			if ((m == null) || (r == null))
			{
				throw new ArgumentNullException("Array is null");
			}

			// if m == 0 or r == 0 - return zero.
			if ((!m.Any(bit => bit)) || (!r.Any(bit => bit)))
			{
				return new bool[] { false};
			}

			bool[] mcopy = new bool[m.Length];
			m.CopyTo(mcopy, 0);

			bool[] mminuscopy = new bool[m.Length];
			m.CopyTo(mminuscopy, 0);
			mminuscopy = UnaryMinus(mminuscopy);

			bool[] rcopy = new bool[r.Length];
			r.CopyTo(rcopy, 0);

			int x = mcopy.Length;
			int y = rcopy.Length;

			int length = x + y + 1;
			bool[] A = new bool[length];
			bool[] S = new bool[length];
			bool[] P = new bool[length];

			for (int i = y + 1; i < x + y + 1; i++)
			{
				A[i] = mcopy[i - y - 1];
				S[i] = mminuscopy[i - y - 1];
			}

			for (int i = 0; i < x; i++)
			{
				P[i + 1] = rcopy[i];
			}

			for (int i = 0; i < y; i++)
			{
				bool last = P[0];
				bool penult = P[1];

				if (!penult && last) // 01
				{
					P = BitArraySum(P, A);
				}

				if (penult && !last) // 10
				{
					P = BitArraySum(P, S);
				}

				P = BoolArrayRightShift(P, 1);
			}

			P = BoolArrayRightShift(P, 1);

			bool[] result = new bool[P.Length];
			P.CopyTo(result, 0);

			return result;
		}

		/// <summary>
		/// Right shift without overflow
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="shift"></param>
		/// <returns>New array with same length as parent has</returns>
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

		/// <summary>
		/// Returns new array as difference of two arrays without overflow.
		/// </summary>
		/// <param name="lhs">Left array must have the same length as right array</param>
		/// <param name="rhs">Right array must have the same length as left array</param>
		/// <returns>New array with same length as parents have</returns>
		internal static bool[] BoolArraySubtract(bool[] lhs, bool[] rhs)
		{
			rhs = UnaryMinus(rhs);

			return BitArraySum(lhs, rhs);
		}
	}
}
