namespace HallScheduler.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EventLog
    {
        public EventLog()
        {
            this.DateCreated = DateTime.Now;
        }

        public int Id { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [MaxLength(300)]
        public string AuthorId { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public int EventId { get; set; }
    }
}
