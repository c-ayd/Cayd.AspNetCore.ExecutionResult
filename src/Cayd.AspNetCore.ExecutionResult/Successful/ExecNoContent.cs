using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Successful
{
    /// <summary>
    /// Defines a successful execution result with the status code of 204.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution.</typeparam>
    public class ExecNoContent<TValue> : ExecSuccess<TValue>
    {
        private const int successCode = (int)HttpStatusCode.NoContent;

        /// <summary>
        /// Creates a successful execution result with the status code of 204.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecNoContent(object? metadata = null) : base(successCode, metadata)
        {
        }

        /// <summary>
        /// Creates a successful execution result with the status code of 204.
        /// </summary>
        /// <param name="value">The value returned by the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecNoContent(TValue? value, object? metadata = null) : base(successCode, value, metadata)
        {
        }
    }
}
