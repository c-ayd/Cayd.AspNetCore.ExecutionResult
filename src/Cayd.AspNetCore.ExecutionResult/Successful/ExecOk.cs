using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Successful
{
    /// <summary>
    /// Defines a successful execution result with the status code of 200.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution.</typeparam>
    public class ExecOk<TValue> : ExecSuccess<TValue>
    {
        private const int successCode = (int)HttpStatusCode.OK;

        /// <summary>
        /// Creates a successful execution result with the status code of 200.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecOk(object? metadata = null) : base(successCode, metadata)
        {
        }

        /// <summary>
        /// Creates a successful execution result with the status code of 200.
        /// </summary>
        /// <param name="value">The value returned by the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecOk(TValue? value, object? metadata = null) : base(successCode, value, metadata)
        {
        }
    }
}
