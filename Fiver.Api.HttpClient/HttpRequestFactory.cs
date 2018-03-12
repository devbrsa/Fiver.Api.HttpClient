using System.Net.Http;
using System.Threading.Tasks;

namespace Fiver.Api.HttpClient
{
    public class HttpRequestFactory
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public HttpRequestFactory(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> Get(string requestUri)
            => Get(requestUri, "");

        public Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }

        public Task<HttpResponseMessage> Post(string requestUri, object value)
            => Post(requestUri, value, "");

        public Task<HttpResponseMessage> Post(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }

        public Task<HttpResponseMessage> Put(string requestUri, object value)
            => Put(requestUri, value, "");

        public Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value))
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }

        public Task<HttpResponseMessage> Patch(string requestUri, object value)
            => Patch(requestUri, value, "");

        public Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value))
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }

        public Task<HttpResponseMessage> Delete(string requestUri)
            => Delete(requestUri, "");

        public Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }

        public async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, "");

        public Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, string bearerToken)
        {
            var builder = new HttpRequestBuilder(_httpClient)
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new FileContent(filePath, apiParamName))
                                .AddBearerToken(bearerToken);

            return builder.SendAsync();
        }
    }
}
