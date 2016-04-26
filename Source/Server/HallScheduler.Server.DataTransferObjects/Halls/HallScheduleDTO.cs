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
        public List<EventDTM> Monday { get; set; }

        public List<EventDTM> Tuesday { get; set; }

        public List<EventDTM> Wednesday { get; set; }

        public List<EventDTM> Thursday { get; set; }

        public List<EventDTM> Friday { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Hall, HallScheduleDTO>()
                .ForMember(dest => dest.Monday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Monday).ToList()))
                .ForMember(dest => dest.Tuesday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Tuesday).ToList()))
                .ForMember(dest => dest.Wednesday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Wednesday).ToList()))
                .ForMember(dest => dest.Thursday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Thursday).ToList()))
                .ForMember(dest => dest.Friday, opts => opts.MapFrom(src => src.Schedule.Where(x => x.DayOfWeek == DayOfWeek.Friday).ToList()));
        }
    }
}