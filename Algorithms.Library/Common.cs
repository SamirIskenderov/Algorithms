﻿using System;

namespace Algorithms.Library
{
    internal static class Common
    {
        internal static readonly Random Rand = new Random();
    }

    /// <summary>
    /// I need simple user so many times...
    /// </summary>
    internal class User
    {
        internal int Age { get; set; }
        internal string Name { get; set; }
        internal string Surname { get; set; }
    }
}