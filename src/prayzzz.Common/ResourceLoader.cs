using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using prayzzz.Common.Results;
using System;

namespace prayzzz.Common
{
    public class ResourceLoader
    {
        private readonly ILogger<ResourceLoader> _logger;

        public ResourceLoader(ILogger<ResourceLoader> logger)
        {
            _logger = logger;
        }

        public async Task<(Result result, string content)> ReadEmbeddedFileAsync(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file \"{filePath}\" from \"{assembly.FullName}\"");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (new ErrorResult(new FileNotFoundException(name), "File not found"), string.Empty);
                }

                using (var reader = new StreamReader(stream))
                {
                    return (new SuccessResult(), await reader.ReadToEndAsync());
                }
            }
        }

        public (Result result, string content) ReadEmbeddedFile(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file \"{filePath}\" from \"{assembly.FullName}\"");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (new ErrorResult(new FileNotFoundException(name), "File not found"), string.Empty);
                }

                using (var reader = new StreamReader(stream))
                {
                    return (new SuccessResult(), reader.ReadToEnd());
                }
            }
        }

        public (Result result, byte[] content) ReadEmbeddedBinaryFile(Assembly assembly, string filePath)
        {
            _logger.LogDebug($"Loading file \"{filePath}\" from \"{assembly.FullName}\"");

            var name = $"{assembly.GetName().Name}.{filePath}";
            using (var stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                {
                    return (new ErrorResult(new FileNotFoundException(name), "File not found"), Array.Empty<byte>());
                }

                using (var reader = new BinaryReader(stream))
                {
                    return (new SuccessResult(), reader.ReadBytes((int)stream.Length));
                }
            }
        }
    }
}