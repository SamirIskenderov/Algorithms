using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using big = Algorithms.BigNumber.BigNumberDS;

namespace Algorithms.BigNumber
{
	public class BigNumberDS : IComparable<BigNumberDS>, ICloneable
	{
		public static big DivisionAccuracy { get; set; } = BigNumberDS.Create("5");

		internal uint currentValue;

		internal bool isBigPart;

		internal bool isPositive;

		internal big previousBlock;

		#region operators

		public static big operator -(big value)
		{
			return value.Invert();
		}

		public static big operator -(big lhs, big rhs)
		 => BigNumberDSMath.Subtract(lhs, rhs);

		public static big operator -(big lhs, int rhs)
		 => BigNumberDSMath.Subtract(lhs, rhs);

		public static big operator -(int lhs, big rhs)
		 => BigNumberDSMath.Subtract(rhs, lhs);

		public static big operator --(big value)
		 => BigNumberDSMath.Subtract(value, BigNumberDS.Create("1"));

		public static bool operator !=(big lhs, big rhs)
		 => !(lhs == rhs);

		public static bool operator !=(int lhs, big rhs)
			=> rhs != lhs;

		public static bool operator !=(big lhs, int rhs)
			=> lhs != BigNumberDS.Create(rhs.ToString());

		public static big operator *(big lhs, big rhs)
		{
			return BigNumberDSMath.Multiple(lhs, rhs);
		}

		public static big operator *(big lhs, byte rhs)
		{
			return BigNumberDSMath.Multiple(lhs, rhs);
		}

		public static big operator *(byte lhs, big rhs)
			=> rhs * lhs;

		public static big operator /(big lhs, big rhs)
		{
			return BigNumberDSMath.Divide(lhs, rhs);
		}

		public static big operator /(big lhs, int rhs)
		{
			return BigNumberDSMath.Divide(lhs, rhs);
		}

		public static big operator /(int lhs, big rhs)
		    => rhs / lhs;

		public static big operator ^(int lhs, big rhs)
			=> (BigNumberDS.Create(lhs.ToString())) ^ rhs;

		public static big operator ^(big lhs, int rhs)
			=> (rhs ^ BigNumberDS.Create(rhs.ToString()));

		public static big operator ^(big lhs, big rhs)
			=> BigNumberDSMath.Exponentiation(lhs, rhs);

		public static big operator +(big value)
		{
			value.isPositive = true;
			return value;
		}

		public static big operator +(big lhs, big rhs)
		 => BigNumberDSMath.Add(lhs, rhs);

		public static big operator +(big lhs, int rhs)
		 => BigNumberDSMath.Add(lhs, rhs);

		public static big operator +(int lhs, big rhs)
		 => BigNumberDSMath.Add(rhs, lhs);

		public static big operator ++(big value)
		 => BigNumberDSMath.Add(value, BigNumberDS.Create("1"));

		public static bool operator <(big lhs, big rhs)
		 => lhs.CompareTo(rhs) < 0;

		public static bool operator <(big lhs, int rhs)
		 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) < 0;

