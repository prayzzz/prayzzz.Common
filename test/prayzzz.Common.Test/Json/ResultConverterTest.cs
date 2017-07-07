﻿using System;
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
        public void CanConvertWithSimpleErrorResult()
        {
            Assert.IsTrue(new ResultConverter().CanConvert(typeof(ErrorResult)));
        }

        [TestMethod]
        public void CanConvertWithSimpleSuccessResult()
        {
            Assert.IsTrue(new ResultConverter().CanConvert(typeof(SuccessResult)));
        }

        [TestMethod]
        public void WriteSimpleSuccessResult()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var successResult = new SuccessResult();

            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":0,\"Exception\":null,\"IsError\":false,\"IsSuccess\":true,\"Message\":\"\",\"MessageArgs\":[]}", json);
        }

        [TestMethod]
        public void ReadSimpleSuccessResult()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

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
        public void ReadSimpleSuccessResultAsAbstractResult()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

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
        public void WriteSimpleErrorResultWithMessage()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var successResult = new ErrorResult(ErrorType.ValidationError, "My Message", "My Argument");

            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":4,\"Exception\":null,\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadSimpleErrorResultWithMessage()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var json = "{\"ErrorType\":4,\"Exception\":null,\"IsSuccess\":false,\"IsError\":true,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}";
            var successResult = JsonConvert.DeserializeObject<ErrorResult>(json, settings);

            Assert.AreEqual(ErrorType.ValidationError, successResult.ErrorType);
            Assert.IsNull(successResult.Exception);
            Assert.IsFalse(successResult.IsSuccess);
            Assert.IsTrue(successResult.IsError);
            Assert.AreEqual("My Message", successResult.Message);
            Assert.IsTrue(successResult.MessageArgs.Contains("My Argument"));
        }

        [TestMethod]
        public void WriteSimpleSuccessResultWithMessage()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var successResult = new SuccessResult("My Message", "My Argument");

            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":0,\"Exception\":null,\"IsError\":false,\"IsSuccess\":true,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadSimpleSuccessResultWithMessage()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var json = "{\"ErrorType\":0,\"Exception\":null,\"IsSuccess\":true,\"IsError\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}";
            var successResult = JsonConvert.DeserializeObject<SuccessResult>(json, settings);

            Assert.AreEqual(ErrorType.None, successResult.ErrorType);
            Assert.IsNull(successResult.Exception);
            Assert.IsTrue(successResult.IsSuccess);
            Assert.IsFalse(successResult.IsError);
            Assert.AreEqual("My Message", successResult.Message);
            Assert.IsTrue(successResult.MessageArgs.Contains("My Argument"));
        }

        [TestMethod]
        public void WriteSimpleErrorResultWitException()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var successResult = new ErrorResult(new NotImplementedException(), "My Message", "My Argument");

            var json = JsonConvert.SerializeObject(successResult, settings);

            Assert.AreEqual("{\"ErrorType\":3,\"Exception\":{\"Message\":\"The method or operation is not implemented.\",\"Data\":{},\"InnerException\":null,\"TargetSite\":null,\"StackTrace\":null,\"HelpLink\":null,\"Source\":null,\"HResult\":-2147467263},\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}", json);
        }

        [TestMethod]
        public void ReadSimpleErrorResultWithException()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var json = "{\"ErrorType\":3,\"Exception\":{\"Message\":\"The method or operation is not implemented.\",\"Data\":{},\"InnerException\":null,\"TargetSite\":null,\"StackTrace\":null,\"HelpLink\":null,\"Source\":null,\"HResult\":-2147467263},\"IsError\":true,\"IsSuccess\":false,\"Message\":\"My Message\",\"MessageArgs\":[\"My Argument\"]}";
            var successResult = JsonConvert.DeserializeObject<ErrorResult>(json, settings);

            Assert.AreEqual(ErrorType.InternalError, successResult.ErrorType);
            Assert.IsNotNull(successResult.Exception);
            Assert.IsFalse(successResult.IsSuccess);
            Assert.IsTrue(successResult.IsError);
            Assert.AreEqual("My Message", successResult.Message);
            Assert.IsTrue(successResult.MessageArgs.Contains("My Argument"));
        }

        [TestMethod]
        public void ReadErrorResultWithoutException()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var json = "{\"errorType\":3,\"isError\":true,\"isSuccess\":false,\"message\":\"My Message\",\"messageArgs\":[]}\r\n";
            var result = JsonConvert.DeserializeObject<ErrorResult>(json, settings);

            Assert.AreEqual(ErrorType.InternalError, result.ErrorType);
            Assert.IsNull(result.Exception);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("My Message", result.Message);
            Assert.IsTrue(result.MessageArgs.Contains("My Argument"));
        }

        [TestMethod]
        public void ReadSuccessResult()
        {
            var resultConverter = new ResultConverter();
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(resultConverter);

            var json = "{\"errorType\":0,\"isError\":false,\"isSuccess\":true,\"message\":\"My Message\",\"messageArgs\":[]}";
            var successResult = JsonConvert.DeserializeObject<Result>(json, settings);

            Assert.AreEqual(ErrorType.None, successResult.ErrorType);
            Assert.IsNull(successResult.Exception);
            Assert.IsTrue(successResult.IsSuccess);
            Assert.IsFalse(successResult.IsError);
            Assert.AreEqual("My Message", successResult.Message);
        }
    }
}