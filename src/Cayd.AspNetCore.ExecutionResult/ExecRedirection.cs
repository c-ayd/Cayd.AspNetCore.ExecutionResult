namespace Cayd.AspNetCore.ExecutionResult
{
    /// <summary>
    /// Represents the base class of a redirection execution result.
    /// </summary>
    public class ExecRedirection
    {
        /// <summary>
        /// The HTTP code representing the result of the execution.
        /// </summary>
        public int RedirectionCode { get; private set; }
        /// <summary>
        /// Extra metadata of the execution.
        /// </summary>
        public object? Metadata { get; private set; }

        /// <summary>
        /// Creates a redirection execution result.
        /// </summary>
        /// <param name="redirectionCode">The HTTP code of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecRedirection(int redirectionCode, object? metadata = null)
        {
            RedirectionCode = redirectionCode;
            Metadata = metadata;
        }
    }
}
