using System;

namespace Algorithms.Library
{
	internal static class Common
	{
		internal static readonly Random rand = new Random();
	}

	internal class User
	{
		internal int Age { get; set; }
		internal string Name { get; set; }
		internal string Surname { get; set; }
	}
}