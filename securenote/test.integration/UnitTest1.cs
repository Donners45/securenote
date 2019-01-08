using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using test.integration.Base;
using Xunit;

namespace test.integration
{
    public class UnitTest1 : IClassFixture<TestFixture<api.Startup>>
    {
        public UnitTest1(TestFixture<api.Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task GET_ValuesRoute_ReturnsOK()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "api/values");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
