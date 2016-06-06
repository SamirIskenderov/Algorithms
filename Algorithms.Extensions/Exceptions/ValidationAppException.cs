using System;
using System.Runtime.Serialization;

namespace Algorithms.Extensions.Exceptions
{
	[Serializable]
	public class ValidationAppException : ApplicationException
	{
		public ValidationAppException()
		{
		}

		public ValidationAppException(string message) : base(message)
		{
		}

		public ValidationAppException(string message, Exception inner) : base(message, inner)
		{
		}

		protected ValidationAppException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}