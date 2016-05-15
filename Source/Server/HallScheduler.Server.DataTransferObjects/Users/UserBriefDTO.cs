namespace HallScheduler.Server.DataTransferObjects.Users
{
    using Data.Models;
    using Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class UserBriefDTO : IMapFrom<User>, IHaveCustomMappings
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserBriefDTO>()
                .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));
        }
    }
}
