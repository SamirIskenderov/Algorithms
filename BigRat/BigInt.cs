namespace BigRat
{
	using System;
	using System.Collections.Generic;
	using bigint = BigInt;

	public class BigInt
	{
		private static BigIntMath math = BigIntMath.Instance;
		private bigint previousBlock;
		private uint value;

		internal static bigint One { get; } = new bigint(1);
		internal static bigint Zero { get; } = new bigint(0);

		#region ctor

		public BigInt(bigint b)
		{
			this.value = b.value;
			this.previousBlock = b.previousBlock;
		}

		public BigInt(uint v)
		{
			this.value = v;
		}

		public BigInt()
		{
		}

		#endregion ctor

		#region operators

		#region unary

		public static bigint operator --(bigint lhs)
			=> math.Subtract(lhs, bigint.One);

		public static bigint operator ++(bigint lhs)
			=> math.Add(lhs, One);

		#endregion unary

		#region binary

		public static bigint operator -(bigint lhs, bigint rhs)
			=> math.Subtract(lhs, rhs);

		public static bigint operator -(uint lhs, bigint rhs)
			=> math.Subtract(new bigint(lhs), rhs);

		public static bigint operator -(bigint lhs, uint rhs)
			=> math.Subtract(lhs, new bigint(rhs));

		public static bigint operator %(bigint lhs, bigint rhs)
			=> math.Reminder(lhs, rhs);

		public static bigint operator %(uint lhs, bigint rhs)
			=> math.Reminder(new bigint(lhs), rhs);

		public static bigint operator %(bigint lhs, uint rhs)
			=> math.Reminder(lhs, new bigint(rhs));

		public static bigint operator *(bigint lhs, bigint rhs)
			=> math.Multiple(lhs, rhs);

		public static bigint operator *(uint lhs, bigint rhs)
			=> math.Multiple(new bigint(lhs), rhs);

		public static bigint operator *(bigint lhs, uint rhs)
			=> math.Multiple(lhs, new bigint(rhs));

		public static bigint operator /(bigint lhs, bigint rhs)
			=> math.Divide(lhs, rhs);

		public static bigint operator /(uint lhs, bigint rhs)
			=> math.Divide(new bigint(lhs), rhs);

		public static bigint operator /(bigint lhs, uint rhs)
			=> math.Divide(lhs, new bigint(rhs));

		public static bigint operator +(bigint lhs, bigint rhs)
																											=> math.Add(lhs, rhs);

		public static bigint operator +(uint lhs, bigint rhs)
			=> math.Add(new bigint(lhs), rhs);

		public static bigint operator +(bigint lhs, uint rhs)
			=> math.Add(lhs, new bigint(rhs));

		#endregion binary

		#region eq

		public static bool operator !=(bigint lhs, bigint rhs)
			=> !(lhs == rhs);

		public static bool operator !=(uint lhs, bigint rhs)
			=> !(lhs == rhs);

		public static bool operator !=(bigint lhs, uint rhs)
			=> !(lhs == rhs);

		public static bool operator <(bigint lhs, bigint rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <(uint lhs, bigint rhs)
			=> new bigint(lhs) < rhs;

		public static bool operator <(bigint lhs, uint rhs)
			=> lhs < new bigint(rhs);

		public static bool operator <=(bigint lhs, bigint rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(uint lhs, bigint rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(bigint lhs, uint rhs)
			=> lhs == rhs || lhs < rhs;

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

		public static bool operator ==(uint lhs, bigint rhs)
			=> new bigint(lhs) == rhs;

		public static bool operator ==(bigint lhs, uint rhs)
			=> lhs == new bigint(rhs);

		public static bool operator >(bigint lhs, bigint rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >(uint lhs, bigint rhs)
			=> new bigint(lhs) > rhs;

		public static bool operator >(bigint lhs, uint rhs)
			=> lhs > new bigint(rhs);

		public static bool operator >=(bigint lhs, bigint rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(uint lhs, bigint rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(bigint lhs, uint rhs)
			=> lhs == rhs || lhs > rhs;

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
			throw new NotImplementedException();
		}

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
			throw new NotImplementedException();
		}

		#endregion eq

		#endregion operators
	}
}