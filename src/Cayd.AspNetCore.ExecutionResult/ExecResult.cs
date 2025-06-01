using Cayd.AspNetCore.ExecutionResult.ClientError;
using Cayd.AspNetCore.ExecutionResult.Success;
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
        private readonly EResult _result;
        private readonly ExecSuccess<TValue>? _success;
        private readonly ExecRedirection? _redirection;
        private readonly ExecError? _error;

        private ExecResult(ExecSuccess<TValue> success)
        {
            _result = EResult.Success;
            _success = success;
            _redirection = null;
            _error = null;
        }

        private ExecResult(ExecRedirection redirection)
        {
            _result = EResult.Redirection;
            _success = null;
            _redirection = redirection;
            _error = null;
        }

        private ExecResult(ExecError error)
        {
            _result = EResult.Error;
            _success = null;
            _redirection = null;
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
            => _result == EResult.Success ? success(_success!.SuccessCode, _success.Value, _success.Metadata) : error(_error!.ErrorCode, _error.Details, _error.Metadata);

        /// <summary>
        /// Matches the execution result and calls the corresponding method based on success or error.
        /// </summary>
        /// <typeparam name="T">The return type of the match method.</typeparam>
        /// <param name="redirection">The method to be executed if the execution result is redirected.</param>
        /// <param name="error">The method to be executed if the execution result is a failure.</param>
        /// <returns>The return type of the match method.</returns>
        public T Match<T>(Func<int, object?, T> redirection, Func<int, ICollection<ExecErrorDetail>, object?, T> error)
            => _result == EResult.Redirection ? redirection(_redirection!.RedirectionCode, _redirection.Metadata) : error(_error!.ErrorCode, _error.Details, _error.Metadata);


        public static implicit operator ExecResult<TValue>(ExecSuccess<TValue> success)
            => new ExecResult<TValue>(success);
        public static implicit operator ExecResult<TValue>(TValue value)
            => new ExecResult<TValue>(new ExecOk<TValue>(value));

        public static implicit operator ExecResult<TValue>(ExecRedirection redirection)
            => new ExecResult<TValue>(redirection);

        public static implicit operator ExecResult<TValue>(ExecError error)
            => new ExecResult<TValue>(error);
        public static implicit operator ExecResult<TValue>(List<ExecErrorDetail> errorDetails)
            => new ExecResult<TValue>(new ExecBadRequest(errorDetails));
        public static implicit operator ExecResult<TValue>(ExecErrorDetail[] errorDetails)
            => new ExecResult<TValue>(new ExecBadRequest(errorDetails));
        public static implicit operator ExecResult<TValue>(ExecErrorDetail errorDetail)
            => new ExecResult<TValue>(new ExecBadRequest(errorDetail));

        private enum EResult
        {
            Success         =   0,
            Redirection     =   1,
            Error           =   2,
        }
    }
}
