using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 308.
    /// </summary>
    public class ExecPermanentRedirection : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.PermanentRedirect;

        /// <summary>
        /// Creates a redirection execution result with the status code of 308.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPermanentRedirection(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
