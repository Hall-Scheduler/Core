namespace HallScheduler.Desktop.Client.Providers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using FeserWard.Controls;
    using Models;
    using Server.DataTransferObjects.Halls;
    using Services;

    public class LinqToEntitiesProvider : IIntelliboxResultsProvider
    {
        public LinqToEntitiesProvider(HttpService httpService)
        {
            this.HttpService = httpService;

            // TODO: Rethink the initialization logic
            this.Initialize();
        }

        public HttpService HttpService { get; set; }

        public List<HallBriefDTO> Halls { get; set; }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            return this.Halls?.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public async void Initialize()
        {
            try
            {
                var url = "http://localhost:38013/api/Halls/All";
                var response = await this.HttpService.Get<ResponseResult<List<HallBriefDTO>>>(url);
                this.Halls = (response as ResponseResult<List<HallBriefDTO>>).Data;
            }
            catch (Exception exc)
            {
                var error = exc.ToString();
                this.Halls = new List<HallBriefDTO>();
            }
        }
    }
}
