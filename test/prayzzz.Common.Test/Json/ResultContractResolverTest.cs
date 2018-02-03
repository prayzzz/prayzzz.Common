using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using prayzzz.Common.Mvc.Json;
using prayzzz.Common.Results;

namespace prayzzz.Common.Test.Json
{
    [TestClass]
    public class ResultContractResolverTest
    {
        [TestMethod]
        public void SerializeSimpleResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };

            var successResult = new Result();
            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":0,\"Message\":\"\",\"IsError\":false,\"IsSuccess\":true}", json);
        }

        [TestMethod]
        public void DeserializeSimpleResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };

            var json = "{\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"\",\"MessageArgs\":[]}";
            var successResult = JsonConvert.DeserializeObject<Result>(json, settings);

            Assert.AreEqual(ErrorType.None, successResult.ErrorType);
            Assert.IsNull(successResult.Exception);
            Assert.IsTrue(successResult.IsSuccess);
            Assert.IsFalse(successResult.IsError);
            Assert.AreEqual("", successResult.Message);
        }
    }
}