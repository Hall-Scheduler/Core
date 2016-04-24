﻿namespace HallScheduler.Desktop.Client.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections;
    using FeserWard.Controls;
    using Services;
    using Models;
    using Server.DataTransferModels.Halls;

    public class LinqToEntitiesProvider : IIntelliboxResultsProvider
    {
        public LinqToEntitiesProvider()
        {
            this.Initialize();
        }

        public HttpService HttpService { get; set; } = new HttpService();

        public List<HallBriefDTO> Halls { get; set; }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            return this.Halls.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public async void Initialize()
        {
            var url = "http://localhost:38013/api/Halls/All";

            try
            {
                var response = await this.HttpService.Get<ResponseResult<List<HallBriefDTO>>>(url);
                this.Halls = (response as ResponseResult<List<HallBriefDTO>>).Data;
            }
            catch (Exception exc)
            {
                this.Halls = new List<HallBriefDTO>();
            }
        }
    }
}