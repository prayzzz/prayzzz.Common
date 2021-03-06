﻿using Microsoft.Extensions.Logging;
using System;

namespace prayzzz.Common.Unit
{
    public class ConsoleLogger<T> : ConsoleLogger, ILogger<T>
    {
    }

    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return new LoggingScope();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"[{logLevel}]: {state}");
        }

        private class LoggingScope : IDisposable
        {
            public LoggingScope()
            {
                Console.WriteLine("Scope started");
            }

            public void Dispose()
            {
                Console.WriteLine("Scope disposed");
            }
        }
    }
}