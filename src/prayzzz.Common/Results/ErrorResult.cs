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

        /// <inheritdoc />
        /// <summary>
        /// Creates a new ErrorResult from the given <paramref name="result"/>.
        /// If <see cref="T:prayzzz.Common.Results.ErrorType" /> is None, ErrorType.Unknown is used instead.
        /// </summary>
        public ErrorResult(IResult result) : base(result)
        {
            if (result.ErrorType == ErrorType.None)
            {
                ErrorType = ErrorType.Unknown;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new ErrorResult from the given <paramref name="result"/> using the given <paramref name="errorType"/>.
        /// </summary>
        public ErrorResult(ErrorType errorType, IResult result) : base(result)
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

        /// <inheritdoc />
        /// <summary>
        /// Creates a new ErrorResult from the given <paramref name="result"/>.
        /// If <see cref="T:prayzzz.Common.Results.ErrorType" /> is None, ErrorType.Unknown is used instead.
        /// </summary>
        public ErrorResult(IResult result) : base(result)
        {
            if (result.ErrorType == ErrorType.None)
            {
                ErrorType = ErrorType.Unknown;
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// Creates a new ErrorResult from the given <paramref name="result"/> using the given <paramref name="errorType"/>.
        /// </summary>
        public ErrorResult(ErrorType errorType, IResult result) : base(result)
        {
            ErrorType = errorType;
        }
    }
}