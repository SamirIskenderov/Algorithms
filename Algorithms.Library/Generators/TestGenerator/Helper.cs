using System.Collections.Generic;

namespace Algorithms.Library
{
	public static class Helper
	{
		public static MethodParamPair MethodParamPair { get; } = new MethodParamPair
		{
			Pairs = new Dictionary<string, List<MethodTestCases>>
			{
				["this.newspaperLogic.Add"] = new List<MethodTestCases>
		{
		    new MethodTestCases
		    {
			IsCorrectTest = false,
			ExpectedResponse = "ValidateNullException",
			ExpectedResult = ExpectedResult.Exception,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "Null",
				},
			    }
			}
		    },
		    new MethodTestCases
		    {
			IsCorrectTest = true,
			ExpectedResponse = string.Empty,
			ExpectedResult = ExpectedResult.Void,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "Punctuation",
				    "MixedLanguageName",
				    "CyrillicName",
				    "LatinName",
				    "StrangeSymbols",
				},
				["Issn"] = new List<string>
				{
				    "UsualType",
				    "Null",
				},
			    }
			}
		    },
		    new MethodTestCases
		    {
			IsCorrectTest = false,
			ExpectedResponse = "ValidateException",
			ExpectedResult = ExpectedResult.Exception,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "BigName",
				    "EmptyName",
				    "SpaceName",
				},
				["Issn"] = new List<string>
				{
				    "IncorrectTypeOne",
				    "IncorrectTypeTwo",
				    "IncorrectTypeThree",
				},
			    }
			}
		    },
		},
				["this.newspaperLogic.Update"] = new List<MethodTestCases>
		{
		    new MethodTestCases
		    {
			IsCorrectTest = false,
			ExpectedResponse = "ValidateNullException",
			ExpectedResult = ExpectedResult.Exception,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "Null",
				},
			    }
			}
		    },
		    new MethodTestCases
		    {
			IsCorrectTest = true,
			ExpectedResponse = string.Empty,
			ExpectedResult = ExpectedResult.Void,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "Punctuation",
				    "MixedLanguageName",
				    "CyrillicName",
				    "LatinName",
				    "StrangeSymbols",
				},
				["Issn"] = new List<string>
				{
				    "UsualType",
				    "Null",
				},
			    }
			}
		    },
		    new MethodTestCases
		    {
			IsCorrectTest = false,
			ExpectedResponse = "ValidateException",
			ExpectedResult = ExpectedResult.Exception,
			FieldTestCases = new FieldTestCases
			{
			    Pairs = new Dictionary<string, List<string>>
			    {
				["Title"] = new List<string>
				{
				    "BigName",
				    "EmptyName",
				    "SpaceName",
				},
				["Issn"] = new List<string>
				{
				    "IncorrectTypeOne",
				    "IncorrectTypeTwo",
				    "IncorrectTypeThree",
				},
			    }
			}
		    },
		},
			}
		};
	}
}