using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 301.
    /// </summary>
    public class ExecMovedPermanently : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.MovedPermanently;

        /// <summary>
        /// Creates a redirection execution result with the status code of 301.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecMovedPermanently(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
