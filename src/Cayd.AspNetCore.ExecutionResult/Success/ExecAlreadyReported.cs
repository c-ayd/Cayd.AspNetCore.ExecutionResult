using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Success
{
    /// <summary>
    /// Defines a successful execution result with the status code of 208.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution.</typeparam>
    public class ExecAlreadyReported<TValue> : ExecSuccess<TValue>
    {
        private const int successCode = (int)HttpStatusCode.AlreadyReported;

        /// <summary>
        /// Creates a successful execution result with the status code of 208.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecAlreadyReported(object? metadata = null) : base(successCode, metadata)
        {
        }

        /// <summary>
        /// Creates a successful execution result with the status code of 208.
        /// </summary>
        /// <param name="value">The value returned by the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecAlreadyReported(TValue? value, object? metadata = null) : base(successCode, value, metadata)
        {
        }
    }
}
