using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DotnetCoreDapperSample.Api.Models;
using DotnetCoreDapperSample.Api.Test.Shared;
using NUnit.Framework;

namespace DotnetCoreDapperSample.Api.Test.Controllers
{
    public class BlogControllerTest : IntegrationTestBase
    {
        private const string _url = "http://localhost/api/blog";

        [Test]
        public async Task Get_正常系()
        {
            // Arrange・Act
            var response = await client.GetAsync(_url);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<List<Blog>>();
            Assert.IsTrue(result?.Any());
        }
    }
}
