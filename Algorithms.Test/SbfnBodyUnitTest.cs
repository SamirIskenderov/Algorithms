using Algorithms.BigNumber;

using System;
using Xunit;

namespace Algorithms.Test
{
	public class BigNumberDSUnitTest
	{
		private BigNumberDS zero { get; } = BigNumberDS.Create("0");

		#region ctor

		public void CorrectCtorMustNotThrowExc()
		{
			BigNumberDS num1 = BigNumberDS.Create();
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,123")]
		[InlineData("-1,123123456789")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,123")]
		[InlineData("-12,123123456789")]
		[InlineData("-123456789123456789123")]
		[InlineData("-123456789123456789123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("-123456789123456789123,123123456789")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,123")]
		[InlineData("0,123123456789")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,123")]
		[InlineData("1,123123456789")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,123")]
		[InlineData("12,123123456789")]
		[InlineData("123456789123456789123")]
		[InlineData("123456789123456789123,0")]
		[InlineData("123456789123456789123,123")]
		[InlineData("123456789123456789123,123123456789")]
		public void CorrectCtorMustNotThrowExc(string str)
		{
			BigNumberDS num1 = BigNumberDS.Create(str);

			if (str.Contains("-"))
			{
				Assert.True(num1 < zero);
			}
			else
			{
				Assert.True(num1 > zero);
			}

			if (str.Contains(","))
			{
				Assert.True(num1.ToString().Contains(","));
			}
			else
			{
				Assert.True(!num1.ToString().Contains(","));
			}
		}

		#endregion ctor

		[Fact]
		public void BigNumberWithNullsAmidMustNotThrowItOut()
		{
			BigNumberDS obj = BigNumberDS.Create("10000000000000000000000000000000000000000000000000000000000000001,100000000000000000000000000000000000000000001");

			Assert.Equal("10000000000000000000000000000000000000000000000000000000000000001,100000000000000000000000000000000000000000001", obj.ToString());
		}

		#region operators

		#region unary+

		[Theory]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,123")]
		[InlineData("0,123456212789")]
		[InlineData("0,123456789123456789")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,123")]
		[InlineData("1,12345612121789")]
		[InlineData("1,123456789101001123456789")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,123")]
		[InlineData("12,123450425476789")]
		[InlineData("12,123456789123456789")]
		[InlineData("1212345678")]
		[InlineData("1212345678,0")]
		[InlineData("1230145678,123")]
		[InlineData("123456078,123")]
		[InlineData("12345678")]
		[InlineData("12345678,0")]
		[InlineData("12345678,12344010156789")]
		[InlineData("12345678,1234560424789")]
		[InlineData("12345678,1234567891101023456789")]
		[InlineData("12345678,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789")]
		[InlineData("123456789123456789110123,123")]
		[InlineData("123456789123456789123")]
		[InlineData("123456789123456789123,0")]
		public void OperatorUnapyPlusMustWorkCorrectly(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(+lhs, rhs);
		}

		#endregion unary+

		#region unary-

		[Theory]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,123")]
		[InlineData("0,123456212789")]
		[InlineData("0,123456789123456789")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,123")]
		[InlineData("1,12345612121789")]
		[InlineData("1,123456789101001123456789")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,123")]
		[InlineData("12,123450425476789")]
		[InlineData("12,123456789123456789")]
		[InlineData("1212345678")]
		[InlineData("1212345678,0")]
		[InlineData("1230145678,123")]
		[InlineData("123456078,123")]
		[InlineData("12345678")]
		[InlineData("12345678,0")]
		[InlineData("12345678,12344010156789")]
		[InlineData("12345678,1234560424789")]
		[InlineData("12345678,1234567891101023456789")]
		[InlineData("12345678,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789")]
		[InlineData("123456789123456789110123,123")]
		[InlineData("123456789123456789123")]
		[InlineData("123456789123456789123,0")]
		public void OperatorUnapyMinusMustWorkCorrectlyVer1(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create("-" + one);

			Assert.Equal<BigNumberDS>(rhs, -lhs);
		}

		#endregion unary-

		#region pre/postfix+

		[Theory]
		[InlineData("0", "1")]
		[InlineData("0,0", "1,0")]
		[InlineData("0,123", "1,123")]
		[InlineData("0,123456212789", "1,123456212789")]
		[InlineData("0,123456789123456789", "1,123456789123456789")]
		[InlineData("1", "2")]
		[InlineData("1,0", "2,0")]
		[InlineData("1,123", "2,123")]
		[InlineData("1,12345612121789", "2,12345612121789")]
		[InlineData("1,123456789101001123456789", "2,123456789101001123456789")]
		[InlineData("12", "13")]
		[InlineData("12,0", "13,0")]
		[InlineData("12,123", "13,123")]
		[InlineData("12,123450425476789", "13,123450425476789")]
		[InlineData("12,123456789123456789", "13,123456789123456789")]
		[InlineData("1212345678", "1212345679")]
		[InlineData("1212345678,0", "1212345679,0")]
		[InlineData("1230145678,123", "1230145679,123")]
		[InlineData("123456078,123", "123456079,123")]
		[InlineData("12345678", "12345679")]
		[InlineData("12345678,0", "12345679,0")]
		[InlineData("12345678,12344010156789", "12345679,12344010156789")]
		[InlineData("12345678,1234560424789", "12345679,1234560424789")]
		[InlineData("12345678,1234567891101023456789", "12345679,1234567891101023456789")]
		[InlineData("12345678,123456789123456789", "12345679,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1234567891200103456789124,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1234567891234501010426789124,123456789")]
		[InlineData("123456789123456789110123,123", "123456789123456789110124,123")]
		[InlineData("123456789123456789123", "123456789123456789124")]
		[InlineData("123456789123456789123,0", "123456789123456789124,0")]
		public void OperatorPostfixPlusMustWorkCorrectly(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);
			lhs++;
			Assert.Equal<BigNumberDS>(rhs, lhs);
		}


		[Theory]
		[InlineData("0", "1")]
		[InlineData("0,0", "1,0")]
		[InlineData("0,123", "1,123")]
		[InlineData("0,123456212789", "1,123456212789")]
		[InlineData("0,123456789123456789", "1,123456789123456789")]
		[InlineData("1", "2")]
		[InlineData("1,0", "2,0")]
		[InlineData("1,123", "2,123")]
		[InlineData("1,12345612121789", "2,12345612121789")]
		[InlineData("1,123456789101001123456789", "2,123456789101001123456789")]
		[InlineData("12", "13")]
		[InlineData("12,0", "13,0")]
		[InlineData("12,123", "13,123")]
		[InlineData("12,123450425476789", "13,123450425476789")]
		[InlineData("12,123456789123456789", "13,123456789123456789")]
		[InlineData("1212345678", "1212345679")]
		[InlineData("1212345678,0", "1212345679,0")]
		[InlineData("1230145678,123", "1230145679,123")]
		[InlineData("123456078,123", "123456079,123")]
		[InlineData("12345678", "12345679")]
		[InlineData("12345678,0", "12345679,0")]
		[InlineData("12345678,12344010156789", "12345679,12344010156789")]
		[InlineData("12345678,1234560424789", "12345679,1234560424789")]
		[InlineData("12345678,1234567891101023456789", "12345679,1234567891101023456789")]
		[InlineData("12345678,123456789123456789", "12345679,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1234567891200103456789124,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1234567891234501010426789124,123456789")]
		[InlineData("123456789123456789110123,123", "123456789123456789110124,123")]
		[InlineData("123456789123456789123", "123456789123456789124")]
		[InlineData("123456789123456789123,0", "123456789123456789124,0")]
		public void OperatorPrefixPlusMustWorkCorrectly(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);
			++lhs;
			Assert.Equal<BigNumberDS>(rhs, lhs);
		}

		#endregion pre/postfix+

		#region pre/postfix-


		public void OperatorPostfixMinusMustWorkCorrectlyVer0(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);
			lhs--;
			Assert.Equal<BigNumberDS>(rhs, lhs);
		}

		[Theory]
		[InlineData("0", "-1")]
		[InlineData("0,0", "-1,0")]
		[InlineData("0,123", "-0,877")]
		[InlineData("0,123456212789", "-0,876543787211")]
		[InlineData("0,123456789123456789", "-0,876543210876543211")]
		[InlineData("1", "0")]
		[InlineData("1,0", "0")]
		[InlineData("1,123", "0,123")]
		[InlineData("1,12345612121789", "0,12345612121789")]
		[InlineData("1,123456789101001123456789", "0,123456789101001123456789")]
		[InlineData("12", "11")]
		[InlineData("12,0", "11,0")]
		[InlineData("12,123", "11,123")]
		[InlineData("12,123450425476789", "11,123450425476789")]
		[InlineData("12,123456789123456789", "11,123456789123456789")]
		[InlineData("1212345678", "1212345677")]
		[InlineData("1212345678,0", "1212345677,0")]
		[InlineData("1230145678,123", "1230145677,123")]
		[InlineData("123456078,123", "123456077,123")]
		[InlineData("12345678", "12345677")]
		[InlineData("12345678,0", "12345677,0")]
		[InlineData("12345678,12344010156789", "12345677,12344010156789")]
		[InlineData("12345678,1234560424789", "12345677,1234560424789")]
		[InlineData("12345678,1234567891101023456789", "12345677,1234567891101023456789")]
		[InlineData("12345678,123456789123456789", "12345677,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1234567891200103456789122,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1234567891234501010426789122,123456789")]
		[InlineData("123456789123456789110123,123", "123456789123456789110122,123")]
		[InlineData("123456789123456789123", "123456789123456789122")]
		[InlineData("123456789123456789123,0", "123456789123456789122,0")]
		public void OperatorPrefixMinusMustWorkCorrectlyVer0(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);
			--lhs;
			Assert.Equal<BigNumberDS>(rhs, lhs);
		}

		#endregion pre/postfix-

		#region multiplicative+

		[Theory]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,123", "0,24416")]
		[InlineData("0,123456212789", "0,900000000", "1,02341242456789")]
		[InlineData("0,123456789123456789", "0,12345678109123456789", "0,24691357824601913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,12013", "2,246")]
		[InlineData("1,12345612121789", "1,924210100000000", "3,023456789")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789", "2,246913010578246913578")]
		[InlineData("12", "0", "412")]
		[InlineData("12,0", "0,0", "412,0")]
		[InlineData("12,123", "0,000123", "12,246")]
		[InlineData("12,123450425476789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,12345106789123456789", "12,246913578246913578")]
		[InlineData("1212345678", "123456780", "135802458")]
		[InlineData("1212345678,0", "123456780,0", "135802458,0")]
		[InlineData("1230145678,123", "12345101678,123", "24691356,246")]
		[InlineData("123456078,123", "12310456780,123", "135802458,246")]
		[InlineData("12345678", "1234561078", "2445691356")]
		[InlineData("12345678,0", "1234561078,0", "2445691356,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000", "1101035802459,023456789")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000", "24691010357,023456789")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789", "24691356,4212246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789", "135802458,246913578246913578")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789", "12345678912345101042426789124,246913578246913578")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000", "12345678914212123456789124,023456789")]
		[InlineData("123456789123456789110123,123", "1,000000123", "123456789123000456789124,246")]
		[InlineData("123456789123456789123", "1", "12345678912345678912124")]
		[InlineData("123456789123456789123,0", "1,0", "12345678912345678912124,0")]
		public void OperatorMultiplicativePlusMustBeAssociative(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>((lhs + mhs) + rhs, rhs + (mhs + lhs));
		}

		[Theory]
		[InlineData("0", "0")]
		[InlineData("0,0", "0")]
		[InlineData("0,123", "0")]
		[InlineData("0,123456789", "0")]
		[InlineData("0,123456789123456789", "0")]
		[InlineData("1", "1")]
		[InlineData("1,0", "1")]
		[InlineData("1,123", "1")]
		[InlineData("1,123456789", "1")]
		[InlineData("1,123456789123456789", "1")]
		[InlineData("12", "0")]
		[InlineData("12,0", "0")]
		[InlineData("12,123", "0")]
		[InlineData("12,123456789", "0")]
		[InlineData("12,123456789123456789", "0")]
		[InlineData("12345678", "12345678")]
		[InlineData("12345678", "123456780")]
		[InlineData("12345678,0", "12345678")]
		[InlineData("12345678,0", "123456780")]
		[InlineData("12345678,123", "12345678")]
		[InlineData("12345678,123", "123456780")]
		[InlineData("12345678,123456789", "12345678")]
		[InlineData("12345678,123456789", "123456780")]
		[InlineData("12345678,123456789123456789", "12345678")]
		[InlineData("12345678,123456789123456789", "123456780")]
		[InlineData("123456789123456789123", "1")]
		[InlineData("123456789123456789123,0", "1")]
		[InlineData("123456789123456789123,123", "1")]
		[InlineData("123456789123456789123,123456789", "1")]
		[InlineData("123456789123456789123,123456789123456789", "1")]
		public void OperatorMultiplicativePlusMustBeCommutative(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);

			Assert.Equal<BigNumberDS>(lhs + rhs, rhs + lhs);
		}

		[Theory]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,123", "0,24416")]
		[InlineData("0,123456212789", "0,900000000", "1,02341242456789")]
		[InlineData("0,123456789123456789", "0,12345678109123456789", "0,24691357824601913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,12013", "2,246")]
		[InlineData("1,12345612121789", "1,924210100000000", "3,023456789")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789", "2,246913010578246913578")]
		[InlineData("12", "0", "412")]
		[InlineData("12,0", "0,0", "412,0")]
		[InlineData("12,123", "0,000123", "12,246")]
		[InlineData("12,123450425476789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,12345106789123456789", "12,246913578246913578")]
		[InlineData("1212345678", "123456780", "135802458")]
		[InlineData("1212345678,0", "123456780,0", "135802458,0")]
		[InlineData("1230145678,123", "12345101678,123", "24691356,246")]
		[InlineData("123456078,123", "12310456780,123", "135802458,246")]
		[InlineData("12345678", "1234561078", "2445691356")]
		[InlineData("12345678,0", "1234561078,0", "2445691356,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000", "1101035802459,023456789")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000", "24691010357,023456789")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789", "24691356,4212246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789", "135802458,246913578246913578")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789", "12345678912345101042426789124,246913578246913578")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000", "12345678914212123456789124,023456789")]
		[InlineData("123456789123456789110123,123", "1,000000123", "123456789123000456789124,246")]
		[InlineData("123456789123456789123", "1", "12345678912345678912124")]
		[InlineData("123456789123456789123,0", "1,0", "12345678912345678912124,0")]
		public void OperatorMultiplicativePlusMustBeDistributive(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(mhs * (lhs + rhs), mhs * lhs + mhs * rhs);
		}

		[Theory]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,123", "0,246")]
		[InlineData("0,123456789", "0,900000000", "1,023456789")]
		[InlineData("0,123456789123456789", "0,123456789123456789", "0,246913578246913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,123", "2,246")]
		[InlineData("1,123456789", "1,900000000", "3,023456789")]
		[InlineData("1,123456789123456789", "1,123456789123456789", "2,246913578246913578")]
		[InlineData("12", "0", "12")]
		[InlineData("12,0", "0,0", "12,0")]
		[InlineData("12,123", "0,123", "12,246")]
		[InlineData("12,123456789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,123456789123456789", "12,246913578246913578")]
		[InlineData("12345678", "12345678", "24691356")]
		[InlineData("12345678", "123456780", "135802458")]
		[InlineData("12345678,0", "12345678,0", "24691356,0")]
		[InlineData("12345678,0", "123456780,0", "135802458,0")]
		[InlineData("12345678,123", "12345678,123", "24691356,246")]
		[InlineData("12345678,123", "123456780,123", "135802458,246")]
		[InlineData("12345678,123456789", "12345678,900000000", "24691357,023456789")]
		[InlineData("12345678,123456789", "123456780,900000000", "135802459,023456789")]
		[InlineData("12345678,123456789123456789", "12345678,123456789123456789", "24691356,246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456789123456789", "135802458,246913578246913578")]
		[InlineData("123456789123456789123", "1", "123456789123456789124")]
		[InlineData("123456789123456789123,0", "1,0", "123456789123456789124,0")]
		[InlineData("123456789123456789123,123", "1,123", "123456789123456789124,246")]
		[InlineData("123456789123456789123,123456789", "1,900000000", "123456789123456789125,023456789")]
		[InlineData("123456789123456789123,123456789123456789", "1,123456789123456789", "123456789123456789124,246913578246913578")]
		public void OperatorMultiplicativePlusMustWorkCorrectlyVer0(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS added = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(rhs, lhs + added);
		}

		[Theory]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,123")]
		[InlineData("0,123456789")]
		[InlineData("0,123456789123456789")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,123")]
		[InlineData("1,123456789")]
		[InlineData("1,123456789123456789")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,123")]
		[InlineData("12,123456789")]
		[InlineData("12,123456789123456789")]
		[InlineData("12345678")]
		[InlineData("12345678")]
		[InlineData("12345678,0")]
		[InlineData("12345678,0")]
		[InlineData("12345678,123")]
		[InlineData("12345678,123")]
		[InlineData("12345678,123456789")]
		[InlineData("12345678,123456789")]
		[InlineData("12345678,123456789123456789")]
		[InlineData("12345678,123456789123456789")]
		[InlineData("123456789123456789123")]
		[InlineData("123456789123456789123,0")]
		[InlineData("123456789123456789123,123")]
		[InlineData("123456789123456789123,123456789")]
		[InlineData("123456789123456789123,123456789123456789")]
		public void OperatorMultiplicativePlusMustWorkWithNullCorrectly(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(lhs, lhs + 0);
		}

		#endregion multiplicative+

		#region multiplicative-

		[Theory]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,123", "0,24416")]
		[InlineData("0,123456212789", "0,900000000", "1,02341242456789")]
		[InlineData("0,123456789123456789", "0,12345678109123456789", "0,24691357824601913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,12013", "2,246")]
		[InlineData("1,12345612121789", "1,924210100000000", "3,023456789")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789", "2,246913010578246913578")]
		[InlineData("12", "0", "412")]
		[InlineData("12,0", "0,0", "412,0")]
		[InlineData("12,123", "0,000123", "12,246")]
		[InlineData("12,123450425476789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,12345106789123456789", "12,246913578246913578")]
		[InlineData("1212345678", "123456780", "135802458")]
		[InlineData("1212345678,0", "123456780,0", "135802458,0")]
		[InlineData("1230145678,123", "12345101678,123", "24691356,246")]
		[InlineData("123456078,123", "12310456780,123", "135802458,246")]
		[InlineData("12345678", "1234561078", "2445691356")]
		[InlineData("12345678,0", "1234561078,0", "2445691356,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000", "1101035802459,023456789")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000", "24691010357,023456789")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789", "24691356,4212246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789", "135802458,246913578246913578")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789", "12345678912345101042426789124,246913578246913578")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000", "12345678914212123456789124,023456789")]
		[InlineData("123456789123456789110123,123", "1,000000123", "123456789123000456789124,246")]
		[InlineData("123456789123456789123", "1", "12345678912345678912124")]
		[InlineData("123456789123456789123,0", "1,0", "12345678912345678912124,0")]
		public void OperatorMultiplicativeMinusMustBeAbsAssociative(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(((lhs - mhs) - rhs).Abs(), (lhs - (mhs + rhs)).Abs());
		}

		[Theory]
		[InlineData("0", "0")]
		[InlineData("0,0", "0,0")]
		[InlineData("0,123", "0,123")]
		[InlineData("0,123456212789", "0,900000000")]
		[InlineData("0,123456789123456789", "0,12345678109123456789")]
		[InlineData("1", "1")]
		[InlineData("1,0", "1,0")]
		[InlineData("1,123", "1,12013")]
		[InlineData("1,12345612121789", "1,924210100000000")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789")]
		[InlineData("12", "0")]
		[InlineData("12,0", "0,0")]
		[InlineData("12,123", "0,000123")]
		[InlineData("12,123450425476789", "0,900000000")]
		[InlineData("12,123456789123456789", "0,12345106789123456789")]
		[InlineData("1212345678", "123456780")]
		[InlineData("1212345678,0", "123456780,0")]
		[InlineData("1230145678,123", "12345101678,123")]
		[InlineData("123456078,123", "12310456780,123")]
		[InlineData("12345678", "1234561078")]
		[InlineData("12345678,0", "1234561078,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000")]
		[InlineData("123456789123456789110123,123", "1,000000123")]
		[InlineData("123456789123456789123", "1")]
		[InlineData("123456789123456789123,0", "1,0")]
		public void OperatorMultiplicativeMinusMustBeAbsCommutative(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = /*            */ BigNumberDS.Create(two);

			Assert.Equal<BigNumberDS>((lhs - rhs).Abs(), (rhs - lhs).Abs());
		}

		[Theory]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,123", "0,24416")]
		[InlineData("0,123456212789", "0,900000000", "1,02341242456789")]
		[InlineData("0,123456789123456789", "0,12345678109123456789", "0,24691357824601913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,12013", "2,246")]
		[InlineData("1,12345612121789", "1,924210100000000", "3,023456789")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789", "2,246913010578246913578")]
		[InlineData("12", "0", "412")]
		[InlineData("12,0", "0,0", "412,0")]
		[InlineData("12,123", "0,000123", "12,246")]
		[InlineData("12,123450425476789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,12345106789123456789", "12,246913578246913578")]
		[InlineData("1212345678", "123456780", "135802458")]
		[InlineData("1212345678,0", "123456780,0", "135802458,0")]
		[InlineData("1230145678,123", "12345101678,123", "24691356,246")]
		[InlineData("123456078,123", "12310456780,123", "135802458,246")]
		[InlineData("12345678", "1234561078", "2445691356")]
		[InlineData("12345678,0", "1234561078,0", "2445691356,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000", "1101035802459,023456789")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000", "24691010357,023456789")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789", "24691356,4212246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789", "135802458,246913578246913578")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789", "12345678912345101042426789124,246913578246913578")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000", "12345678914212123456789124,023456789")]
		[InlineData("123456789123456789110123,123", "1,000000123", "123456789123000456789124,246")]
		[InlineData("123456789123456789123", "1", "12345678912345678912124")]
		[InlineData("123456789123456789123,0", "1,0", "12345678912345678912124,0")]
		public void OperatorMultiplicativeMinusMustBeDistributive(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(mhs * (lhs - rhs), mhs * lhs - mhs * rhs);
		}

		[Theory]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,123")]
		[InlineData("0,123456212789")]
		[InlineData("0,123456789123456789")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,123")]
		[InlineData("1,12345612121789")]
		[InlineData("1,123456789101001123456789")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,123")]
		[InlineData("12,123450425476789")]
		[InlineData("12,123456789123456789")]
		[InlineData("1212345678")]
		[InlineData("1212345678,0")]
		[InlineData("1230145678,123")]
		[InlineData("123456078,123")]
		[InlineData("12345678")]
		[InlineData("12345678,0")]
		[InlineData("12345678,12344010156789")]
		[InlineData("12345678,1234560424789")]
		[InlineData("12345678,1234567891101023456789")]
		[InlineData("12345678,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789")]
		[InlineData("123456789123456789110123,123")]
		[InlineData("123456789123456789123")]
		[InlineData("123456789123456789123,0")]
		public void OperatorMultiplicativeMinusMustBeInverseElementCorrect(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(lhs + lhs, -(-lhs) + -(-lhs));
		}

		[Theory]
		[InlineData("0,123", "0,123", "0,24416")]
		[InlineData("0,123456212789", "0,900000000", "1,02341242456789")]
		[InlineData("0,123456789123456789", "0,12345678109123456789", "0,24691357824601913578")]
		[InlineData("1", "1", "2")]
		[InlineData("1,0", "1,0", "2,0")]
		[InlineData("1,123", "1,12013", "2,246")]
		[InlineData("1,12345612121789", "1,924210100000000", "3,023456789")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789", "2,246913010578246913578")]
		[InlineData("12", "0", "412")]
		[InlineData("12,0", "0,0", "412,0")]
		[InlineData("12,123", "0,000123", "12,246")]
		[InlineData("12,123450425476789", "0,900000000", "13,023456789")]
		[InlineData("12,123456789123456789", "0,12345106789123456789", "12,246913578246913578")]
		[InlineData("1212345678", "123456780", "135802458")]
		[InlineData("1212345678,0", "123456780,0", "135802458,0")]
		[InlineData("1230145678,123", "12345101678,123", "24691356,246")]
		[InlineData("123456078,123", "12310456780,123", "135802458,246")]
		[InlineData("12345678", "1234561078", "2445691356")]
		[InlineData("12345678,0", "1234561078,0", "2445691356,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000", "1101035802459,023456789")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000", "24691010357,023456789")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789", "24691356,4212246913578246913578")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789", "135802458,246913578246913578")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789", "12345678912345101042426789124,246913578246913578")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000", "12345678914212123456789124,023456789")]
		[InlineData("123456789123456789110123,123", "1,000000123", "123456789123000456789124,246")]
		[InlineData("123456789123456789123", "1", "12345678912345678912124")]
		[InlineData("123456789123456789123,0", "1,0", "12345678912345678912124,0")]
		public void OperatorMultiplicativeMinusMustBeNonAssociative(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.NotEqual<BigNumberDS>((lhs - mhs) - rhs, rhs - (mhs - lhs));
		}

		[Theory]
		[InlineData("0,123456212789", "0,900000000")]
		[InlineData("0,123456789123456789", "0,12345678109123456789")]
		[InlineData("1,123", "1,12013")]
		[InlineData("1,12345612121789", "1,924210100000000")]
		[InlineData("1,123456789101001123456789", "1,123456789123456789")]
		[InlineData("12", "0")]
		[InlineData("12,0", "0,0")]
		[InlineData("12,123", "0,000123")]
		[InlineData("12,123450425476789", "0,900000000")]
		[InlineData("12,123456789123456789", "0,12345106789123456789")]
		[InlineData("1212345678", "123456780")]
		[InlineData("1212345678,0", "123456780,0")]
		[InlineData("1230145678,123", "12345101678,123")]
		[InlineData("123456078,123", "12310456780,123")]
		[InlineData("12345678", "1234561078")]
		[InlineData("12345678,0", "1234561078,0")]
		[InlineData("12345678,12344010156789", "123456780,900000000")]
		[InlineData("12345678,1234560424789", "12345678,92410101000000000")]
		[InlineData("12345678,1234567891101023456789", "12345678,11023456789123456789")]
		[InlineData("12345678,123456789123456789", "123456780,123456784429123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1,12345678910123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1,900000000")]
		[InlineData("123456789123456789110123,123", "1,000000123")]
		[InlineData("123456789123456789123", "1")]
		[InlineData("123456789123456789123,0", "1,0")]
		public void OperatorMultiplicativeMinusMustBeNonCommutative(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = /*            */ BigNumberDS.Create(two);

			Assert.NotEqual<BigNumberDS>(lhs - rhs, rhs - lhs);
		}

		[Theory]
		[InlineData("-1", "-1", "0")]
		[InlineData("-1,0", "-1,0", "0,0")]
		[InlineData("-1,123", "-1,135", "0,012")]
		[InlineData("-1,123456789", "-1,301235412", "0,177778623")]
		[InlineData("-12", "0", "-12")]
		[InlineData("-12,0", "0,0", "-12,0")]
		[InlineData("-12,123", "0,135", "-12,258")]
		[InlineData("-12,123456789", "0,301235412", "-12,424692201")]
		[InlineData("-12345678", "12345678", "-24691356")]
		[InlineData("-12345678", "123456780", "-135802458")]
		[InlineData("-12345678,0", "12345678,0", "-24691356,0")]
		[InlineData("-12345678,0", "123456780,0", "-135802458,0")]
		[InlineData("-12345678,123", "12345678,135", "-24691356,258")]
		[InlineData("-12345678,123", "123456780,135", "-135802458,258")]
		[InlineData("-12345678,123456789", "12345678,301235412", "-24691356.424692201")]
		[InlineData("-12345678,123456789", "123456780,301235412", "-135802458,424692201")]
		[InlineData("-123456789123456789123", "1", "-123456789123456789124")]
		[InlineData("-123456789123456789123,0", "1,0", "-123456789123456789124,0")]
		[InlineData("-123456789123456789123,123", "1,135", "-123456789123456789124,258")]
		[InlineData("-123456789123456789123,123456789", "1,301235412", "-123456789123456789124,424692201")]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,135", "-0,012")]
		[InlineData("0,123456789", "0,301235412", "-0,177778623")]
		[InlineData("1", "1", "0")]
		[InlineData("1,0", "1,0", "0,0")]
		[InlineData("1,123", "1,135", "-0,012")]
		[InlineData("1,123456789", "1,301235412", "-0,177778623")]
		[InlineData("12", "0", "12")]
		[InlineData("12,0", "0,0", "12,0")]
		[InlineData("12,123", "0,135", "11,988")]
		[InlineData("12,123456789", "0,301235412", "11,822221377")]
		[InlineData("123456789123456789123", "1", "123456789123456789122")]
		[InlineData("123456789123456789123,0", "1,0", "123456789123456789122,0")]
		[InlineData("123456789123456789123,123", "1,135", "123456789123456789121,988")]
		[InlineData("123456789123456789123,123456789", "1,301235412", "123456789123456789121,822221377")]
		public void OperatorMultiplicativeMinusMustWorkCorrectly(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS added = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(rhs, lhs - added);
		}

		#endregion multiplicative-

		#region binary*

		[Theory]
		[InlineData("-1", "-1", "1")]
		[InlineData("-1,0", "-1,0", "1,0")]
		[InlineData("-1,186723", "-1,3144435", "1,47644440422745")]
		[InlineData("-12", "0", "0")]
		[InlineData("-12,0", "0,0", "0,0")]
		[InlineData("-12,17543423", "0,3434315", "3,818434745")]
		[InlineData("-1230120145678,0", "1234101256780,0", "-1524157010652796840,0")]
		[InlineData("-123012045678,0", "-12345583106780,0", "152415712013652796840,0")]
		[InlineData("-1231201245678,0", "-120120345678,0", "152415761015279684,0")]
		[InlineData("-1234105678", "12345678", "-1524157651204025279684")]
		[InlineData("-12341205678", "-12320145678", "15241576012015279684")]
		[InlineData("-12341205678", "12341201256780", "-15240010157652796840")]
		[InlineData("-12345045678,14523", "-123045456780,315", "1524157120671870912,543448745")]
		[InlineData("-123454245678,12043", "-123456134578,315", "15241574512070687091,0027434345")]
		[InlineData("-1234545678,12043", "1234566784560,345645615", "-1524157120671870912,5484578745")]
		[InlineData("-1234545678,123", "123456422045378,315", "-152415775420687091,002743445")]
		[InlineData("-1234567450401208", "-123456780", "15241576,12453252796840")]
		[InlineData("-12345678,0", "123412047525678,0", "-152412015765279684,0")]
		[InlineData("-123456789112023456789123", "1", "-123456789123456789123")]
		[InlineData("-1234567891234567801049123,0", "1,0", "-1234567891250103456789123,0")]
		[InlineData("-123456789123456789123,123", "1,3143425", "-152415770643487091,00274543645")]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,12013", "0,3120115", "0,038120120745")]
		[InlineData("1", "1", "1")]
		[InlineData("1,0", "1120120,0", "1,0")]
		[InlineData("1,121203", "1,315", "1,476745")]
		[InlineData("12", "0", "0")]
		[InlineData("12", "1", "12")]
		[InlineData("12,0", "0,0", "0,0")]
		[InlineData("12,0", "1101,0", "1212,0")]
		[InlineData("12,1120120123", "143,315", "15,94212,451745")]
		[InlineData("12,112023", "0,315436456105", "3,81648745")]
		[InlineData("12345678912345612120789123", "1", "123456712089123456789123")]
		[InlineData("12345678912345678120109123,0", "1,0", "12345678912312010456789123,0")]
		[InlineData("123456789123456789123,412445345645", "1,315", "16234567769734512677696,90674545564545")]
		public void OperatorMultiplicativeMultipleMustBeAssociative(string one, string two, string three)
		{
			BigNumberDS /* */ lhs = BigNumberDS.Create(one);
			BigNumberDS /*      */ mhs = BigNumberDS.Create(two);
			BigNumberDS /*          */ rhs = BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>((lhs * mhs) * rhs, rhs * (mhs * lhs));
		}

		[Theory]
		[InlineData("-1", "-1")]
		[InlineData("-1,0", "-1,0")]
		[InlineData("-1,186723", "-1,3144435")]
		[InlineData("-12", "0")]
		[InlineData("-12,0", "0,0")]
		[InlineData("-12,17543423", "0,3434315")]
		[InlineData("-1230120145678,0", "1234101256780,0")]
		[InlineData("-123012045678,0", "-12345583106780,0")]
		[InlineData("-1231201245678,0", "-120120345678,0")]
		[InlineData("-1234105678", "12345678")]
		[InlineData("-12341205678", "-12320145678")]
		[InlineData("-12341205678", "12341201256780")]
		[InlineData("-12345045678,14523", "-123045456780,315")]
		[InlineData("-123454245678,12043", "-123456134578,315")]
		[InlineData("-1234545678,12043", "1234566784560,345645615")]
		[InlineData("-1234545678,123", "123456422045378,315")]
		[InlineData("-1234567450401208", "-123456780")]
		[InlineData("-12345678,0", "123412047525678,0")]
		[InlineData("-123456789112023456789123", "1")]
		[InlineData("-1234567891234567801049123,0", "1,0")]
		[InlineData("-123456789123456789123,123", "1,3143425")]
		[InlineData("0", "0")]
		[InlineData("0,0", "0,0")]
		[InlineData("0,12013", "0,3120115")]
		[InlineData("1", "1")]
		[InlineData("1,0", "1120120,0")]
		[InlineData("1,121203", "1,315")]
		[InlineData("12", "0")]
		[InlineData("12", "1")]
		[InlineData("12,0", "0,0")]
		[InlineData("12,0", "1101,0")]
		[InlineData("12,1120120123", "143,315")]
		[InlineData("12,112023", "0,315436456105")]
		[InlineData("12345678912345612120789123", "1")]
		[InlineData("12345678912345678120109123,0", "1,0")]
		[InlineData("123456789123456789123,412445345645", "1,315")]
		public void OperatorMultiplicativeMultipleMustBeCommutative(string one, string two)
		{
			BigNumberDS lhs = /* */ BigNumberDS.Create(one);
			BigNumberDS rhs = /*            */ BigNumberDS.Create(two);

			Assert.Equal<BigNumberDS>(lhs * rhs, rhs * lhs);
		}

		[Theory]
		[InlineData("-1", "-1", "1")]
		[InlineData("-1,0", "-1,0", "1,0")]
		[InlineData("-1,186723", "-1,3144435", "1,47644440422745")]
		[InlineData("-12", "0", "0")]
		[InlineData("-12,0", "0,0", "0,0")]
		[InlineData("-12,17543423", "0,3434315", "3,818434745")]
		[InlineData("-1230120145678,0", "1234101256780,0", "-1524157010652796840,0")]
		[InlineData("-123012045678,0", "-12345583106780,0", "152415712013652796840,0")]
		[InlineData("-1231201245678,0", "-120120345678,0", "152415761015279684,0")]
		[InlineData("-1234105678", "12345678", "-1524157651204025279684")]
		[InlineData("-12341205678", "-12320145678", "15241576012015279684")]
		[InlineData("-12341205678", "12341201256780", "-15240010157652796840")]
		[InlineData("-12345045678,14523", "-123045456780,315", "1524157120671870912,543448745")]
		[InlineData("-123454245678,12043", "-123456134578,315", "15241574512070687091,0027434345")]
		[InlineData("-1234545678,12043", "1234566784560,345645615", "-1524157120671870912,5484578745")]
		[InlineData("-1234545678,123", "123456422045378,315", "-152415775420687091,002743445")]
		[InlineData("-1234567450401208", "-123456780", "15241576,12453252796840")]
		[InlineData("-12345678,0", "123412047525678,0", "-152412015765279684,0")]
		[InlineData("-123456789112023456789123", "1", "-123456789123456789123")]
		[InlineData("-1234567891234567801049123,0", "1,0", "-1234567891250103456789123,0")]
		[InlineData("-123456789123456789123,123", "1,3143425", "-152415770643487091,00274543645")]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,12013", "0,3120115", "0,038120120745")]
		[InlineData("1", "1", "1")]
		[InlineData("1,0", "1120120,0", "1,0")]
		[InlineData("1,121203", "1,315", "1,476745")]
		[InlineData("12", "0", "0")]
		[InlineData("12", "1", "12")]
		[InlineData("12,0", "0,0", "0,0")]
		[InlineData("12,0", "1101,0", "1212,0")]
		[InlineData("12,1120120123", "143,315", "15,94212,451745")]
		[InlineData("12,112023", "0,315436456105", "3,81648745")]
		[InlineData("12345678912345612120789123", "1", "123456712089123456789123")]
		[InlineData("12345678912345678120109123,0", "1,0", "12345678912312010456789123,0")]
		[InlineData("123456789123456789123,412445345645", "1,315", "16234567769734512677696,90674545564545")]
		public void OperatorMultiplicativeMultipleMustBeDistributive(string one, string two, string three)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(two);
			BigNumberDS rhs = /*            */ BigNumberDS.Create(three);

			Assert.Equal<BigNumberDS>(mhs * (lhs + rhs), mhs * lhs + mhs * rhs);
		}

		[Theory]
		[InlineData("-1", "-1", "1")]
		[InlineData("-1,0", "-1,0", "1,0")]
		[InlineData("-1,123", "-1,315", "1,476745")]
		[InlineData("-12", "0", "0")]
		[InlineData("-12,0", "0,0", "0,0")]
		[InlineData("-12,123", "0,315", "-3,818745")]
		[InlineData("-12345678", "-12345678", "152415765279684")]
		[InlineData("-12345678", "-123456780", "1524157652796840")]
		[InlineData("-12345678", "12345678", "-152415765279684")]
		[InlineData("-12345678", "123456780", "-1524157652796840")]
		[InlineData("-12345678,0", "-12345678,0", "152415765279684,0")]
		[InlineData("-12345678,0", "-123456780,0", "1524157652796840,0")]
		[InlineData("-12345678,0", "12345678,0", "-152415765279684,0")]
		[InlineData("-12345678,0", "123456780,0", "-1524157652796840,0")]
		[InlineData("-12345678,123", "-12345678,315", "152415770687091,002745")]
		[InlineData("-12345678,123", "-123456780,315", "1524157671870912,548745")]
		[InlineData("-12345678,123", "12345678,315", "-152415770687091,002745")]
		[InlineData("-12345678,123", "123456780,315", "-1524157671870912,548745")]
		[InlineData("-123456789123456789123", "1", "-123456789123456789123")]
		[InlineData("-123456789123456789123,0", "1,0", "-123456789123456789123,0")]
		[InlineData("-123456789123456789123,123", "1,315", "-152415770687091,002745")]
		[InlineData("0", "0", "0")]
		[InlineData("0,0", "0,0", "0,0")]
		[InlineData("0,123", "0,315", "0,038745")]
		[InlineData("1", "1", "1")]
		[InlineData("1,0", "1,0", "1,0")]
		[InlineData("1,123", "1,315", "1,476745")]
		[InlineData("12", "0", "0")]
		[InlineData("12", "1", "12")]
		[InlineData("12,0", "0,0", "0,0")]
		[InlineData("12,0", "1,0", "12,0")]
		[InlineData("12,123", "0,315", "3,818745")]
		[InlineData("12,123", "1,315", "15,941745")]
		[InlineData("123456789123456789123", "1", "123456789123456789123")]
		[InlineData("123456789123456789123,0", "1,0", "123456789123456789123,0")]
		[InlineData("123456789123456789123,123", "1,315", "162345677697345677696,906745")]
		public void OperatorMultiplicativeMultipleMustWorkCorrectlyVer0(string one, string two, string free)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS added = BigNumberDS.Create(two);
			BigNumberDS rhs = BigNumberDS.Create(free);

			Assert.Equal<BigNumberDS>(rhs, lhs * added);
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]

		public void OperatorMultiplicativeMultipleMustWorkWithMinusOneCorrectly(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(-lhs, lhs * -1);
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]

		public void OperatorMultiplicativeMultipleMustWorkWithNullCorrectly(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(zero, (lhs * 0));
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]

		public void OperatorMultiplicativeMultipleMustWorkWithOneCorrectly(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(lhs, lhs * 1);
		}

		#endregion binary*

		#region binary/

		#endregion binary/

		#region operator==

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]

		public void OperatorEqualatyMustWorkCorrectlyVer0(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal<BigNumberDS>(lhs, rhs);
		}

		#endregion operator==

		#region operator!=

		[Theory]
		[InlineData("-1", "0")]
		[InlineData("-1,186723", "-1,0")]
		[InlineData("-1,3144435", "-1,186723")]
		[InlineData("-12", "-1,3144435")]
		[InlineData("-12,17543423", "-12,0")]
		[InlineData("-120120345678,0", "-12,17543423")]
		[InlineData("-1230120145678,0", "-120120345678,0")]
		[InlineData("-123012045678,0", "-1230120145678,0")]
		[InlineData("-123045456780,315", "-123012045678,0")]
		[InlineData("-1231201245678,0", "-123045456780,315")]
		[InlineData("-12320145678", "-1231201245678,0")]
		[InlineData("-1234105678", "-12320145678")]
		[InlineData("-12341205678", "-1234105678")]
		[InlineData("-12345045678,14523", "-12341205678")]
		[InlineData("-123454245678,12043", "-12345045678,14523")]
		[InlineData("-1234545678,12043", "-123454245678,12043")]
		[InlineData("-1234545678,123", "-1234545678,12043")]
		[InlineData("-12345583106780,0", "-1234545678,123")]
		[InlineData("-123456134578,315", "-12345583106780,0")]
		[InlineData("-1234567450401208", "-123456134578,315")]
		[InlineData("-12345678,0", "-1234567450401208")]
		[InlineData("-123456780", "-12345678,0")]
		[InlineData("-123456789112023456789123", "-123456780")]
		[InlineData("-1234567891234567801049123,0", "-123456789112023456789123")]
		[InlineData("-123456789123456789123,123", "-1234567891234567801049123,0")]
		[InlineData("0", "-123456789123456789123,123")]
		[InlineData("0,12013", "0,0")]
		[InlineData("0,3120115", "0,12013")]
		[InlineData("0,315436456105", "0,3120115")]
		[InlineData("0,3434315", "0,315436456105")]
		[InlineData("1", "0,3434315")]
		[InlineData("1,121203", "1,0")]
		[InlineData("1,3143425", "1,121203")]
		[InlineData("1,315", "1,3143425")]
		[InlineData("1101,0", "1,315")]
		[InlineData("1120120,0", "1101,0")]
		[InlineData("12", "1120120,0")]
		[InlineData("12,1120120123", "12,0")]
		[InlineData("12,112023", "12,1120120123")]
		[InlineData("1234101256780,0", "12,112023")]
		[InlineData("12341201256780", "1234101256780,0")]
		[InlineData("123412047525678,0", "12341201256780")]
		[InlineData("123456422045378,315", "123412047525678,0")]
		[InlineData("1234566784560,345645615", "123456422045378,315")]
		[InlineData("12345678", "1234566784560,345645615")]
		[InlineData("12345678912345612120789123", "12345678")]
		[InlineData("12345678912345678120109123,0", "12345678912345612120789123")]
		[InlineData("123456789123456789123,412445345645", "12345678912345678120109123,0")]
		[InlineData("143,315", "123456789123456789123,412445345645")]

		public void OperatorNonEqualatyMustWorkCorrectlyVer0(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);

			Assert.True(lhs != rhs);
		}

