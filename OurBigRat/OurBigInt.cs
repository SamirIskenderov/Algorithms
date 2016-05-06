using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OurBigRat
{
	public class OurBigInt : IComparable, IEnumerable<bool>
	{
		internal const int BOOL_ARRAY_SIZE = 32;

		internal bool[] value;

		internal OurBigInt previousBlock;

		#region ctor

		public OurBigInt()
		{
			this.value = new bool[BOOL_ARRAY_SIZE];
		}

		internal OurBigInt(IEnumerable<bool> v) : this()
		{
			if (v == null)
			{
				throw new ArgumentNullException();
			}

			int i = 0;

			OurBigInt current = this;

			foreach (var item in v)
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

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			byte a = 0;

			OurBigInt tmp = this;

			while (tmp != null)
			{
				sb.Append("[ ");

				foreach (var item in tmp.value.Reverse())
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

		public static bool operator ==(ulong lhs, OurBigInt rhs)
			=> new OurBigInt(lhs) == rhs;

		public static bool operator ==(OurBigInt lhs, ulong rhs)
			=> lhs == new OurBigInt(rhs);

		public static bool operator !=(OurBigInt lhs, OurBigInt rhs)
			=> !(lhs == rhs);

		public static bool operator !=(ulong lhs, OurBigInt rhs)
			=> !(lhs == rhs);

		public static bool operator !=(OurBigInt lhs, ulong rhs)
			=> !(lhs == rhs);

		public static bool operator <(OurBigInt lhs, OurBigInt rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) < 0;
		}

		public static bool operator <(ulong lhs, OurBigInt rhs)
			=> new OurBigInt(lhs) < rhs;

		public static bool operator <(OurBigInt lhs, ulong rhs)
			=> lhs < new OurBigInt(rhs);

		public static bool operator >(OurBigInt lhs, OurBigInt rhs)
		{
			if ((object)lhs == null)
			{
				return false;
			}

			return lhs.CompareTo(rhs) > 0;
		}

		public static bool operator >(ulong lhs, OurBigInt rhs)
			=> new OurBigInt(lhs) > rhs;

		public static bool operator >(OurBigInt lhs, ulong rhs)
			=> lhs > new OurBigInt(rhs);

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
			if ((object)input == null)
			{
				return 1;
			}

			OurBigInt thiscopy = this;

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

			OurBigInt lhscopy = this.Clone();
			OurBigInt rhscopy = input.Clone();

			OurBigInt lhscopyParent = lhscopy;
			OurBigInt rhscopyParent = rhscopy;

			OurBigInt tmp = new OurBigInt();

			if (lhsBlockCount != 1)
			{
				while (lhscopy.previousBlock.previousBlock != null)
				{
					lhscopy = lhscopy.previousBlock;
					rhscopy = rhscopy.previousBlock;
				}

				for (int i = OurBigInt.BOOL_ARRAY_SIZE - 1; i >= 0; i--)
				{
					if ((lhscopy.previousBlock.value[i] ^ rhscopy.previousBlock.value[i]) && lhscopy.previousBlock.value[i])
					{
						return 1;
					}
					else if ((lhscopy.previousBlock.value[i] ^ rhscopy.previousBlock.value[i]) && rhscopy.previousBlock.value[i])
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
				for (int i = OurBigInt.BOOL_ARRAY_SIZE - 1; i >= 0; i--)
				{
					if ((lhscopy.value[i] ^ rhscopy.value[i]) && lhscopy.value[i])
					{
						return 1;
					}
					else if ((lhscopy.value[i] ^ rhscopy.value[i]) && rhscopy.value[i])
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

			OurBigInt current = this;

			while (current != null)
			{
				for (int i = 0; i < current.value.Length; i++)
				{
					yield return current.value[i];
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

		public OurBigInt Clone()
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

		public OurBigInt DeepClone()
		{
			OurBigInt result = new OurBigInt();
			OurBigInt thiscopy = this;
			OurBigInt tmp = result;


			while (thiscopy != null)
			{
				bool[] arr = new bool[OurBigInt.BOOL_ARRAY_SIZE];

				for (int i = 0; i < OurBigInt.BOOL_ARRAY_SIZE; i++)
				{
					arr[i] = thiscopy.value[i];
				}

				tmp.value = arr;

				thiscopy = thiscopy.previousBlock;

				if ((thiscopy != null) && (result.previousBlock == null))
				{
					result = OurBigIntMathHelper.AddNewPreviousBlock(tmp, new bool[OurBigInt.BOOL_ARRAY_SIZE]);
				}

				tmp = tmp.previousBlock;
			}

			OurBigIntMathHelper.TrimStructure(ref result);

			return result;
		}

		public static OurBigInt Parse(string input)
		{
			// TODO

			return new OurBigInt();
		}
	}
}