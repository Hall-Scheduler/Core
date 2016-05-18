namespace HallScheduler.Data.Common
{
    using Contracts;
 
    public class ScheduleNode<T>
        where T : IEvent
    {
        public ScheduleNode(T data)
        {
            this.Data = data;
        }

        public ScheduleNode<T> Parent { get; set; }

        public ScheduleNode<T> LeftChild { get; set; }

        public ScheduleNode<T> RightChild { get; set; }

        public T Data { get; set; }

        public double Weight
        {
            get
            {
                return this.Data.StartsAt.TotalSeconds + this.Data.EndsAt.TotalSeconds;
            }
        }
    }
}
