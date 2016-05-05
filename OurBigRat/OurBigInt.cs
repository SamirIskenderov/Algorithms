namespace OurBigRat
{
	public class OurBigInt
	{
		private bool[] value;

		private OurBigInt previousBlock;

		#region ctor

		public OurBigInt()
		{
			this.value = new bool[32];
		}

		public OurBigInt(ulong v) : this()
		{
			int i = 0;

			foreach (var item in OurBigIntMathHelper.GetNextBit(v))
			{
				this.value[i] = item;
				i++;
			}
		}

		#endregion ctor

		#region operators

		#region unary

		public static OurBigInt operator +(OurBigInt lhs)
			=> OurBigIntMath.Abs(lhs);

		public static OurBigInt operator -(OurBigInt lhs)
			=> OurBigIntMath.Invert(lhs);

		public static OurBigInt operator --(OurBigInt lhs)
			=> OurBigIntMath.Subtract(lhs, new OurBigInt(1));

		public static OurBigInt operator ++(OurBigInt lhs)
			=> OurBigIntMath.Add(lhs, new OurBigInt(1));

		public static OurBigInt operator !(OurBigInt lhs)
			=> OurBigIntMath.Not(lhs);

		public static OurBigInt operator ~(OurBigInt lhs)
			=> OurBigIntMath.Complement(lhs);

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
			=> !(lhs < rhs);

		public static bool operator >(ulong lhs, OurBigInt rhs)
			=> !(lhs < rhs);

		public static bool operator >(OurBigInt lhs, ulong rhs)
			=> !(lhs < rhs);

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
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#endregion eq

		#region bitwise

		public static OurBigInt operator &(OurBigInt lhs, OurBigInt rhs)
			=> OurBigIntMath.And(lhs, rhs);

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