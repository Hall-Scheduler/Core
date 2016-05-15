namespace HallScheduler.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common.Constants;

    public class Event
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(NavigationPropertiesConstants.LecturerId)]
        public virtual User Lecturer { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [ForeignKey(NavigationPropertiesConstants.HallId)]
        public virtual Hall Hall { get; set; }

        [Required]
        public int HallId { get; set; }

        [Required]
        [EnumDataType(
            typeof(DayOfWeek), 
            ErrorMessage = ValidationConstants.EventDayOfWeekErrorMessage)]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartsAt { get; set; }

        [Required]
        public TimeSpan EndsAt { get; set; }

        [Required]
        [MinLength(ValidationConstants.EventTopicMinLength, ErrorMessage = ValidationConstants.EventTopicMinLengthErrorMessage)]
        [MaxLength(ValidationConstants.EventTopicMaxLength, ErrorMessage = ValidationConstants.EventTopicMaxLengthErrorMessage)]
        public string Topic { get; set; }
    }
}
