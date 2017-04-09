using System;

namespace prayzzz.Common.Results
{
    public class ErrorResult<TData> : Result<TData>
    {
        public ErrorResult(string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.Unknown;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ErrorType errorType, string message, params object[] messageArgs)
        {
            ErrorType = errorType;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(Exception exception, string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.InternalError;
            Exception = exception;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ValidationResult result)
        {
            ErrorType = ErrorType.ValidationError;
            Message = result.ToString();
        }

        /// <summary>
        ///     Creates a new <see cref="ErrorResult" /> from the given result
        /// </summary>
        public ErrorResult(Result result)
            : base(result)
        {
        }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.Unknown;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ErrorType errorType, string message, params object[] messageArgs)
        {
            ErrorType = errorType;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(Exception exception, string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.InternalError;
            Exception = exception;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ValidationResult result)
        {
            ErrorType = ErrorType.ValidationError;
            Message = result.ToString();
        }

        /// <summary>
        ///     Creates a new <see cref="ErrorResult" /> from the given result
        /// </summary>
        public ErrorResult(Result result) : base(result)
        {
        }
    }
}