namespace HallScheduler.Data.Common.Contracts
{
    using System;

    public interface IEvent
    {
        TimeSpan StartsAt { get; set; }

        TimeSpan EndsAt { get; set; }
    }
}
