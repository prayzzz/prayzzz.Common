using System;

namespace prayzzz.Common.Result
{
    public class ErrorResult<TData> : Result<TData>
    {
        public ErrorResult(string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            ErrorMessageArgs = errorMessageArgs;
        }

        public ErrorResult(ErrorType errorType, string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            ErrorType = errorType;
            ErrorMessageArgs = errorMessageArgs;
        }

        public ErrorResult(Exception exception, string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            Exception = exception;
            ErrorMessageArgs = errorMessageArgs;
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
        public ErrorResult(string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            ErrorMessageArgs = errorMessageArgs;
        }

        public ErrorResult(ErrorType errorType, string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            ErrorType = errorType;
            ErrorMessageArgs = errorMessageArgs;
        }

        public ErrorResult(Exception exception, string errorMessage, params object[] errorMessageArgs)
            : base(errorMessage)
        {
            IsSuccess = false;
            Exception = exception;
            ErrorMessageArgs = errorMessageArgs;
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