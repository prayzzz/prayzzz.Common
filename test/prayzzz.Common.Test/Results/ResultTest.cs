using Microsoft.VisualStudio.TestTools.UnitTesting;
using prayzzz.Common.Results;

namespace prayzzz.Common.Test.Results
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void CastUnknownToInterface()
        {
            var result = GetResultOfUnkownType();

            var typedResult = result as IResult<object>;

            Assert.IsNotNull(typedResult);
        }

        private static object GetResultOfUnkownType()
        {
            return new SuccessResult<string>("My Data");
        }
    }
}