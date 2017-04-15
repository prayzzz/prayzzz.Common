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

        public ErrorResult(Result result) : base(result)
        {
            if (result.ErrorType == ErrorType.None)
            {
                ErrorType = ErrorType.Unknown;
            }
        }

        public ErrorResult(ErrorType errorType, Result result) : base(result)
        {
            ErrorType = errorType;
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

        public ErrorResult(Result result) : base(result)
        {
            if (result.ErrorType == ErrorType.None)
            {
                ErrorType = ErrorType.Unknown;
            }
        }

        public ErrorResult(ErrorType errorType, Result result) : base(result)
        {
            ErrorType = errorType;
        }
    }
}