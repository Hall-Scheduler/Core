namespace HallScheduler.Services.Data.Contracts
{
    using HallScheduler.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventsService
    {
        IQueryable<Event> All();

        void Add(Event model);

        void Update(Event model);
    }
}