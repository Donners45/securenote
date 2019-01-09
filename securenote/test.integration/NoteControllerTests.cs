using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using test.integration.Base;
using Xunit;

namespace test.integration
{
    public class NoteControllerTests : IClassFixture<TestFixture<api.Startup>>
    {
        public NoteControllerTests(TestFixture<api.Startup> fixture)
        { 
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task GetNote_WithId_200()
        {
            var Id = "test-id";
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/note/{Id}");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetNote_WithoutId_404()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/note/");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}






