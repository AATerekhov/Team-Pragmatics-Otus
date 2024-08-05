using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings
{
    public class SimpleHttpClient(string baseurl)
    {
        public async Task DeleteAsync<T,D>(string url,D index)
            where D : struct
            where T : class
        {
            HttpClient client = GetClient();
            await client.DeleteAsync(url+"/"+ index);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            HttpClient client = GetClient();

            using var response = await client.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, T placeType) 
        {
            HttpClient сlient = GetClient();

            JsonContent content = JsonContent.Create(placeType);
            using var response = await сlient.PostAsync(url, content);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task PostAsyncNotResult<T>(string url, T element)
        {
            HttpClient сlient = GetClient();
            JsonContent content = JsonContent.Create(element);
            await сlient.PostAsync(url, content);
        }

        private HttpClient GetClient() 
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(baseurl)
            };
        }
    }
}
