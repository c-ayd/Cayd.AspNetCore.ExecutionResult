using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 303.
    /// </summary>
    public class ExecSeeOther : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.SeeOther;

        /// <summary>
        /// Creates a redirection execution result with the status code of 303.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecSeeOther(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