		#endregion operator!=

		#region gethashcode

		[Theory]
		[InlineData("-1", "0")]
		[InlineData("-1,186723", "-1,0")]
		[InlineData("-1,3144435", "-1,186723")]
		[InlineData("-12", "-1,3144435")]
		[InlineData("-12,17543423", "-12,0")]
		[InlineData("-120120345678,0", "-12,17543423")]
		[InlineData("-1230120145678,0", "-120120345678,0")]
		[InlineData("-123012045678,0", "-1230120145678,0")]
		[InlineData("-123045456780,315", "-123012045678,0")]
		[InlineData("-1231201245678,0", "-123045456780,315")]
		[InlineData("-12320145678", "-1231201245678,0")]
		[InlineData("-1234105678", "-12320145678")]
		[InlineData("-12341205678", "-1234105678")]
		[InlineData("-12345045678,14523", "-12341205678")]
		[InlineData("-123454245678,12043", "-12345045678,14523")]
		[InlineData("-1234545678,12043", "-123454245678,12043")]
		[InlineData("-1234545678,123", "-1234545678,12043")]
		[InlineData("-12345583106780,0", "-1234545678,123")]
		[InlineData("-123456134578,315", "-12345583106780,0")]
		[InlineData("-1234567450401208", "-123456134578,315")]
		[InlineData("-12345678,0", "-1234567450401208")]
		[InlineData("-123456780", "-12345678,0")]
		[InlineData("-123456789112023456789123", "-123456780")]
		[InlineData("-1234567891234567801049123,0", "-123456789112023456789123")]
		[InlineData("-123456789123456789123,123", "-1234567891234567801049123,0")]
		[InlineData("0", "-123456789123456789123,123")]
		[InlineData("0,12013", "0,0")]
		[InlineData("0,3120115", "0,12013")]
		[InlineData("0,315436456105", "0,3120115")]
		[InlineData("0,3434315", "0,315436456105")]
		[InlineData("1", "0,3434315")]
		[InlineData("1,121203", "1,0")]
		[InlineData("1,3143425", "1,121203")]
		[InlineData("1,315", "1,3143425")]
		[InlineData("1101,0", "1,315")]
		[InlineData("1120120,0", "1101,0")]
		[InlineData("12", "1120120,0")]
		[InlineData("12,1120120123", "12,0")]
		[InlineData("12,112023", "12,1120120123")]
		[InlineData("1234101256780,0", "12,112023")]
		[InlineData("12341201256780", "1234101256780,0")]
		[InlineData("123412047525678,0", "12341201256780")]
		[InlineData("123456422045378,315", "123412047525678,0")]
		[InlineData("1234566784560,345645615", "123456422045378,315")]
		[InlineData("12345678", "1234566784560,345645615")]
		[InlineData("12345678912345612120789123", "12345678")]
		[InlineData("12345678912345678120109123,0", "12345678912345612120789123")]
		[InlineData("123456789123456789123,412445345645", "12345678912345678120109123,0")]
		[InlineData("143,315", "123456789123456789123,412445345645")]

