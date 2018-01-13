using System.Collections.Generic;
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

            var successResult = new SuccessResult();
            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":0,\"Exception\":null,\"IsError\":false,\"IsSuccess\":true,\"Message\":\"\",\"MessageArgs\":[]}", json);
        }

        [TestMethod]
        public void SerializeDataResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };

            var successResult = new SuccessResult<string>("My Data");
            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"Data\":\"My Data\",\"ErrorType\":0,\"Exception\":null,\"IsError\":false,\"IsSuccess\":true,\"Message\":\"\",\"MessageArgs\":[]}", json);
        }

        [TestMethod]
        public void DeserializeDataResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };

            var json = "{\"Data\":\"My Data\",\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"\",\"MessageArgs\":[]}";
            var result = JsonConvert.DeserializeObject<SuccessResult<string>>(json, settings);
            
            Assert.IsInstanceOfType(result, typeof(SuccessResult<string>));
            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("", result.Message);
            Assert.AreEqual(0, result.MessageArgs.Length);
            Assert.AreEqual("My Data", result.Data);
        }

        [TestMethod]
        public void DeserializeSimpleResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };

            var json = "{\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"\",\"MessageArgs\":[]}";
            var successResult = JsonConvert.DeserializeObject<SuccessResult>(json, settings);

            Assert.AreEqual(ErrorType.None, successResult.ErrorType);
            Assert.IsNull(successResult.Exception);
            Assert.IsTrue(successResult.IsSuccess);
            Assert.IsFalse(successResult.IsError);
            Assert.AreEqual("", successResult.Message);
            Assert.AreEqual(0, successResult.MessageArgs.Length);
        }

        [TestMethod]
        public void DeserializeSimpleSuccessResultAsAbstractResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };
            
            var json = "{\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"\",\"MessageArgs\":[]}";
            var result = JsonConvert.DeserializeObject<Result>(json, settings);

            Assert.IsInstanceOfType(result, typeof(SuccessResult));
            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("", result.Message);
            Assert.AreEqual(0, result.MessageArgs.Length);
        }

        [TestMethod]
        public void DeserializeDataResultAsAbstractResult()
        {
            var settings = new JsonSerializerSettings { ContractResolver = new ResultContractResolver() };
            
            var json = "{\"Data\":\"My Data\",\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"\",\"MessageArgs\":[]}";
            var result = JsonConvert.DeserializeObject<Result<string>>(json, settings);

            Assert.IsInstanceOfType(result, typeof(SuccessResult<string>));
            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("", result.Message);
            Assert.AreEqual(0, result.MessageArgs.Length);
        }
    }
}