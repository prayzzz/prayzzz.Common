using System.Reflection;

namespace prayzzz.Common.Unit
{
    public static class StaticResourceLoader
    {
        public static string ReadEmbeddedFile(Assembly assembly, string filePath)
        {
            var (result, content) = new ResourceLoader(new ConsoleLogger<ResourceLoader>()).ReadEmbeddedFile(assembly, filePath);

            if (result.IsError)
            {
                throw result.Exception;
            }

            return content;
        }
    }
}