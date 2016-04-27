using System.Text;
using System.Text.RegularExpressions;

namespace Algorithms.Library
{
	internal class ISSN
	{
		public int A { get; set; }
		public int B { get; set; }
		public int Control { get; set; }

		public override string ToString()
		{
			return $"ISSN {A.ToString()}-{B.ToString()}{(Control == 10 ? "X" : Control.ToString())}";
		}
	}

	public class IssnGenerator
	{
		internal const string IssnRegex = @"^ISSN\x20(?=.{9}$)\d{4}([- ])\d{3}(\d|X)$";

		public string Generate()
		{
			ISSN issn = new ISSN
			{
				A = Common.rand.Next(1000, 9999),
				B = Common.rand.Next(100, 999),
			};

			StringBuilder sb = new StringBuilder(32);
			sb.Append(issn.ToString());

			// Remove "ISSN " word
			for (int i = 0; i < 5; i++)
			{
				sb[i] = ' ';
			}

			sb.Replace("-", string.Empty).Replace(" ", string.Empty);

			int summ = 0;
			for (int i = 0; i < sb.Length - 1; i++)
			{
				summ += int.Parse(sb[i].ToString()) * (8 - i);
			}

			issn.Control = summ % 11 == 0 ? 0 : 11 - (summ % 11);

			return issn.ToString();
		}

		public bool ValidateIssn(string issn)
		{
			if (issn == null)
			{
				return false;
			}

			////
			if (!Regex.IsMatch(issn, IssnRegex))
			{
				return false;
			}

			////

			// parsing number from issn

			StringBuilder sb = new StringBuilder(32);
			sb.Append(issn.ToString());

			// Remove "ISSN " word
			for (int i = 0; i < 5; i++)
			{
				sb[i] = ' ';
			}

			sb.Replace("-", string.Empty).Replace(" ", string.Empty);

			int summ = 0;
			int digit = 0;
			for (int i = 0; i < sb.Length; i++)
			{
				digit = (sb[i] == 'x') || (sb[i] == 'X') ? 10 : int.Parse(sb[i].ToString());

				summ += digit * (8 - i);
			}

			if (summ % 11 != 0)
			{
				return false;
			}

			return true;
		}
	}
}