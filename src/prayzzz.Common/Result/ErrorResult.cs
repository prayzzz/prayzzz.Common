using System;

namespace prayzzz.Common.Result
{
    public class ErrorResult<TData> : Result<TData>
    {
        public ErrorResult(string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            Message = message;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ErrorType errorType, string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            ErrorType = errorType;
            MessageArgs = messageArgs;
        }

        public ErrorResult(Exception exception, string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            Exception = exception;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ValidationResult result)
            : base(result.ToString())
        {
            IsSuccess = false;
            ErrorType = ErrorType.ValidationError;
        }

        /// <summary>
        ///     Creates a new <see cref="ErrorResult" /> with the data from the given result
        /// </summary>
        public ErrorResult(Result result)
            : base(result)
        {
        }
    }

    public class ErrorResult : Result
    {
        public ErrorResult(string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ErrorType errorType, string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            ErrorType = errorType;
            MessageArgs = messageArgs;
        }

        public ErrorResult(Exception exception, string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = false;
            Exception = exception;
            MessageArgs = messageArgs;
        }

        public ErrorResult(ValidationResult result)
            : base(result.ToString())
        {
            IsSuccess = false;
            ErrorType = ErrorType.ValidationError;
        }
        
        /// <summary>
        ///     Creates a new <see cref="ErrorResult" /> with the data from the given result
        /// </summary>
        public ErrorResult(Result result)
            : base(result)
        {
        }
    }
}