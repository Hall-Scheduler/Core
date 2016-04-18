namespace HallScheduler.Services.Data.Contracts
{
    using HallScheduler.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHallsService
    {
        IQueryable<Hall> All();

        Hall GetById(int id);
    }
}
