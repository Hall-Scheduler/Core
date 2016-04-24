namespace HallScheduler.Server.DataTransferObjects.Halls
{
    using AutoMapper;
    using Data.Models;
    using Events;
    using Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HallDetailedDTO : IMapFrom<Hall>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Block { get; set; }

        [Required]
        public string Stage { get; set; }

        [Required]
        public string Room { get; set; }

        public virtual ICollection<EventDTM> Schedule { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Hall, HallDetailedDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Block, opts => opts.MapFrom(src => src.Block.ToString()))
                .ForMember(dest => dest.Stage, opts => opts.MapFrom(src => src.Stage.ToString()))
                .ForMember(dest => dest.Room, opts => opts.MapFrom(src => src.Room.ToString()))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Schedule, opts => opts.MapFrom(src => src.Schedule));
        }
    }
}
