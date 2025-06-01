using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 302.
    /// </summary>
    public class ExecFound : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.Found;

        /// <summary>
        /// Creates a redirection execution result with the status code of 302.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecFound(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
