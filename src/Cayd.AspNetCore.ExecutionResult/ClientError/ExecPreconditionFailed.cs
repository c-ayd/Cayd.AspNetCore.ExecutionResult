using System.Collections.Generic;
using System.Net;

namespace Cayd.AspNetCore.ExecutionResult.ClientError
{
    /// <summary>
    /// Defines a failed execution result with the status code of 412.
    /// </summary>
    public class ExecPreconditionFailed : ExecError
    {
        private const int errorCode = (int)HttpStatusCode.PreconditionFailed;

        /// <summary>
        /// Creates a failed execution result with the status code of 412.
        /// </summary>
        /// <param name="details">The details of the errors of the exeucution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPreconditionFailed(ICollection<ExecErrorDetail> details, object? metadata = null) : base(errorCode, details, metadata)
        {
        }

        /// <summary>
        /// Creates a failed execution result with the status code of 412.
        /// </summary>
        /// <param name="detail">The detail of the error of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPreconditionFailed(ExecErrorDetail detail, object? metadata = null) : base(errorCode, detail, metadata)
        {
        }

        /// <summary>
        /// Creates a failed execution result with the status code of 412.
        /// </summary>
        /// <param name="message">The message of the error of the execution.</param>
        /// <param name="messageCode">The message code of the error of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecPreconditionFailed(string? message = null, string? messageCode = null, object? metadata = null) : base(errorCode, message, messageCode, metadata)
        {
        }
    }
}
