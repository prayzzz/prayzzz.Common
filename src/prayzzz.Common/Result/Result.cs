using System;

namespace prayzzz.Common.Result
{
    public interface IResult<out TData>
    {
        TData Data { get; }

        string ErrorMessage { get; }

        object[] ErrorMessageArgs { get; }

        ErrorType ErrorType { get; }

        Exception Exception { get; }

        bool IsSuccess { get; }
    }

    public abstract class Result<TData> : Result, IResult<TData>
    {
        /// <summary>
        ///     Creates a result with an error message.
        /// </summary>
        /// <param name="errorMessage">Unlocalized error message</param>
        protected Result(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        ///     Creates a result with the given data.
        /// </summary>
        /// <param name="data"></param>
        protected Result(TData data)
        {
            Data = data;
        }

        /// <summary>
        ///     Creates a new result with the data from the given result
        /// </summary>
        /// <param name="result"></param>
        protected Result(Result result)
            : base(result)
        {
        }

        public TData Data { get; protected set; }
    }

    public abstract class Result
    {
        protected Result()
        {
        }

        /// <summary>
        ///     Creates a result with an error message.
        /// </summary>
        /// <param name="errorMessage">Unlocalized error message</param>
        protected Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        ///     Creates a new result with the data from the given result
        /// </summary>
        protected Result(Result result)
        {
            IsSuccess = result.IsSuccess;
            ErrorType = result.ErrorType;
            ErrorMessage = result.ErrorMessage;
            ErrorMessageArgs = result.ErrorMessageArgs;
        }

        public string ErrorMessage { get; protected set; }

        public object[] ErrorMessageArgs { get; protected set; }

        public ErrorType ErrorType { get; protected set; }

        public Exception Exception { get; protected set; }

        public bool IsSuccess { get; protected set; }
    }
}