using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Experian.Test.ApiClient.Json;

namespace Experian.Test.ApiClient
{
    public interface IAlbumsRestClient
    {
        Task<IEnumerable<Albums>> GetAlbumsAsync();
        Task<IEnumerable<Photos>> GetPhotos();
    }

    public class AlbumsRestClient : IAlbumsRestClient
    {
        private static readonly HttpClient _client = new HttpClient();

        public async Task<IEnumerable<Albums>> GetAlbumsAsync()
        {
            //http://jsonplaceholder.typicode.com/albums
            var request = new HttpRequestMessage(HttpMethod.Get, "/albums");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            HttpResponseMessage response = await _client.GetAsync("http://jsonplaceholder.typicode.com/albums");
                    
            var stream = await response.Content.ReadAsStreamAsync();
            response.EnsureSuccessStatusCode();
            return stream.ReadAndDeserializeFromJson<List<Albums>>();
        }

        public async Task<IEnumerable<Photos>> GetPhotos()
        {
            //http://jsonplaceholder.typicode.com/photos
            var request = new HttpRequestMessage(HttpMethod.Get, "/photos");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            HttpResponseMessage response = await _client.GetAsync("http://jsonplaceholder.typicode.com/photos");
            var stream = await response.Content.ReadAsStreamAsync();
            response.EnsureSuccessStatusCode();
            return stream.ReadAndDeserializeFromJson<List<Photos>>();
        }
    }
}