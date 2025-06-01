namespace Cayd.AspNetCore.ExecutionResult
{
    /// <summary>
    /// Represents the base class of a successful execution result.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution.</typeparam>
    public class ExecSuccess<TValue>
    {
        /// <summary>
        /// The HTTP code representing the result of the execution.
        /// </summary>
        public int SuccessCode { get; private set; }
        /// <summary>
        /// The value returned by the execution.
        /// </summary>
        public TValue? Value { get; private set; }
        /// <summary>
        /// Extra metadata of the execution.
        /// </summary>
        public object? Metadata { get; private set; }

        /// <summary>
        /// Creates a successful execution result.
        /// </summary>
        /// <param name="successCode">The HTTP code of the execution.</param>
        /// <param name="value">The value returned by the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecSuccess(int successCode, TValue? value, object? metadata = null)
        {
            SuccessCode = successCode;
            Value = value;
            Metadata = metadata;
        }

        /// <summary>
        /// Creates a successful execution result.
        /// </summary>
        /// <param name="successCode">The HTTP code of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecSuccess(int successCode, object? metadata = null)
        {
            SuccessCode = successCode;
            Value = default;
            Metadata = metadata;
        }
    }
}
