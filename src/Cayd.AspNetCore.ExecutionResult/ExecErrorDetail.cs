namespace Cayd.AspNetCore.ExecutionResult
{
    /// <summary>
    /// Represents the detail of an error in a failed execution.
    /// </summary>
    public class ExecErrorDetail
    {
        /// <summary>
        /// A human readable message of the error.
        /// </summary>
        public string? Message { get; private set; }
        /// <summary>
        /// The code of <see cref="Message"/>
        /// </summary>
        public string? MessageCode { get; private set; }

        /// <summary>
        /// Creates an error detail for a failed execution.
        /// </summary>
        /// <param name="message">A human readable message of the error.</param>
        /// <param name="messageCode">The code of <paramref name="message"/></param>
        public ExecErrorDetail(string? message = null, string? messageCode = null)
        {
            Message = message;
            MessageCode = messageCode;
        }
    }
}
