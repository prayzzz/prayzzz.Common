using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using prayzzz.Common.Results;
using prayzzz.Common.Unit;

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
            var (result, content) = await resourceLoader.ReadEmbeddedFileAsync(GetType().Assembly, "Data.Entity.json");
            var entity = JsonConvert.DeserializeObject<Entity>(content);

            // Assert
            Assert.IsInstanceOfType(result, typeof(SuccessResult));
            Assert.AreEqual("Bar", entity.Foo);
        }

        public class Entity
        {
            public string Foo { get; set; }
        }
    }
}