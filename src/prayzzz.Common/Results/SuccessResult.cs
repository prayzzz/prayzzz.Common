namespace prayzzz.Common.Results
{
    public class SuccessResult<TData> : Result<TData>
    {
        public SuccessResult(TData data)
        {
            ErrorType = ErrorType.None;
            Data = data;
        }

        public SuccessResult(string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.None;
            Message = message;
            MessageArgs = messageArgs;
        }

        public SuccessResult(TData data, string message, params object[] messageArgs)
        {
            Data = data;
            ErrorType = ErrorType.None;
            Message = message;
            MessageArgs = messageArgs;
        }
    }

    public class SuccessResult : Result
    {
        /// <summary>
        ///     Default instance of <see cref="SuccessResult" />.
        /// </summary>
        public static SuccessResult Default;

        static SuccessResult()
        {
            Default = new SuccessResult();
        }

        public SuccessResult()
        {
            ErrorType = ErrorType.None;
        }

        public SuccessResult(string message, params object[] messageArgs)
        {
            ErrorType = ErrorType.None;
            Message = message;
            MessageArgs = messageArgs;
        }
    }
}