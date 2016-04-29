using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Algorithms.BigNumber
{
	public class BigNumberDS : IComparable<BigNumberDS>, ICloneable
	{
        public static BigNumberDS DivisionAccuracy { get; set; } = BigNumberDS.Create("5");

		internal int currentValue;

		internal bool isBigPart;

		internal bool isPositive;

		internal BigNumberDS previousBlock;

		#region operators

		public static BigNumberDS operator -(BigNumberDS value)
		{
			return value.Invert();
		}

		public static BigNumberDS operator -(BigNumberDS lhs, BigNumberDS rhs)
		 => BigNumberDSMath.Subtract(lhs, rhs);

		public static BigNumberDS operator -(BigNumberDS lhs, int rhs)
		 => BigNumberDSMath.Subtract(lhs, rhs);

		public static BigNumberDS operator -(int lhs, BigNumberDS rhs)
		 => BigNumberDSMath.Subtract(rhs, lhs);

		public static BigNumberDS operator --(BigNumberDS value)
		 => BigNumberDSMath.Subtract(value, BigNumberDS.Create("1"));

		public static bool operator !=(BigNumberDS lhs, BigNumberDS rhs)
		 => !(lhs == rhs);

		public static bool operator !=(int lhs, BigNumberDS rhs)
			=> rhs != lhs;

		public static bool operator !=(BigNumberDS lhs, int rhs)
			=> lhs != BigNumberDS.Create(rhs.ToString());

		public static BigNumberDS operator *(BigNumberDS lhs, BigNumberDS rhs)
		{
			return BigNumberDSMath.Multiple(lhs, rhs);
		}

		public static BigNumberDS operator *(BigNumberDS lhs, short rhs)
		{
			return BigNumberDSMath.Multiple(lhs, rhs);
		}

		public static BigNumberDS operator *(int lhs, BigNumberDS rhs)
			=> rhs * lhs;

		public static BigNumberDS operator *(BigNumberDS lhs, int rhs)
		{
			if (rhs < short.MaxValue)
			{
				return BigNumberDSMath.Multiple(lhs, (short)rhs);
			}
			return BigNumberDSMath.Multiple(lhs, new BigNumberDS(rhs, isBigPart: true, isPositive: rhs > 0));
		}

		public static BigNumberDS operator *(short lhs, BigNumberDS rhs)
		    => rhs * lhs;

		public static BigNumberDS operator /(BigNumberDS lhs, BigNumberDS rhs)
		{
			return BigNumberDSMath.Divide(lhs, rhs);
		}

		public static BigNumberDS operator /(BigNumberDS lhs, int rhs)
		{
			return BigNumberDSMath.Divide(lhs, rhs);
		}

		public static BigNumberDS operator /(int lhs, BigNumberDS rhs)
		    => rhs / lhs;

		public static BigNumberDS operator ^(int lhs, BigNumberDS rhs)
			=> (BigNumberDS.Create(lhs.ToString())) ^ rhs;

		public static BigNumberDS operator ^(BigNumberDS lhs, int rhs)
			=> (rhs ^ BigNumberDS.Create(rhs.ToString()));

		public static BigNumberDS operator ^(BigNumberDS lhs, BigNumberDS rhs)
			=> BigNumberDSMath.Exponentiation(lhs, rhs);

		public static BigNumberDS operator +(BigNumberDS value)
		{
			value.isPositive = true;
			return value;
		}

		public static BigNumberDS operator +(BigNumberDS lhs, BigNumberDS rhs)
		 => BigNumberDSMath.Add(lhs, rhs);

		public static BigNumberDS operator +(BigNumberDS lhs, int rhs)
		 => BigNumberDSMath.Add(lhs, rhs);

		public static BigNumberDS operator +(int lhs, BigNumberDS rhs)
		 => BigNumberDSMath.Add(rhs, lhs);

		public static BigNumberDS operator ++(BigNumberDS value)
		 => BigNumberDSMath.Add(value, BigNumberDS.Create("1"));

		public static bool operator <(BigNumberDS lhs, BigNumberDS rhs)
		 => lhs.CompareTo(rhs) < 0;

		public static bool operator <(BigNumberDS lhs, int rhs)
		 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) < 0;

		public static bool operator <(int lhs, BigNumberDS rhs)
		 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) >= 0);

		public static bool operator <=(BigNumberDS lhs, BigNumberDS rhs)
						 => lhs.CompareTo(rhs) <= 0;

		public static bool operator <=(BigNumberDS lhs, int rhs)
						 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) <= 0;

		public static bool operator <=(int lhs, BigNumberDS rhs)
						 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) > 0);

		public static bool operator ==(int lhs, BigNumberDS rhs)
			=> rhs == lhs;

		public static BigNumberDS operator &(int lhs, BigNumberDS rhs)
			=> BigNumberDS.Create(lhs.ToString()) & rhs;

		public static BigNumberDS operator &(BigNumberDS lhs, int rhs)
			=> BigNumberDS.Create(rhs.ToString()) & lhs;

		public static BigNumberDS operator &(BigNumberDS lhs, BigNumberDS rhs)
		{
			return BigNumberDSMath.And(lhs, rhs);
		}

		public static BigNumberDS operator |(int lhs, BigNumberDS rhs)
			=> BigNumberDS.Create(lhs.ToString()) | rhs;

		public static BigNumberDS operator |(BigNumberDS lhs, int rhs)
			=> BigNumberDS.Create(rhs.ToString()) | lhs;

		public static BigNumberDS operator |(BigNumberDS lhs, BigNumberDS rhs)
		{
			return BigNumberDSMath.Or(lhs, rhs);
		}

		public static bool operator ==(BigNumberDS lhs, int rhs)
			=> lhs == BigNumberDS.Create(rhs.ToString());

		public static bool operator ==(BigNumberDS lhs, BigNumberDS rhs)
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

		public static bool operator >(BigNumberDS lhs, BigNumberDS rhs)
		 => lhs.CompareTo(rhs) > 0;

		public static bool operator >(BigNumberDS lhs, int rhs)
		 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) > 0;

		public static bool operator >(int lhs, BigNumberDS rhs)
		 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) <= 0);

		public static bool operator >=(BigNumberDS lhs, int rhs)
																				 => lhs.CompareTo(BigNumberDS.Create(rhs.ToString())) >= 0;

		public static bool operator >=(int lhs, BigNumberDS rhs)
						 => !(rhs.CompareTo(BigNumberDS.Create(lhs.ToString())) < 0);

		public static bool operator >=(BigNumberDS lhs, BigNumberDS rhs)
				 => lhs.CompareTo(rhs) >= 0;

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			BigNumberDS p = obj as BigNumberDS;
			if ((object)p == null)
			{
				return false;
			}

			return this == p;
		}

		public bool Equals(BigNumberDS p)
		{
			if ((object)p == null)
			{
				return false;
			}

			return this == p;
		}

		public override int GetHashCode()
		{
			BigNumberDS block = this;
			int hash = 0;

			while ((block != null))
			{
				hash ^= block.currentValue;
				hash ^= block.isPositive ? 3 : 7;

				block = block.previousBlock;
			}

			return hash;
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
					this.currentValue = Convert.ToInt32(currentSmallPart);
					this.isBigPart = false;
					this.isPositive = isPositive;

					if (inputString.Split(',')[0] != string.Empty)
					{
						this.previousBlock = new BigNumberDS(inputString.Split(',')[0], isPositive);
					}
				}
				else if (currentSmallPart.Length < 9)
				{
					int additionMultiplier = 10;

					for (int i = 0; i < 8 - currentSmallPart.Length % 9; i++)
					{
						additionMultiplier = additionMultiplier * 10;
					}

					this.currentValue = additionMultiplier * Convert.ToInt32(currentSmallPart.Substring(currentSmallPart.Length - (currentSmallPart.Length % 9)));
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
						this.currentValue = Convert.ToInt32(currentSmallPart.Substring(currentSmallPart.Length - 9));
						this.isBigPart = false;
						this.isPositive = isPositive;

						this.previousBlock = new BigNumberDS(inputString.Substring(0, inputString.Length - 9), isPositive);
					}
					else
					{
						int additionMultiplier = 10;

						for (int i = 0; i < 8 - currentSmallPart.Length % 9; i++)
						{
							additionMultiplier = additionMultiplier * 10;
						}

						this.currentValue = additionMultiplier * Convert.ToInt32(currentSmallPart.Substring(currentSmallPart.Length - (currentSmallPart.Length % 9)));
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
					this.currentValue = Convert.ToInt32(inputString);
					this.isBigPart = true;
					this.isPositive = isPositive;
				}
				else
				{
					this.currentValue = Convert.ToInt32(inputString.Substring(inputString.Length - 9));
					this.isBigPart = true;
					this.isPositive = isPositive;

					this.previousBlock = new BigNumberDS(inputString.Substring(0, inputString.Length - 9), isPositive);
				}
			}
		}

		internal BigNumberDS(int input, bool isBigPart, bool isPositive)
		{
			this.currentValue = input;

			this.isBigPart = isBigPart;

			this.isPositive = isPositive;
		}

		public static BigNumberDS Create(string input = "")
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return new BigNumberDS();
			}
			else
			{
				BigNumberDS result = new BigNumberDS(input[0] == '-' ? input.Substring(1) : input, input[0] != '-');

				BigNumberDSHelper.TrimStructure(ref result);

				return result;
			}
		}

		#endregion ctor

		/// <summary>
		/// Getting absolute value of number
		/// </summary>
		/// <returns></returns>
		public BigNumberDS Abs()
		{
			BigNumberDS current = (BigNumberDS)this.Clone();

			if (this.isPositive)
			{
				return current;
			}
			else
			{
				BigNumberDS result = current;

				while (current != null)
				{
					current.isPositive = true;

					current = current.previousBlock;
				}

				return result;
			}
		}

		public object Clone()
		{
			BigNumberDS current = this;

			BigNumberDS result = new BigNumberDS();

			BigNumberDS currentForResult = result;
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

		public int CompareTo(BigNumberDS other)
		{
			BigNumberDS tmp = this;
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
		public BigNumberDS Invert()
		{
			BigNumberDS current = (BigNumberDS)this.Clone();

			BigNumberDS result = current;

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

			BigNumberDS currentSbnf = (BigNumberDS)this.Clone();

			BigNumberDSHelper.TrimStructure(ref currentSbnf);

			sb = BigNumberDSHelper.MakeTextString(currentSbnf, null, true);

			if (!this.isPositive)
			{
				sb.Insert(0, "-");
			}

			return sb.ToString();
		}

        public BigNumberDS Divide(BigNumberDS divider, BigNumberDS accuracy)
        {
            return BigNumberDSMath.Divide(this, divider, accuracy);
        }
	}
}