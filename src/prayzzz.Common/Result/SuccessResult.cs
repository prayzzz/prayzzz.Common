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
        public SuccessResult()
        {
            IsSuccess = true;
        }
    }
}