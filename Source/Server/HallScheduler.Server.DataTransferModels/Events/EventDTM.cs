namespace HallScheduler.Server.DataTransferModels.Events
{
    using AutoMapper;
    using Data.Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class EventDTM : IMapFrom<Event>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required]
        public string LecturerId { get; set; }

        [Required]
        public string HallName { get; set; }

        [Required]
        public int HallId { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        [Range(
            ValidationConstants.EventStartsAtMinValue,
            ValidationConstants.EventStartsAtMaxValue,
            ErrorMessage = ValidationConstants.EventStartsAtErrorMessage)]
        public int StartsAt { get; set; }

        [Required]
        [Range(
            ValidationConstants.EventDurationMinLength,
            ValidationConstants.EventDurationMaxLength,
            ErrorMessage = ValidationConstants.EventDurationErrorMessage)]
        public int EndsAt { get; set; }

        [Required]
        [MinLength(
            ValidationConstants.EventTopicMinLength, 
            ErrorMessage = ValidationConstants.EventTopicMinLengthErrorMessage)]
        [MaxLength(
            ValidationConstants.EventTopicMaxLength, 
            ErrorMessage = ValidationConstants.EventTopicMaxLengthErrorMessage)]
        public string Topic { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Event, EventDTM>()
                .ForMember(dest => dest.DayOfWeek, opts => opts.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.EndsAt, opts => opts.MapFrom(src => src.EndsAt))
                .ForMember(dest => dest.HallName, opts => opts.MapFrom(src => src.Hall.Block.ToString() + src.Hall.Stage.ToString() + src.Hall.Room.ToString()))
                .ForMember(dest => dest.HallId, opts => opts.MapFrom(src => src.HallId))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.LecturerName, opts => opts.MapFrom(src => src.Lecturer.FirstName + " " + src.Lecturer.LastName))
                .ForMember(dest => dest.LecturerId, opts => opts.MapFrom(src => src.LecturerId))
                .ForMember(dest => dest.StartsAt, opts => opts.MapFrom(src => src.StartsAt))
                .ForMember(dest => dest.Topic, opts => opts.MapFrom(src => src.Topic));
        }
    }
}
