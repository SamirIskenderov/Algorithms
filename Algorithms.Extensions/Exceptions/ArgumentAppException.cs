using System;
using System.Runtime.Serialization;

namespace Algorithms.Extensions.Exceptions
{
	[Serializable]
	public class ArgumentAppException : ApplicationException
	{
		public ArgumentAppException()
		{
		}

		public ArgumentAppException(string message, string paramName) : base(string.Join(" parametr name: ", message, paramName))
		{
		}

		public ArgumentAppException(string message) : base(message)
		{
		}

		public ArgumentAppException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ArgumentAppException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}