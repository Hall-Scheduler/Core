namespace HallScheduler.Desktop.Client.Providers
{
    using FeserWard.Controls;
    using Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections;
    using Models;
    using Data.Models;
    using Server.DataTransferObjects.Users;

    public class LecturersProvider : IIntelliboxResultsProvider
    {
        public LecturersProvider(IHttpService httpService)
        {
            this.HttpService = httpService;
            // TODO: Rethink the initialization logic
            this.Initialize();
        }

        public IHttpService HttpService { get; set; }

        public List<UserBriefDTO> Lecturers { get; set; }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            return this.Lecturers?.Where(x => x.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        public async void Initialize()
        {
            try
            {
                var url = "http://localhost:38013/api/Users/Lecturers";
                var response = await this.HttpService.GetAsync<ResponseResult<List<UserBriefDTO>>>(url);
                this.Lecturers = (response as ResponseResult<List<UserBriefDTO>>).Data;
            }
            catch (Exception exc)
            {
                var error = exc.ToString();
                this.Lecturers = new List<UserBriefDTO>();
            }
        }
    }
}
