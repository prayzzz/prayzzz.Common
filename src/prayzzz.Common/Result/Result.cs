using System;

namespace prayzzz.Common.Result
{
    public interface IResult<out TData>
    {
        TData Data { get; }

        string Message { get; }

        object[] MessageArgs { get; }

        ErrorType ErrorType { get; }

        Exception Exception { get; }

        bool IsSuccess { get; }
    }

    public abstract class Result<TData> : Result, IResult<TData>
    {
        /// <summary>
        ///     Creates a result with an error message.
        /// </summary>
        /// <param name="message">Unlocalized error message</param>
        protected Result(string message)
            : base(message)
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
        /// <param name="message">Unlocalized error message</param>
        protected Result(string message)
        {
            Message = message;
        }

        /// <summary>
        ///     Creates a new result with the data from the given result
        /// </summary>
        protected Result(Result result)
        {
            IsSuccess = result.IsSuccess;
            ErrorType = result.ErrorType;
            Message = result.Message;
            MessageArgs = result.MessageArgs;
        }

        public string Message { get; protected set; }

        public object[] MessageArgs { get; protected set; }

        public ErrorType ErrorType { get; protected set; }

        public Exception Exception { get; protected set; }

        public bool IsSuccess { get; protected set; }
    }
}