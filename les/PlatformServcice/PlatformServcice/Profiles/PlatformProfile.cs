using AutoMapper;
using PlatformServcice.Dtos;
using PlatformServcice.Models;

namespace PlatformServcice.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}
