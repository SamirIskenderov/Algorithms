using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OurBigRat
{
	public class OurBigInt : IComparable
	{
		internal const int BOOL_ARRAY_SIZE = 32;

		public bool[] value;

		internal OurBigInt previousBlock;

		#region ctor

		public OurBigInt()
		{
			this.value = new bool[BOOL_ARRAY_SIZE];
		}

		internal OurBigInt(IEnumerable<bool> v)
		{
			if (v == null)
			{
				this.value = new bool[BOOL_ARRAY_SIZE];
			}

			this.value = v.ToArray();
		}

		internal OurBigInt Clone()
		{
			OurBigInt result = new OurBigInt
			{
				previousBlock = this.previousBlock,
			};

			bool[] arr = new bool[OurBigInt.BOOL_ARRAY_SIZE];

			for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
			{
				arr[i] = this.value[i];
			}

			result.value = arr;

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var item in this.value.Reverse())
			{
				sb.Append(item ? '1' : '0');
			}

			return sb.ToString();
		}

		internal OurBigInt DeepClone()
		{
			OurBigInt result = new OurBigInt();

			OurBigInt tmp = result;

			bool[] arr = new bool[OurBigInt.BOOL_ARRAY_SIZE];

			while (tmp != null)
			{
				for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
				{
					arr[i] = this.value[i];
				}

				result.value = arr;

				tmp = tmp.previousBlock;
				result = OurBigIntMathHelper.AddNewPreviousBlock(tmp, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
			}

			return result;
		}

		public static OurBigInt Parse(string input)
		{
			// TODO

			return new OurBigInt();
		}

		public OurBigInt(ulong v) : this()
		{
			int i = 0;

			OurBigInt current = this;

			foreach (var item in OurBigIntMathHelper.GetNextBit(v))
			{
				current.value[i] = item;
				i++;

				if (i == OurBigInt.BOOL_ARRAY_SIZE)
				{
					i = 0;

					current.previousBlock = new OurBigInt();

					current = current.previousBlock;
				}
			}
		}

		#endregion ctor

		#region operators

		#region unary

		public static OurBigInt operator --(OurBigInt lhs)
			=> OurBigIntMath.Subtract(lhs, new OurBigInt(1));

		public static OurBigInt operator ++(OurBigInt lhs)
			=> OurBigIntMath.Add(lhs, new OurBigInt(1));

		public static OurBigInt operator !(OurBigInt lhs)
			=> OurBigIntMath.Not(lhs);

		#endregion unary

		#region binary

		public static OurBigInt operator +(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Add(lhs, rhs);

		public static OurBigInt operator +(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Add(new OurBigInt(lhs), rhs);

		public static OurBigInt operator +(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Add(lhs, new OurBigInt(rhs));

		public static OurBigInt operator -(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Subtract(lhs, rhs);

		public static OurBigInt operator -(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Subtract(new OurBigInt(lhs), rhs);

		public static OurBigInt operator -(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Subtract(lhs, new OurBigInt(rhs));

		public static OurBigInt operator *(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Multiple(lhs, rhs);

		public static OurBigInt operator *(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Multiple(new OurBigInt(lhs), rhs);

		public static OurBigInt operator *(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Multiple(lhs, new OurBigInt(rhs));

		public static OurBigInt operator /(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Divide(lhs, rhs);

		public static OurBigInt operator /(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Divide(new OurBigInt(lhs), rhs);

		public static OurBigInt operator /(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Divide(lhs, new OurBigInt(rhs));

		#endregion binary

		#region eq

		public static bool operator ==(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Equally(lhs, rhs);

		public static bool operator ==(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Equally(new OurBigInt(lhs), rhs);

		public static bool operator ==(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Equally(lhs, new OurBigInt(rhs));

		public static bool operator !=(OurBigInt lhs, OurBigInt rhs)
			=> !(lhs == rhs);

		public static bool operator !=(ulong lhs, OurBigInt rhs)
			=> !(lhs == rhs);

		public static bool operator !=(OurBigInt lhs, ulong rhs)
			=> !(lhs == rhs);

		public static bool operator <(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.IsLess(lhs, rhs);

		public static bool operator <(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.IsLess(new OurBigInt(lhs), rhs);

		public static bool operator <(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.IsLess(lhs, new OurBigInt(rhs));

		public static bool operator >(OurBigInt lhs, OurBigInt rhs)
			=> !(lhs <= rhs);

		public static bool operator >(ulong lhs, OurBigInt rhs)
			=> !(lhs <= rhs);

		public static bool operator >(OurBigInt lhs, ulong rhs)
			=> !(lhs <= rhs);

		public static bool operator <=(OurBigInt lhs, OurBigInt rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(ulong lhs, OurBigInt rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(OurBigInt lhs, ulong rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator >=(OurBigInt lhs, OurBigInt rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(ulong lhs, OurBigInt rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(OurBigInt lhs, ulong rhs)
			=> lhs == rhs || lhs > rhs;

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			OurBigInt p = obj as OurBigInt;
			if ((object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return this == p;
		}

		public bool Equals(OurBigInt obj)
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

			OurBigInt p = obj as OurBigInt;
			if (p == null)
			{
				return -1; // TODO  to ask
			}

			return this.CompareTo(p);
		}

		public int CompareTo(OurBigInt input)
		{
			if (this == null) // srly?
			{
				throw new NullReferenceException();
			}
			else if (input == null)
			{
				throw new ArgumentNullException();
			}

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

			OurBigInt lhscopy = this;
			OurBigInt rhscopy = input;

			OurBigInt tmp = new OurBigInt();

			while (lhscopy != null)
			{
				for (int i = OurBigInt.BOOL_ARRAY_SIZE - 1; i >= 0; i--)
				{
					if ((lhscopy.value[i] ^ rhscopy.value[i]) && lhscopy.value[i])
					{
						return 1;
					}
					else if ((lhscopy.value[i] ^ rhscopy.value[i]) && lhscopy.value[i])
					{
						return -1;
					}
				}

				lhscopy = lhscopy.previousBlock;
				rhscopy = rhscopy.previousBlock;
			}

			return 0;
		}

		#endregion eq

		#region bitwise

		public static OurBigInt operator &(OurBigInt lhs, OurBigInt rhs)
				=> OurBigIntMath.And(lhs, rhs);

		public static OurBigInt operator >>(OurBigInt lhs, int rhs)
			=> OurBigIntMath.RightShift(lhs, rhs);

		public static OurBigInt operator <<(OurBigInt lhs, int rhs)
			=> OurBigIntMath.LeftShift(lhs, rhs);

		public static OurBigInt operator &(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.And(new OurBigInt(lhs), rhs);

		public static OurBigInt operator &(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.And(lhs, new OurBigInt(rhs));

		public static OurBigInt operator |(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Or(lhs, rhs);

		public static OurBigInt operator |(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Or(new OurBigInt(lhs), rhs);

		public static OurBigInt operator |(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Or(lhs, new OurBigInt(rhs));

		public static OurBigInt operator ^(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.Xor(lhs, rhs);

		public static OurBigInt operator ^(ulong lhs, OurBigInt rhs)
			=> OurBigIntMath.Xor(new OurBigInt(lhs), rhs);

		public static OurBigInt operator ^(OurBigInt lhs, ulong rhs)
			=> OurBigIntMath.Xor(lhs, new OurBigInt(rhs));

		#endregion bitwise

		#endregion operators
	}
}