		public static bool operator <(int lhs, big rhs)
		 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) >= 0);

		public static bool operator <=(big lhs, big rhs)
						 => lhs.CompareTo(rhs) <= 0;

		public static bool operator <=(big lhs, int rhs)
						 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) <= 0;

		public static bool operator <=(int lhs, big rhs)
						 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) > 0);

		public static bool operator ==(int lhs, big rhs)
			=> rhs == lhs;

		public static big operator &(int lhs, big rhs)
			=> BigNumberDS.Create(lhs.ToString()) & rhs;

		public static big operator &(big lhs, int rhs)
			=> BigNumberDS.Create(rhs.ToString()) & lhs;

		public static big operator &(big lhs, big rhs)
		{
			return BigNumberDSMath.And(lhs, rhs);
		}

		public static big operator |(int lhs, big rhs)
			=> BigNumberDS.Create(lhs.ToString()) | rhs;

		public static big operator |(big lhs, int rhs)
			=> BigNumberDS.Create(rhs.ToString()) | lhs;

		public static big operator |(big lhs, big rhs)
		{
			return BigNumberDSMath.Or(lhs, rhs);
		}

		public static bool operator ==(big lhs, int rhs)
			=> lhs == BigNumberDS.Create(rhs.ToString());

		public static bool operator ==(big lhs, big rhs)
		{
			if ((object)lhs == null && (object)rhs == null)
			{
				return true;
			}
			else if (((object)lhs == null) || ((object)rhs == null))
			{
				return false;
			}

			return lhs.CompareTo(rhs) == 0;
		}

		public static bool operator >(big lhs, big rhs)
		 => lhs.CompareTo(rhs) > 0;

		public static bool operator >(big lhs, int rhs)
		 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) > 0;

		public static bool operator >(int lhs, big rhs)
		 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) <= 0);

		public static bool operator >=(big lhs, int rhs)
																				 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) >= 0;

		public static bool operator >=(int lhs, big rhs)
						 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) < 0);

		public static bool operator >=(big lhs, big rhs)
				 => lhs.CompareTo(rhs) >= 0;

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			big p = obj as BigNumberDS;
			if ((object)p == null)
			{
				return false;
			}

			return this == p;
		}

		public bool Equals(big p)
		{
			if ((object)p == null)
			{
				return false;
			}

			return this == p;
		}

		public override int GetHashCode()
		{
			big block = this;
			uint hash = 0;

			while ((block != null))
			{
				hash ^= block.currentValue;
				hash ^= block.isPositive ? (byte)3 : (byte)7;

				block = block.previousBlock;
			}

			return (int)hash;
		}

		#endregion operators

		#region ctor

		public BigNumberDS()
		{
			this.currentValue = 0;
			this.isBigPart = true;
			this.isPositive = true;
			this.previousBlock = null;
		}

		internal BigNumberDS(string inputString, bool isPositive)
		{
			if (string.IsNullOrWhiteSpace(inputString))
			{
				throw new ArgumentNullException("Input is empty");
			}

			if (!Regex.IsMatch(inputString, @"[0-9,]*")) // TODO to rewrite , to , and . and smth else
			{
				throw new ArgumentException($"Unknown symbols in string {inputString}");
			}

			if (inputString.Contains(','))
			{
				string currentSmallPart = inputString.Split(',')[1];

				if (currentSmallPart.Length == 9)
				{
					this.currentValue = Convert.ToUInt32(currentSmallPart);
					this.isBigPart = false;
					this.isPositive = isPositive;

					if (inputString.Split(',')[0] != string.Empty)
					{
						this.previousBlock = new BigNumberDS(inputString.Split(',')[0], isPositive);
					}
				}
				else if (currentSmallPart.Length < 9)
				{
					uint additionMultiplier = 10;

					for (int i = 0; i < 8 - currentSmallPart.Length % 9; i++)
					{
						additionMultiplier = additionMultiplier * 10;
					}

					this.currentValue = additionMultiplier * Convert.ToUInt32(currentSmallPart.Substring(currentSmallPart.Length - (currentSmallPart.Length % 9)));
					this.isBigPart = false;
					this.isPositive = isPositive;

					if (inputString.Split(',')[0] != string.Empty)
					{
						this.previousBlock = new BigNumberDS(inputString.Split(',')[0], isPositive);
					}
				}
				else
				{
					if (currentSmallPart.Length % 9 == 0)
					{
						this.currentValue = Convert.ToUInt32(currentSmallPart.Substring(currentSmallPart.Length - 9));
						this.isBigPart = false;
						this.isPositive = isPositive;

						this.previousBlock = new BigNumberDS(inputString.Substring(0, inputString.Length - 9), isPositive);
					}
					else
					{
						uint additionMultiplier = 10;

						for (int i = 0; i < 8 - currentSmallPart.Length % 9; i++)
						{
							additionMultiplier = additionMultiplier * 10;
						}

						this.currentValue = additionMultiplier * Convert.ToUInt32(currentSmallPart.Substring(currentSmallPart.Length - (currentSmallPart.Length % 9)));
						this.isBigPart = false;
						this.isPositive = isPositive;

						this.previousBlock = new BigNumberDS(inputString.Substring(0, inputString.Length - (currentSmallPart.Length % 9)), isPositive);
					}
				}
			}
			else
			{
				if (inputString.Length <= 9)
				{
					this.currentValue = Convert.ToUInt32(inputString);
					this.isBigPart = true;
					this.isPositive = isPositive;
				}
				else
				{
					this.currentValue = Convert.ToUInt32(inputString.Substring(inputString.Length - 9));
					this.isBigPart = true;
					this.isPositive = isPositive;

					this.previousBlock = new BigNumberDS(inputString.Substring(0, inputString.Length - 9), isPositive);
				}
			}
		}

		internal BigNumberDS(uint input, bool isBigPart, bool isPositive)
		{
			this.currentValue = input;

			this.isBigPart = isBigPart;

			this.isPositive = isPositive;
		}

		public static big Create(string input = "")
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return new BigNumberDS();
			}
			else
			{
				big result = new BigNumberDS(input[0] == '-' ? input.Substring(1) : input, input[0] != '-');

				BigNumberDSHelper.TrimStructure(ref result);

				return result;
			}
		}

		#endregion ctor

		/// <summary>
		/// Getting absolute value of number
		/// </summary>
		/// <returns></returns>
		public big Abs()
		{
			big current = (BigNumberDS)this.Clone();

			if (this.isPositive)
			{
				return current;
			}
			else
			{
				big result = current;

				while (current != null)
				{
					current.isPositive = true;

					current = current.previousBlock;
				}

				return result;
			}
		}

		public static bool GetNextBit(ulong num)
		{
			ulong div = NextPowerOfTwo(num);
			bool bit;

			num++;

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
			}

			return false; // temporary, to shut c# up
		}

		public static ulong NextPowerOfTwo(ulong v)
		{
			// compute the next highest power of 2 of 32-bit v

			v--;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v++;

			return v;
		}

		public object Clone()
		{
			big current = this;

			big result = new BigNumberDS();

			big currentForResult = result;
			currentForResult.currentValue = current.currentValue;
			currentForResult.isBigPart = current.isBigPart;
			currentForResult.isPositive = current.isPositive;

			while (current.previousBlock != null)
			{
				current = current.previousBlock;
				currentForResult.previousBlock = new BigNumberDS();
				currentForResult = currentForResult.previousBlock;

				currentForResult.currentValue = current.currentValue;
				currentForResult.isBigPart = current.isBigPart;
				currentForResult.isPositive = current.isPositive;
			}

			return (object)result;
		}

		public int CompareTo(big other)
		{
			big tmp = this;
			BigNumberDSHelper.TrimStructure(ref tmp);
			BigNumberDSHelper.TrimStructure(ref other);

			if ((object)other == null)
			{
				return 1;
			}

			if (tmp.isPositive ^ other.isPositive)
			{
				if (tmp.isPositive)
				{
					return 1;
				}

				return -1;
			}
			else if (tmp.isPositive)
			{
				if (BigNumberDSHelper.GetIntegerPartBlocksCount(tmp) > BigNumberDSHelper.GetIntegerPartBlocksCount(other))
				{
					return 1;
				}
				else if (BigNumberDSHelper.GetIntegerPartBlocksCount(tmp) < BigNumberDSHelper.GetIntegerPartBlocksCount(other))
				{
					return -1;
				}

				int[] comparisionMap = BigNumberDSHelper.MakeComparisionMap(tmp, other);

				for (int i = 0; i < comparisionMap.Length; i++)
				{
					if (comparisionMap[i] != 0)
					{
						return comparisionMap[i];
					}
				}

				return 0;
			}
			else
			{
				if (BigNumberDSHelper.GetIntegerPartBlocksCount(tmp) > BigNumberDSHelper.GetIntegerPartBlocksCount(other))
				{
					return -1;
				}
				else if (BigNumberDSHelper.GetIntegerPartBlocksCount(tmp) < BigNumberDSHelper.GetIntegerPartBlocksCount(other))
				{
					return 1;
				}

				int[] comparisionMap = BigNumberDSHelper.MakeComparisionMap(tmp, other);

				for (int i = 0; i < comparisionMap.Length; i++)
				{
					if (comparisionMap[i] != 0)
					{
						return -1 * comparisionMap[i];
					}
				}

				return 0;
			}
		}

		/// <summary>
		/// Change the sign
		/// </summary>
		/// <returns></returns>
		public big Invert()
		{
			big current = (BigNumberDS)this.Clone();

			big result = current;

			while (current != null)
			{
				current.isPositive = !current.isPositive;

				current = current.previousBlock;
			}

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			big currentSbnf = (BigNumberDS)this.Clone();

			BigNumberDSHelper.TrimStructure(ref currentSbnf);

			sb = BigNumberDSHelper.MakeTextString(currentSbnf, null, true);

			if (!this.isPositive)
			{
				sb.Insert(0, "-");
			}

			return sb.ToString();
		}

		public big Divide(big divider, big accuracy)
		{
			return BigNumberDSMath.Divide(this, divider, accuracy);
		}

		
	}
}