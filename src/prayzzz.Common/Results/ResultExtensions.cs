namespace prayzzz.Common.Results
{
    public static class ResultExtensions
    {
        public static string ToMessageString(this Result result)
        {
            if (string.IsNullOrEmpty(result.Message))
            {
                return string.Empty;
            }
            
            return string.Format(result.Message, result.MessageArgs);
        }
    }
}