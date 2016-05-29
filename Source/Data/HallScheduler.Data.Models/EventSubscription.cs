namespace HallScheduler.Data.Models
{
    using Common.Constants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventSubscription
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(NavigationPropertiesConstants.LecturerId)]
        public virtual User Lecturer { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [ForeignKey(NavigationPropertiesConstants.EventId)]
        public virtual Event Event { get; set; }

        [Required]
        public int EventId { get; set; }
    }
}