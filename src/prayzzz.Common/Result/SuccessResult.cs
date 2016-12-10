namespace prayzzz.Common.Result
{
    public class SuccessResult<TData> : Result<TData>
    {
        public SuccessResult(TData data) :
            base(data)
        {
            IsSuccess = true;
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
    }
}