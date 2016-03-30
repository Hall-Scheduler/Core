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

        [Required]
        [ForeignKey(NavigationPropertiesConstants.LecturerId)]
        public virtual User Lecturer { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [Required]
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
        [RegularExpression(
            ValidationConstants.EventStartingHourRegex, 
            ErrorMessage = ValidationConstants.EventStartingHourErrorMessage)]
        public string StartsAt { get; set; }

        [Required]
        [Range(
            ValidationConstants.EventDurationMinLength,
            ValidationConstants.EventDurationMaxLength,
            ErrorMessage = ValidationConstants.EventDurationErrorMessage)]
        public int DurationInMinutes { get; set; }
    }
}
