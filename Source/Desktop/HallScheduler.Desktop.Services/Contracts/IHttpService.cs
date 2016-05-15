namespace HallScheduler.Desktop.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHttpService
    {
        Task<object> GetAsync<Т>(string url);

        Task<object> GetAsync<Т>(string url, string authToken);

        Task<object> PostUrlEncodedAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> data);

        Task<object> PostAsJsonAsync<T>(string url, object data);
    }
}
