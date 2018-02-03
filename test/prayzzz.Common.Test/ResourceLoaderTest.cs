using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using prayzzz.Common.Results;
using prayzzz.Common.Unit;
using System.Threading.Tasks;

namespace prayzzz.Common.Test
{
    [TestClass]
    public class ResourceLoaderTest
    {
        [TestMethod]
        public async Task ReadEmbeddedFileAsync()
        {
            var resourceLoader = new ResourceLoader(new ConsoleLogger<ResourceLoader>());

            // Act
            var (result, content) = await resourceLoader.ReadEmbeddedFileAsync(GetType().Assembly, "Data.Entity.json").ConfigureAwait(false);
            var entity = JsonConvert.DeserializeObject<Entity>(content);

            // Assert
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Bar", entity.Foo);
        }

        public class Entity
        {
            public string Foo { get; set; }
        }
    }
}