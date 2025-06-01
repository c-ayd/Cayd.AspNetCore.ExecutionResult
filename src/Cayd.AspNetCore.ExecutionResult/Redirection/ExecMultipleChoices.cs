using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.Redirection
{
    /// <summary>
    /// Defines a redirection execution result with the status code of 300.
    /// </summary>
    public class ExecMultipleChoices : ExecRedirection
    {
        private const int redirectionCode = (int)HttpStatusCode.MultipleChoices;

        /// <summary>
        /// Creates a redirection execution result with the status code of 300.
        /// </summary>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecMultipleChoices(object? metadata = null) : base(redirectionCode, metadata)
        {
        }
    }
}
