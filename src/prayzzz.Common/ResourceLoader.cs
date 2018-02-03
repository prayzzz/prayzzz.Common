using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using prayzzz.Common.Results;

namespace prayzzz.Common
{
    public class ResourceLoader
    {
        private readonly ILogger<ResourceLoader> _logger;

        public ResourceLoader(ILogger<ResourceLoader> logger)
        {
            _logger = logger;
        }

        public async Task<(Result Result, string Content)> ReadEmbeddedFileAsync(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file '{filePath}' from '{assembly.FullName}'");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (Result.NotFound, string.Empty);
                }

                using (var reader = new StreamReader(stream))
                {
                    return (Result.Success, await reader.ReadToEndAsync());
                }
            }
        }

        public (Result Result, string Content) ReadEmbeddedFile(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file '{filePath}' from '{assembly.FullName}'");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (Result.NotFound, string.Empty);
                }

                using (var reader = new StreamReader(stream))
                {
                    return (Result.Success, reader.ReadToEnd());
                }
            }
        }

        public (Result result, byte[] content) ReadEmbeddedBinaryFile(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file '{filePath}' from '{assembly.FullName}'");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (Result.NotFound, Array.Empty<byte>());
                }

                using (var reader = new BinaryReader(stream))
                {
                    return (Result.Success, reader.ReadBytes((int) stream.Length));
                }
            }
        }
    }
}