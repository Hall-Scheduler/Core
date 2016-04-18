namespace HallScheduler.Server.DataTransferModels.Halls
{
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class HallBriefDTO : IMapFrom<Hall>, IHaveCustomMappings
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

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Hall, HallBriefDTO>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Block, opts => opts.MapFrom(src => src.Block.ToString()))
                .ForMember(dest => dest.Stage, opts => opts.MapFrom(src => src.Stage.ToString()))
                .ForMember(dest => dest.Room, opts => opts.MapFrom(src => src.Room.ToString()))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.ToString()));
        }
    }
}