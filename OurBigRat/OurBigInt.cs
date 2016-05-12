namespace OurBigRat
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using bigint = OurBigInt;
	using digit = OurBigDigit;

	public class OurBigInt : IComparable, IEnumerable<bool>
	{
		internal digit digit;

		internal bigint previousBlock;

		#region ctor

		public OurBigInt()
		{
			this.digit = new digit();
		}

		public OurBigInt(IEnumerable<bool> bits) : this()
		{
			if (bits == null)
			{
				throw new ArgumentNullException();
			}

			int i = 0;

			bigint current = this;

			foreach (var bit in bits)
			{
				current.digit.Value[i] = bit;
				i++;

				if (i == digit.RADIX)
				{
					i = 0;

					current.previousBlock = new OurBigInt();

					current = current.previousBlock;
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			byte a = 0;

			bigint tmp = this;

			while (tmp != null)
			{
				sb.Append("[ ");

				foreach (var item in tmp.digit.Value.Reverse())
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

				tmp = tmp.previousBlock;
			}

			return sb.ToString();
		}

		public OurBigInt(ulong num) : this()
		{
			int i = 0;

			bigint current = this;

			foreach (var bit in OurBigDigitMathHelper.GetNextBit(num))
			{
				current.digit.Value[i] = bit;
				i++;

				if (i == digit.RADIX)
				{
					i = 0;

					current.previousBlock = new bigint();

					current = current.previousBlock;
				}
			}
		}

		internal OurBigInt(digit digit)
		{
			this.digit = digit;
		}

		#endregion ctor

		#region operators

		#region unary

		public static bigint operator --(bigint lhs)
			=> OurBigIntMath.Subtract(lhs, new bigint(1));

		public static bigint operator ++(bigint lhs)
			=> OurBigIntMath.Add(lhs, new bigint(1));

		public static bigint operator !(bigint lhs)
			=> OurBigIntMath.Not(lhs);

		#endregion unary

		#region binary

		public static bigint operator +(bigint lhs, bigint rhs)
			=> OurBigIntMath.Add(lhs, rhs);

		public static bigint operator +(ulong lhs, bigint rhs)
			=> OurBigIntMath.Add(new bigint(lhs), rhs);

		public static bigint operator +(bigint lhs, ulong rhs)
			=> OurBigIntMath.Add(lhs, new bigint(rhs));

		public static bigint operator -(bigint lhs, bigint rhs)
			=> OurBigIntMath.Subtract(lhs, rhs);

		public static bigint operator -(ulong lhs, bigint rhs)
			=> OurBigIntMath.Subtract(new bigint(lhs), rhs);

		public static bigint operator -(bigint lhs, ulong rhs)
			=> OurBigIntMath.Subtract(lhs, new bigint(rhs));

		public static bigint operator *(bigint lhs, bigint rhs)
			=> OurBigIntMath.Multiple(lhs, rhs);

		public static bigint operator *(ulong lhs, bigint rhs)
			=> OurBigIntMath.Multiple(new bigint(lhs), rhs);

		public static bigint operator *(bigint lhs, ulong rhs)
			=> OurBigIntMath.Multiple(lhs, new bigint(rhs));

		public static bigint operator /(bigint lhs, bigint rhs)
			=> OurBigIntMath.Divide(lhs, rhs);

		public static bigint operator /(ulong lhs, bigint rhs)
			=> OurBigIntMath.Divide(new bigint(lhs), rhs);

		public static bigint operator /(bigint lhs, ulong rhs)
			=> OurBigIntMath.Divide(lhs, new bigint(rhs));

		public static bigint operator %(bigint lhs, bigint rhs)
			=> OurBigIntMath.Reminder(lhs, rhs);

		public static bigint operator %(ulong lhs, bigint rhs)
			=> OurBigIntMath.Reminder(new bigint(lhs), rhs);

		public static bigint operator %(bigint lhs, ulong rhs)
			=> OurBigIntMath.Reminder(lhs, new bigint(rhs));

		#endregion binary

		#region eq

		public static bool operator ==(bigint lhs, bigint rhs)
		{
			object olhs = (object)lhs;
			object orhs = (object)rhs;

			if ((olhs == null) && (orhs == null))
			{
				return true;
			}

			if ((olhs == null) ^ (olhs == null))
			{
				return false;
			}

			return lhs.CompareTo(rhs) == 0;
		}

		public static bool operator ==(ulong lhs, bigint rhs)
			=> new bigint(lhs) == rhs;

		public static bool operator ==(bigint lhs, ulong rhs)
			=> lhs == new bigint(rhs);

		public static bool operator !=(bigint lhs, bigint rhs)
			=> !(lhs == rhs);

		public static bool operator !=(ulong lhs, bigint rhs)
			=> !(lhs == rhs);

		public static bool operator !=(bigint lhs, ulong rhs)
			=> !(lhs == rhs);

		public static bool operator <(bigint lhs, bigint rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <(ulong lhs, bigint rhs)
			=> new bigint(lhs) < rhs;

		public static bool operator <(bigint lhs, ulong rhs)
			=> lhs < new bigint(rhs);

		public static bool operator >(bigint lhs, bigint rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >(ulong lhs, bigint rhs)
			=> new bigint(lhs) > rhs;

		public static bool operator >(bigint lhs, ulong rhs)
			=> lhs > new bigint(rhs);

		public static bool operator <=(bigint lhs, bigint rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(ulong lhs, bigint rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(bigint lhs, ulong rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator >=(bigint lhs, bigint rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(ulong lhs, bigint rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(bigint lhs, ulong rhs)
			=> lhs == rhs || lhs > rhs;

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			bigint p = obj as bigint;
			if ((object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this == p;
		}

		public bool Equals(bigint obj)
		{
			if ((object)obj == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this == obj;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return -1; // TODO  to ask
			}

			bigint p = obj as bigint;
			if (p == null)
			{
				return -1; // TODO  to ask
			}

			return this.CompareTo(p);
		}

		public int CompareTo(bigint input)
		{
			if ((object)input == null)
			{
				return 1;
			}

			bigint thiscopy = this;

			OurBigIntMathHelper.TrimStructure(ref thiscopy);
			OurBigIntMathHelper.TrimStructure(ref input);

			int lhsBlockCount = OurBigIntMathHelper.GetBlocksCount(this);
			int rhsBlockCount = OurBigIntMathHelper.GetBlocksCount(input);

			if (lhsBlockCount < rhsBlockCount)
			{
				return -1;
			}
			else if (lhsBlockCount > rhsBlockCount)
			{
				return 1;
			}

			bigint lhscopy = this.DeepClone();
			bigint rhscopy = input.DeepClone();

			bigint lhscopyParent = lhscopy;
			bigint rhscopyParent = rhscopy;

			bigint tmp = new bigint();

			if (lhsBlockCount != 1)
			{
				while (lhscopy.previousBlock.previousBlock != null)
				{
					lhscopy = lhscopy.previousBlock;
					rhscopy = rhscopy.previousBlock;
				}

				for (int i = digit.RADIX - 1; i >= 0; i--)
				{
					if ((lhscopy.previousBlock.digit.Value[i] ^ rhscopy.previousBlock.digit.Value[i]) && lhscopy.previousBlock.digit.Value[i])
					{
						return 1;
					}
					else if ((lhscopy.previousBlock.digit.Value[i] ^ rhscopy.previousBlock.digit.Value[i]) && rhscopy.previousBlock.digit.Value[i])
					{
						return -1;
					}
				}

				lhscopy.previousBlock = null;
				rhscopy.previousBlock = null;

				return lhscopyParent.CompareTo(rhscopyParent);
			}
			else
			{
				for (int i = digit.RADIX - 1; i >= 0; i--)
				{
					if ((lhscopy.digit.Value[i] ^ rhscopy.digit.Value[i]) && lhscopy.digit.Value[i])
					{
						return 1;
					}
					else if ((lhscopy.digit.Value[i] ^ rhscopy.digit.Value[i]) && rhscopy.digit.Value[i])
					{
						return -1;
					}
				}
			}

			return 0;
		}

		public IEnumerator<bool> GetEnumerator()
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}

			bigint current = this;

			while (current != null)
			{
				for (int i = 0; i < current.digit.Value.Length; i++)
				{
					yield return current.digit.Value[i];
				}

				current = current.previousBlock;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion eq

		#region bitwise

		public static bigint operator &(bigint lhs, bigint rhs)
			=> OurBigIntMath.And(lhs, rhs);

		public static bigint operator >>(bigint lhs, int rhs)
			=> OurBigIntMath.RightShift(lhs, rhs);

		public static bigint operator <<(bigint lhs, int rhs)
			=> OurBigIntMath.LeftShift(lhs, rhs);

		public static bigint operator &(ulong lhs, bigint rhs)
			=> OurBigIntMath.And(new bigint(lhs), rhs);

		public static bigint operator &(bigint lhs, ulong rhs)
			=> OurBigIntMath.And(lhs, new bigint(rhs));

		public static bigint operator |(bigint lhs, bigint rhs)
			=> OurBigIntMath.Or(lhs, rhs);

		public static bigint operator |(ulong lhs, bigint rhs)
			=> OurBigIntMath.Or(new bigint(lhs), rhs);

		public static bigint operator |(bigint lhs, ulong rhs)
			=> OurBigIntMath.Or(lhs, new bigint(rhs));

		public static bigint operator ^(bigint lhs, bigint rhs)
			=> OurBigIntMath.Xor(lhs, rhs);

		public static bigint operator ^(ulong lhs, bigint rhs)
			=> OurBigIntMath.Xor(new bigint(lhs), rhs);

		public static bigint operator ^(bigint lhs, ulong rhs)
			=> OurBigIntMath.Xor(lhs, new bigint(rhs));

		#endregion bitwise

		#endregion operators

		public static bool Mod(bigint lhs, bigint rhs, bigint modul)
			=> lhs % modul == rhs % modul;

		public bigint Clone()
		{
			bigint result = new bigint
			{
				previousBlock = this.previousBlock,
				digit = new digit(new bool[digit.RADIX]),
			};

			for (int i = 0; i < digit.RADIX; i++)
			{
				digit.Value[i] = this.digit.Value[i];
			}

			return result;
		}

		public bigint DeepClone(bool hasTrim = true)
		{
			bigint result = new bigint();
			bigint thiscopy = this;
			bigint tmp = result;

			while (thiscopy != null)
			{
				digit digit = new digit(new bool[digit.RADIX]);

				for (int i = 0; i < digit.RADIX; i++)
				{
					digit.Value[i] = thiscopy.digit.Value[i];
				}

				tmp.digit = digit;

				thiscopy = thiscopy.previousBlock;

				if ((thiscopy != null) && (tmp.previousBlock == null))
				{
					OurBigIntMathHelper.AddNewPreviousBlock(result, new bool[digit.RADIX]);
				}

				tmp = tmp.previousBlock;
			}

			if (hasTrim)
			{
				OurBigIntMathHelper.TrimStructure(ref result);
			}

			return result;
		}

		public static bigint Parse(string input)
		{
			// TODO

			return new bigint();
		}
	}
}