using System;

namespace Algorithms.Library
{
	internal static class Common
	{
		internal static Random Random { get; } = new Random();
		internal static Isbn10Generator Isbn10 { get; } = new Isbn10Generator();
		internal static IssnGenerator Issn { get; } = new IssnGenerator();
		internal static TextGenerator TextGenerator { get; } = new TextGenerator();
	}
}