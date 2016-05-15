namespace HallScheduler.Server.DataTransferObjects.Halls
{
    using AutoMapper;
    using Data.Models;
    using Events;
    using Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HallScheduleDTO : IMapFrom<Hall>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public List<EventDTО> Monday { get; set; }

        public List<EventDTО> Tuesday { get; set; }

        public List<EventDTО> Wednesday { get; set; }

        public List<EventDTО> Thursday { get; set; }

        public List<EventDTО> Friday { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Hall, HallScheduleDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Monday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Monday).ToList()))
                .ForMember(dest => dest.Tuesday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Tuesday).ToList()))
                .ForMember(dest => dest.Wednesday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Wednesday).ToList()))
                .ForMember(dest => dest.Thursday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Thursday).ToList()))
                .ForMember(dest => dest.Friday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Friday).ToList()));
        }
    }
}