		public void HashCodesFromDifferentObjectMustNotBeSame(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);

			Assert.NotEqual(lhs.GetHashCode(), rhs.GetHashCode());
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void HashCodesFromEqualsObjectMustBeSame(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal(lhs.GetHashCode(), rhs.GetHashCode());
		}

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void HashCodesFromObjectMustBeSame(string one)
		{
			BigNumberDS obj = BigNumberDS.Create(one);

			Assert.Equal(obj.GetHashCode(), obj.GetHashCode());
		}

		#endregion gethashcode

		#region equals

		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void EqualsMustBeAssociativeVer0(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal(lhs.Equals(mhs) && mhs.Equals(rhs), lhs.Equals(rhs));
		}


		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void EqualsMustBeAssociativeVer1(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS mhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal(lhs.Equals(mhs) && mhs.Equals(rhs), lhs.Equals(rhs));
		}



		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void EqualsMustBeCommutativeVer0(string one)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(one);

			Assert.Equal(lhs.Equals(rhs), rhs.Equals(lhs));
		}


		[Theory]
		[InlineData("-1")]
		[InlineData("-1,0")]
		[InlineData("-1,186723")]
		[InlineData("-1,3144435")]
		[InlineData("-12")]
		[InlineData("-12,0")]
		[InlineData("-12,17543423")]
		[InlineData("-120120345678,0")]
		[InlineData("-1230120145678,0")]
		[InlineData("-123012045678,0")]
		[InlineData("-123045456780,315")]
		[InlineData("-1231201245678,0")]
		[InlineData("-12320145678")]
		[InlineData("-1234105678")]
		[InlineData("-12341205678")]
		[InlineData("-12345045678,14523")]
		[InlineData("-123454245678,12043")]
		[InlineData("-1234545678,12043")]
		[InlineData("-1234545678,123")]
		[InlineData("-12345583106780,0")]
		[InlineData("-123456134578,315")]
		[InlineData("-1234567450401208")]
		[InlineData("-12345678,0")]
		[InlineData("-123456780")]
		[InlineData("-123456789112023456789123")]
		[InlineData("-1234567891234567801049123,0")]
		[InlineData("-123456789123456789123,123")]
		[InlineData("0")]
		[InlineData("0,0")]
		[InlineData("0,12013")]
		[InlineData("0,3120115")]
		[InlineData("0,315436456105")]
		[InlineData("0,3434315")]
		[InlineData("1")]
		[InlineData("1,0")]
		[InlineData("1,121203")]
		[InlineData("1,3143425")]
		[InlineData("1,315")]
		[InlineData("1,315")]
		[InlineData("1101,0")]
		[InlineData("1120120,0")]
		[InlineData("12")]
		[InlineData("12,0")]
		[InlineData("12,1120120123")]
		[InlineData("12,112023")]
		[InlineData("1234101256780,0")]
		[InlineData("12341201256780")]
		[InlineData("123412047525678,0")]
		[InlineData("123456422045378,315")]
		[InlineData("1234566784560,345645615")]
		[InlineData("12345678")]
		[InlineData("12345678912345612120789123")]
		[InlineData("12345678912345678120109123,0")]
		[InlineData("123456789123456789123,412445345645")]
		[InlineData("143,315")]
		public void EqualsToItselfMustReturnTrueVer0(string one)
		{
			BigNumberDS obj = BigNumberDS.Create(one);

			Assert.Equal(true, obj.Equals(obj));
		}

