using System;

namespace prayzzz.Common.Results
{
    public static class ResultExtensions
    {
        public static Result<TOut> ContinueWith<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
        {
            if (!result.IsSuccess)
            {
                return new ErrorResult<TOut>(result);
            }

            return func(result.Data);
        }

        public static Result ContinueWith<TIn>(this Result<TIn> result, Func<TIn, Result> func)
        {
            if (!result.IsSuccess)
            {
                return result;
            }

            return func(result.Data);
        }

        public static Result<TOut> WithData<TOut>(this Result result, TOut data)
        {
            if (!result.IsSuccess)
            {
                return new ErrorResult<TOut>(result);
            }

            return new SuccessResult<TOut>(data);
        }

        public static Result<TOut> WithDataAs<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
        {
            if (!result.IsSuccess)
            {
                return new ErrorResult<TOut>(result);
            }

            return new SuccessResult<TOut>(func(result.Data));
        }
    }
}