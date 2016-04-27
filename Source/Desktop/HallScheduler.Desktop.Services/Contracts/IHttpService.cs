namespace HallScheduler.Desktop.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHttpService
    {
        Task<object> Get<Т>(string url);

        Task<object> Get<Т>(string url, string authToken);

        Task<object> Post<T>(string url, IEnumerable<KeyValuePair<string, string>> data);
    }
}
