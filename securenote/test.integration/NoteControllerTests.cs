using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using domain;
using Newtonsoft.Json;
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
        public async Task PostNote_SuppliedMessage_201AndId()
        {
            var message = "test-message";
            var request = new HttpRequestMessage(new HttpMethod("POST"), $"api/note/{message}");

            // Act
            var response = await Client.SendAsync(request);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(!string.IsNullOrEmpty(content));
        }

        [Fact]
        public async Task GetNote_CreateMessageWithId_200AndOriginalMessage()
        {
            var message = "test-message";
            var postRequest = new HttpRequestMessage(new HttpMethod("POST"), $"api/note/{message}");
            var postResponse = await Client.SendAsync(postRequest);
            var noteId = await postResponse.Content.ReadAsStringAsync();
            noteId = noteId.Replace("\"", "");

            var getRequest = new HttpRequestMessage(new HttpMethod("GET"), $"api/note/{noteId}");
            var getResponse = await Client.SendAsync(getRequest);
            var returnedMessage = await getResponse.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<Note>(returnedMessage);

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(message, note.Message);
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






