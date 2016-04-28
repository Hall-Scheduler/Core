namespace HallScheduler.Services.Data.Services
{
    using Contracts;
    using HallScheduler.Data.Contracts;
    using HallScheduler.Data.Models;
    using System.Linq;

    public class HallsService : IHallsService
    {
        public HallsService(IRepository<Hall> halls)
        {
            this.Halls = halls;
        }

        private IRepository<Hall> Halls { get; set; }

        public IQueryable<Hall> All()
        {
            return this.Halls.All();
        }

        public Hall GetById(int id)
        {
            return this.Halls.GetById(id);
        }
    }
}
