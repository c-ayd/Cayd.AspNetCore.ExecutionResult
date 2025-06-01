using System;
using System.Collections.Generic;

namespace Cayd.AspNetCore.ExecutionResult
{
    /// <summary>
    /// Defines the result of an execution with success or error.
    /// </summary>
    /// <typeparam name="TValue">The type of the value returned by the execution if successful.</typeparam>
    public class ExecResult<TValue>
    {
        private readonly bool _isSuccess;
        private readonly ExecSuccess<TValue>? _success;
        private readonly ExecError? _error;

        private ExecResult(ExecSuccess<TValue> success)
        {
            _isSuccess = true;
            _success = success;
            _error = null;
        }

        private ExecResult(ExecError error)
        {
            _isSuccess = false;
            _success = null;
            _error = error;
        }

        /// <summary>
        /// Matches the execution result and calls the corresponding method based on success or error.
        /// </summary>
        /// <typeparam name="T">The return type of the match method.</typeparam>
        /// <param name="success">The method to be executed if the execution result is successful.</param>
        /// <param name="error">The method to be executed if the execution result is a failure.</param>
        /// <returns>The return type of the match method.</returns>
        public T Match<T>(Func<int, TValue?, object?, T> success, Func<int, ICollection<ExecErrorDetail>, object?, T> error)
            => _isSuccess ? success(_success!.SuccessCode, _success.Value, _success.Metadata) : error(_error!.ErrorCode, _error.Details, _error.Metadata);
    }
}