		[Fact]
		public void EqualsToNullMustReturnFalse()
		{
			BigNumberDS obj = BigNumberDS.Create("123");

			Assert.Equal(false, obj.Equals(null));
		}

		#endregion equals

		#region operatorless/greater

		[Theory]
		[InlineData("0", "-1")]
		[InlineData("0,0", "-1,0")]
		[InlineData("0,123", "-1,123")]
		[InlineData("0,123456212789", "-1,123456212789")]
		[InlineData("0,123456789123456789", "-1,123456789123456789")]
		[InlineData("1", "0")]
		[InlineData("1,0", "0")]
		[InlineData("1,123", "0,123")]
		[InlineData("1,12345612121789", "0,12345612121789")]
		[InlineData("1,123456789101001123456789", "0,123456789101001123456789")]
		[InlineData("12", "11")]
		[InlineData("12,0", "11,0")]
		[InlineData("12,123", "11,123")]
		[InlineData("12,123450425476789", "11,123450425476789")]
		[InlineData("12,123456789123456789", "11,123456789123456789")]
		[InlineData("1212345678", "1212345677")]
		[InlineData("1212345678,0", "1212345677,0")]
		[InlineData("1230145678,123", "1230145677,123")]
		[InlineData("123456078,123", "123456077,123")]
		[InlineData("12345678", "12345677")]
		[InlineData("12345678,0", "12345677,0")]
		[InlineData("12345678,12344010156789", "12345677,12344010156789")]
		[InlineData("12345678,1234560424789", "12345677,1234560424789")]
		[InlineData("12345678,1234567891101023456789", "12345677,1234567891101023456789")]
		[InlineData("12345678,123456789123456789", "12345677,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1234567891200103456789122,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1234567891234501010426789122,123456789")]
		[InlineData("123456789123456789110123,123", "123456789123456789110122,123")]
		[InlineData("123456789123456789123", "123456789123456789122")]
		[InlineData("123456789123456789123,0", "123456789123456789122,0")]
		public void OperatorGreaterMustWorkCorrectlyVer0(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);

