namespace HallScheduler.Desktop.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Contracts;
    using System;
    using System.Net.Http.Headers;

    public class HttpService : IHttpService
    {
        public async Task<object> GetAsync<Т>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url))
                {
                    using (var content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();

                        var result = JsonConvert.DeserializeObject<Т>(data);

                        return result;
                    }
                }
            }
        }

        public async Task<object> GetAsync<Т>(string url, string authToken)
        {
            using (var httpClient = new HttpClient())
            {
                if (!String.IsNullOrEmpty(authToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                }

                using (var response = await httpClient.GetAsync(url))
                {
                    using (var content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Т>(data);
                        return result;
                    }
                }
            }
        }

        public async Task<object> PostUrlEncodedAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            var query = new FormUrlEncodedContent(data);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, query))
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(responseContent);
                        return result;
                    }
                }
            }
        }

        public async Task<object> PostAsJsonAsync<T>(string url, object data)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(url, data))
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(responseContent);
                        return result;
                    }
                }
            }
        }
    }
}
