namespace HallScheduler.Desktop.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class HttpService
    {
        public async Task<object> Get<Т>(string url)
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

        public async Task<string> Post(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            var query = new FormUrlEncodedContent(data);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, query))
                {
                    using (var content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();

                        return result;
                    }
                }
            }
        }
    }
}
