using AutoMapper;
using Domain;
using WEB.ViewModels;

namespace WEB.Infrastructure.MappingProfiles;

/// <inheritdoc/>
public class PlayerMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PlayerMappingProfile()
    {
        CreateMap<PlayerViewModel, Player>()
            .ForMember(m=>m.Team,
                opts=>opts.MapFrom(vm=>new Team { Name = vm.TeamName }));
    }
}
