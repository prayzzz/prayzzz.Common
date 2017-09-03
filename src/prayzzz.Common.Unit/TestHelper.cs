using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace prayzzz.Common.Unit
{
    public static class TestHelper
    {
        public static Mock<T> Mock<T>(MockBehavior mockBehavior = MockBehavior.Strict) where T : class
        {
            return new Mock<T>(mockBehavior);
        }

        [Obsolete]
        public static Mock<ILoggerFactory> MockLogger<T>()
        {
            var loggerFactory = Mock<ILoggerFactory>(MockBehavior.Loose);
            loggerFactory.Setup(x => x.CreateLogger(typeof(T).FullName)).Returns(new ConsoleLogger());

            return loggerFactory;
        }
        
        public static Mock<ILoggerFactory> MockLoggerFactory<T>()
        {
            var loggerFactory = Mock<ILoggerFactory>(MockBehavior.Loose);
            loggerFactory.Setup(x => x.CreateLogger(typeof(T).FullName)).Returns(new ConsoleLogger());

            return loggerFactory;
        }

        public static string ReadEmbeddedFile(Assembly assembly, string filePath)
        {
            var (result, content) = new ResourceLoader(new ConsoleLogger<ResourceLoader>()).ReadEmbeddedFile(assembly, filePath);

            if (result.IsError)
            {
                throw result.Exception;
            }

            return content;
        }

        public static IOptions<T> CreateOptions<T>(T value) where T : class, new()
        {
            var optionsMock = Mock<IOptions<T>>();
            optionsMock.Setup(x => x.Value).Returns(value);

            return optionsMock.Object;
        }
    }
}