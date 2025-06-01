using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 307.
    /// </summary>
    public class ExecTemporaryRedirection : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.TemporaryRedirect;

        /// <summary>
        /// Creates a redirection execution result with the status code of 307.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecTemporaryRedirection(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
