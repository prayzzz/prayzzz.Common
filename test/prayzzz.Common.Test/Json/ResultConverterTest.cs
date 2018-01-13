using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.IsFalse(new ResultConverter().CanConvert(typeof(ErrorResult)));
            Assert.IsFalse(new ResultConverter().CanConvert(typeof(SuccessResult)));
        }

        [TestMethod]
        public void WriteSuccessResult()
        {
            var result = new SuccessResult("Success!", "My Argument");

            var json = JsonConvert.SerializeObject(result);

            Assert.AreEqual("{\"ErrorType\":0,\"Exception\":null,\"IsError\":false,\"IsSuccess\":true,\"Message\":\"Success!\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadSuccessResult()
        {
            var json = "{\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"Success!\",\"MessageArgs\":[\"My Argument\"]}";
            var result = JsonConvert.DeserializeObject<SuccessResult>(json);

            Assert.AreEqual(ErrorType.None, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("Success!", result.Message);
            Assert.AreEqual(1, result.MessageArgs.Length);
            Assert.AreEqual("My Argument", result.MessageArgs[0]);
        }

        [TestMethod]
        public void WriteErrorResultWithMessage()
        {
            var result = new ErrorResult(ErrorType.ValidationError, "Error!", "My Argument");

            var json = JsonConvert.SerializeObject(result);

            Assert.AreEqual("{\"ErrorType\":4,\"Exception\":null,\"IsError\":true,\"IsSuccess\":false,\"Message\":\"Error!\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadErrorResultWithMessage()
        {
            var json = "{\"ErrorType\":4,\"Exception\":null,\"IsSuccess\":false,\"IsError\":true,\"Message\":\"Error!\",\"MessageArgs\":[\"My Argument\"]}";
            var result = JsonConvert.DeserializeObject<ErrorResult>(json);

            Assert.AreEqual(ErrorType.ValidationError, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("Error!", result.Message);
            Assert.AreEqual(1, result.MessageArgs.Length);
            Assert.AreEqual("My Argument", result.MessageArgs[0]);
        }

        [TestMethod]
        public void WriteErrorResultWitException()
        {
            var result = new ErrorResult(new NotImplementedException(), "My Message", "My Argument");

            var json = JsonConvert.SerializeObject(result);

            Assert.AreEqual("{\"ErrorType\":3,\"Exception\":{\"Message\":\"The method or operation is not implemented.\",\"Data\":{},\"InnerException\":null,\"TargetSite\":null,\"StackTrace\":null,\"HelpLink\":null,\"Source\":null,\"HResult\":-2147467263},\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadErrorResultWithException()
        {
            var json = "{\"ErrorType\":3,\"Exception\":{\"Message\":\"The method or operation is not implemented.\",\"Data\":{},\"InnerException\":null,\"TargetSite\":null,\"StackTrace\":null,\"HelpLink\":null,\"Source\":null,\"HResult\":-2147467263},\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}";
            var result = JsonConvert.DeserializeObject<ErrorResult>(json);

            Assert.AreEqual(ErrorType.InternalError, result.ErrorType);
            Assert.IsNotNull(result.Exception);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("My Message", result.Message);
            Assert.IsTrue(result.MessageArgs.Contains("My Argument"));
        }

        [TestMethod]
        public void ReadSuccessResultAsAbstractResult()
        {
            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };

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
        public void ReadErrorResultAsAbstractResult()
        {
            var settings = new JsonSerializerSettings { Converters = new List<JsonConverter> { new ResultConverter() } };

            var json = "{\"ErrorType\":3,\"Exception\":{\"Message\":\"The method or operation is not implemented.\",\"Data\":{},\"InnerException\":null,\"TargetSite\":null,\"StackTrace\":null,\"HelpLink\":null,\"Source\":null,\"HResult\":-2147467263},\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}";
            var result = JsonConvert.DeserializeObject<Result>(json, settings);

            Assert.IsInstanceOfType(result, typeof(ErrorResult));
            Assert.AreEqual(ErrorType.InternalError, result.ErrorType);
            Assert.IsNotNull(result.Exception);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("My Message", result.Message);
            Assert.IsTrue(result.MessageArgs.Contains("My Argument"));
        }
    }
}