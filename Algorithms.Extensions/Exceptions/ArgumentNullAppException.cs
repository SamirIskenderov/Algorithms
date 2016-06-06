using System;
using System.Runtime.Serialization;

namespace Algorithms.Extensions.Exceptions
{
	[Serializable]
	public class ArgumentNullAppException : ApplicationException
	{
		public ArgumentNullAppException()
		{
		}

		public ArgumentNullAppException(string paramName, string message) : base(string.Join(", parametr name: ", message, paramName))
		{
		}

		public ArgumentNullAppException(string message) : base(message)
		{
		}

		public ArgumentNullAppException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ArgumentNullAppException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}