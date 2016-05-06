namespace OurBigRat
{
	public class OurBigRat
	{
		private OurBigInt up;

		private OurBigInt down;

		private bool isPositive;

		#region operators

		#region ctor

		public OurBigRat()
		{
			this.up = new OurBigInt();
			this.down = new OurBigInt();
			this.isPositive = true;
		}

		public OurBigRat(ulong v)
		{
			this.up = new OurBigInt(v);
			this.down = new OurBigInt(1);
			this.isPositive = true;
		}

		#endregion ctor

		#region unary

		public static OurBigRat operator +(OurBigRat lhs)
			=> OurBigRatMath.Abs(lhs);

		public static OurBigRat operator -(OurBigRat lhs)
			=> OurBigRatMath.Invert(lhs);

		public static OurBigRat operator --(OurBigRat lhs)
			=> OurBigRatMath.Subtract(lhs, new OurBigRat(1));

		public static OurBigRat operator ++(OurBigRat lhs)
			=> OurBigRatMath.Add(lhs, new OurBigRat(1));

		public static OurBigRat operator !(OurBigRat lhs)
			=> OurBigRatMath.Not(lhs);

		#endregion unary

		#region binary

		public static OurBigRat operator +(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Add(lhs, rhs);

		public static OurBigRat operator +(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Add(new OurBigRat(lhs), rhs);

		public static OurBigRat operator +(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Add(lhs, new OurBigRat(rhs));

		public static OurBigRat operator -(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Subtract(lhs, rhs);

		public static OurBigRat operator -(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Subtract(new OurBigRat(lhs), rhs);

		public static OurBigRat operator -(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Subtract(lhs, new OurBigRat(rhs));

		public static OurBigRat operator *(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Multiple(lhs, rhs);

		public static OurBigRat operator *(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Multiple(new OurBigRat(lhs), rhs);

		public static OurBigRat operator *(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Multiple(lhs, new OurBigRat(rhs));

		public static OurBigRat operator /(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Divide(lhs, rhs);

		public static OurBigRat operator /(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Divide(new OurBigRat(lhs), rhs);

		public static OurBigRat operator /(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Divide(lhs, new OurBigRat(rhs));

		#endregion binary

		#region eq

		public static bool operator ==(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Equally(lhs, rhs);

		public static bool operator ==(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Equally(new OurBigRat(lhs), rhs);

		public static bool operator ==(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Equally(lhs, new OurBigRat(rhs));

		public static bool operator !=(OurBigRat lhs, OurBigRat rhs)
			=> !(lhs == rhs);

		public static bool operator !=(ulong lhs, OurBigRat rhs)
			=> !(lhs == rhs);

		public static bool operator !=(OurBigRat lhs, ulong rhs)
			=> !(lhs == rhs);

		public static bool operator <(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.IsLess(lhs, rhs);

		public static bool operator <(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.IsLess(new OurBigRat(lhs), rhs);

		public static bool operator <(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.IsLess(lhs, new OurBigRat(rhs));

		public static bool operator >(OurBigRat lhs, OurBigRat rhs)
			=> !(lhs < rhs);

		public static bool operator >(ulong lhs, OurBigRat rhs)
			=> !(lhs < rhs);

		public static bool operator >(OurBigRat lhs, ulong rhs)
			=> !(lhs < rhs);

		public static bool operator <=(OurBigRat lhs, OurBigRat rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(ulong lhs, OurBigRat rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator <=(OurBigRat lhs, ulong rhs)
			=> lhs == rhs || lhs < rhs;

		public static bool operator >=(OurBigRat lhs, OurBigRat rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(ulong lhs, OurBigRat rhs)
			=> lhs == rhs || lhs > rhs;

		public static bool operator >=(OurBigRat lhs, ulong rhs)
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

		public static OurBigRat operator &(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.And(lhs, rhs);

		public static OurBigRat operator &(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.And(new OurBigRat(lhs), rhs);

		public static OurBigRat operator &(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.And(lhs, new OurBigRat(rhs));

		public static OurBigRat operator |(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Or(lhs, rhs);

		public static OurBigRat operator |(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Or(new OurBigRat(lhs), rhs);

		public static OurBigRat operator |(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Or(lhs, new OurBigRat(rhs));

		public static OurBigRat operator ^(OurBigRat lhs, OurBigRat rhs)
			=> OurBigRatMath.Xor(lhs, rhs);

		public static OurBigRat operator ^(ulong lhs, OurBigRat rhs)
			=> OurBigRatMath.Xor(new OurBigRat(lhs), rhs);

		public static OurBigRat operator ^(OurBigRat lhs, ulong rhs)
			=> OurBigRatMath.Xor(lhs, new OurBigRat(rhs));

		#endregion bitwise

		#endregion operators
	}
}