			Assert.Equal(true, lhs > rhs);
		}

		[Theory]
		[InlineData("0", "1")]
		[InlineData("0,0", "1,0")]
		[InlineData("0,123", "1,123")]
		[InlineData("0,123456212789", "1,123456212789")]
		[InlineData("0,123456789123456789", "1,123456789123456789")]
		[InlineData("1", "2")]
		[InlineData("1,0", "2,0")]
		[InlineData("1,123", "2,123")]
		[InlineData("1,12345612121789", "2,12345612121789")]
		[InlineData("1,123456789101001123456789", "2,123456789101001123456789")]
		[InlineData("12", "13")]
		[InlineData("12,0", "13,0")]
		[InlineData("12,123", "13,123")]
		[InlineData("12,123450425476789", "13,123450425476789")]
		[InlineData("12,123456789123456789", "13,123456789123456789")]
		[InlineData("1212345678", "1212345679")]
		[InlineData("1212345678,0", "1212345679,0")]
		[InlineData("1230145678,123", "1230145679,123")]
		[InlineData("123456078,123", "123456079,123")]
		[InlineData("12345678", "12345679")]
		[InlineData("12345678,0", "12345679,0")]
		[InlineData("12345678,12344010156789", "12345679,12344010156789")]
		[InlineData("12345678,1234560424789", "12345679,1234560424789")]
		[InlineData("12345678,1234567891101023456789", "12345679,1234567891101023456789")]
		[InlineData("12345678,123456789123456789", "12345679,123456789123456789")]
		[InlineData("1234567891200103456789123,123456101789123456789", "1234567891200103456789124,123456101789123456789")]
		[InlineData("1234567891234501010426789123,123456789", "1234567891234501010426789124,123456789")]
		[InlineData("123456789123456789110123,123", "123456789123456789110124,123")]
		[InlineData("123456789123456789123", "123456789123456789124")]
		[InlineData("123456789123456789123,0", "123456789123456789124,0")]
		public void OperatorLessMustWorkCorrectlyVer0(string one, string two)
		{
			BigNumberDS lhs = BigNumberDS.Create(one);
			BigNumberDS rhs = BigNumberDS.Create(two);

			Assert.Equal(true, lhs < rhs);
		}

		#endregion operatorless/greater

		#endregion operators

		//[TestMethod]
		//public void TrimStructurePositiveVer1()
		//{
		//	BigNumberDS obj = BigNumberDS.Create("21002000000000000000000");
		//	BigNumberDSHelper.TrimStructure(ref obj);

		//	Assert.AreEqual("21002000000000000000000", obj.ToString());
		//}
	}
}