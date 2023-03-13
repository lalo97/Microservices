using AutoMapper;
using PaltformService.DTOs;
using PaltformService.Models;

namespace PaltformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
            CreateMap<PlatformReadDTO, PlatformPublishDTO>();
        }
    }
}
