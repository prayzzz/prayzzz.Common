using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using prayzzz.Common.Mvc.Json;
using prayzzz.Common.Results;

namespace prayzzz.Common.Test.Json
{
    [TestClass]
    public class ResultConverterTest
    {
        [TestMethod]
        public void CanConvert()
        {
            Assert.IsTrue(new ResultConverter().CanConvert(typeof(Result)));
            Assert.IsTrue(new ResultConverter().CanWrite);
            Assert.IsTrue(new ResultConverter().CanRead);
        }

        [TestMethod]
        public void WriteSuccessResult()
        {
            var result = new Result(ErrorType.None, "Success!", "My Argument");

            var jsonSettings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };
            var json = JsonConvert.SerializeObject(result, jsonSettings);

            Assert.AreEqual("{\"ErrorType\":0,\"Message\":\"Success!\",\"IsError\":false,\"IsSuccess\":true}", json);
        }

        [TestMethod]
        public void ReadSuccessResult()
        {
            var json = "{\"ErrorType\":0,\"Message\":\"Success!\"}";
            
            var jsonSettings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };
            var result = JsonConvert.DeserializeObject<Result>(json, jsonSettings);

            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("Success!", result.Message);
        }

        [TestMethod]
        public void WriteErrorResultWithMessage()
        {
            var result = new Result(ErrorType.ValidationError, "Error!", "My Argument");

            var jsonSettings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };
            var json = JsonConvert.SerializeObject(result, jsonSettings);

            Assert.AreEqual("{\"ErrorType\":4,\"Message\":\"Error!\",\"IsError\":true,\"IsSuccess\":false}", json);
        }

        [TestMethod]
        public void ReadErrorResultWithMessage()
        {
            var json = "{\"ErrorType\":4,\"Message\":\"Error!\"}";
            
            var jsonSettings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };
            var result = JsonConvert.DeserializeObject<Result>(json, jsonSettings);

            Assert.AreEqual(ErrorType.ValidationError, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("Error!", result.Message);
        }
    }
}