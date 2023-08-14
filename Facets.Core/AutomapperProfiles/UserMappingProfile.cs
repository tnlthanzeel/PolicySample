using AutoMapper;
using Facets.Core.Security.Dtos;
using Facets.Core.Security.Entities;

namespace Facets.Core.AutomapperProfiles;

public sealed class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserSummaryDto>()
            .ForMember(d => d.FirstName, s => s.MapFrom(src => src.UserProfile.FirstName))
            .ForMember(d => d.LastName, s => s.MapFrom(src => src.UserProfile.LastName));
    }
}
