using System.Collections.Generic;

namespace Cayd.AspNetCore.ExecutionResult
{
    /// <summary>
    /// Represents the base class of a failed execution result.
    /// </summary>
    public class ExecError
    {
        /// <summary>
        /// The HTTP code representing the result of the execution.
        /// </summary>
        public int ErrorCode { get; private set; }
        /// <summary>
        /// The details of the errors of the exeucution.
        /// </summary>
        public ICollection<ExecErrorDetail> Details { get; private set; }
        /// <summary>
        /// Extra metadata of the execution.
        /// </summary>
        public object? Metadata { get; private set; }

        /// <summary>
        /// Creates a failed execution result.
        /// </summary>
        /// <param name="errorCode">The HTTP code of the execution.</param>
        /// <param name="details">The details of the errors of the exeucution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecError(int errorCode, ICollection<ExecErrorDetail> details, object? metadata = null)
        {
            ErrorCode = errorCode;
            Details = details;
            Metadata = metadata;
        }

        /// <summary>
        /// Creates a failed execution result.
        /// </summary>
        /// <param name="errorCode">The HTTP code of the execution.</param>
        /// <param name="detail">The detail of the error of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecError(int errorCode, ExecErrorDetail detail, object? metadata = null)
        {
            ErrorCode = errorCode;
            Details = new List<ExecErrorDetail>() { detail };
            Metadata = metadata;
        }

        /// <summary>
        /// Creates a failed execution result.
        /// </summary>
        /// <param name="errorCode">The HTTP code of the execution.</param>
        /// <param name="message">The message of the error of the execution.</param>
        /// <param name="messageCode">The message code of the error of the execution.</param>
        /// <param name="metadata">Extra metadata of the execution.</param>
        public ExecError(int errorCode, string? message = null, string? messageCode = null, object? metadata = null)
        {
            ErrorCode = errorCode;
            Details = new List<ExecErrorDetail>()
            {
                new ExecErrorDetail(message, messageCode)
            };
            Metadata = metadata;
        }
    }
}
