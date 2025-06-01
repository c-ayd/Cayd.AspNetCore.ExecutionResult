using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 304.
    /// </summary>
    public class ExecNotModified : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.NotModified;

        /// <summary>
        /// Creates a redirection execution result with the status code of 304.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecNotModified(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
