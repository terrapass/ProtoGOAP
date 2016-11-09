using System;

namespace ProtoGOAP.Graphs
{
	/// <summary>
	/// Thrown by IPathfinder implementers, when they fail to find a path on a graph.
	/// </summary>
	[Serializable]
	public class PathNotFoundException : Exception
	{
		private const string DEFAULT_MESSAGE = "Failed to find a path";

		// TODO: Provide some properties for this exception,
		// so that pathfinder could give the details of the failure.

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNotFoundException"/> class
		/// </summary>
		public PathNotFoundException() : base(DEFAULT_MESSAGE)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProtoGOAP.Graphs.PathNotFoundException"/> class.
		/// </summary>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		public PathNotFoundException(Exception inner) : base(DEFAULT_MESSAGE, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNotFoundException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public PathNotFoundException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNotFoundException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. </param>
		public PathNotFoundException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNotFoundException"/> class
		/// </summary>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <param name="info">The object that holds the serialized object data.</param>
		protected PathNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}
	}
}

