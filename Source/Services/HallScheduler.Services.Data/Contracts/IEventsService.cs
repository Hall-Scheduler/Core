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

        int Add(Event model);

        int Update(Event model);

        void Delete(Event model);

        void DeleteById(int eventId);

        Event GetById(int eventId);
    }
}