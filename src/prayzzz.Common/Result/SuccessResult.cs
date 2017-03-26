namespace prayzzz.Common.Result
{
    public class SuccessResult<TData> : Result<TData>
    {
        public SuccessResult(TData data) :
            base(data)
        {
            IsSuccess = true;
        }

        public SuccessResult(string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = true;
            MessageArgs = messageArgs;
        }
    }

    public class SuccessResult : Result
    {
        /// <summary>
        /// Default instance of <see cref="SuccessResult"/> to reduce instantiations.
        /// </summary>
        public static SuccessResult Default;

        static SuccessResult()
        {
            Default = new SuccessResult();
        }

        public SuccessResult()
        {
            IsSuccess = true;
        }

        public SuccessResult(string message, params object[] messageArgs)
            : base(message)
        {
            IsSuccess = true;
            MessageArgs = messageArgs;
        }
    }
}