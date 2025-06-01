using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Successful
{
    /// <summary>
    /// Defines a successful execution result with the status code of 206.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution.</typeparam>
    public class ExecPartialContent<TValue> : ExecSuccess<TValue>
    {
        private const int successCode = (int)HttpStatusCode.PartialContent;

        /// <summary>
        /// Creates a successful execution result with the status code of 206.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPartialContent(object? metadata = null) : base(successCode, metadata)
        {
        }

        /// <summary>
        /// Creates a successful execution result with the status code of 206.
        /// </summary>
        /// <param name="value">The value returned by the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPartialContent(TValue? value, object? metadata = null) : base(successCode, value, metadata)
        {
        }
    }
}
