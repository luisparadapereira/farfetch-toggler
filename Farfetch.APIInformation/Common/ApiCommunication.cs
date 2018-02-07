using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Farfetch.APIHandler.Common
{
    /// <summary>
    /// Http communication with APIs
    /// </summary>
    internal class ApiCommunication
    {
        /// <summary>
        /// The Http client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Default constructor initializes the http client
        /// </summary>
        internal ApiCommunication()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// Gets from the API
        /// </summary>
        /// <typeparam name="T">The type of output we're expecting</typeparam>
        /// <param name="url">The URL to query</param>
        /// <returns>A result of type T</returns>
        internal async Task<T> ApiGet<T>(string url) where T : class
        {
            if (_client == null) throw new NullReferenceException("HttpClient hasn't been initialized");
            HttpResponseMessage response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            string jsonResult = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        /// <summary>
        /// Posts into API
        /// </summary>
        /// <typeparam name="TO">The output type</typeparam>
        /// <typeparam name="T">The type of entity to post</typeparam>
        /// <param name="url">The post Url</param>
        /// <param name="entity">The entity to post</param>
        /// <returns>A result of type TO</returns>
        internal async Task<TO> ApiPost<TO, T>(string url, T entity) where T: class, TO
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));

            if (_client == null) throw new NullReferenceException("HttpClient hasn't been initialized");
            HttpResponseMessage response = await _client.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return default(TO);

            string jsonResult = await response.Content.ReadAsStringAsync();
            TO result = JsonConvert.DeserializeObject<TO>(jsonResult);
            return result;
        }

        /// <summary>
        /// Puts into API
        /// </summary>
        /// <typeparam name="TO">The output type</typeparam>
        /// <typeparam name="T">The type of entity to put</typeparam>
        /// <param name="url">The put Url</param>
        /// <param name="entity">The entity to put</param>
        /// <returns>A result of type TO</returns>
        internal async Task<TO> ApiPut<TO, T>(string url, T entity) where T : class, TO
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));

            if (_client == null) throw new NullReferenceException("HttpClient hasn't been initialized");
            HttpResponseMessage response = await _client.PutAsync(url, content);
            if (!response.IsSuccessStatusCode) return default(TO);

            string jsonResult = await response.Content.ReadAsStringAsync();
            TO result = JsonConvert.DeserializeObject<TO>(jsonResult);
            return result;
        }

        /// <summary>
        /// Deletes from the API
        /// </summary>
        /// <typeparam name="T">The type of output we're expecting</typeparam>
        /// <param name="url">The URL to delete</param>
        /// <returns>A result of type T</returns>
        internal async Task<T> ApiDelete<T>(string url) where T : class
        {
            if (_client == null) throw new NullReferenceException("HttpClient hasn't been initialized");
            HttpResponseMessage response = await _client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            string jsonResult = await response.Content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

    }
}