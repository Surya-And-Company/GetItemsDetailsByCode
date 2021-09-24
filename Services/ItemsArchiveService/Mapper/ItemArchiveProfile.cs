using System;
using AutoMapper;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.Mapper
{
    public class ItemArchiveProfile : Profile
    {
        public ItemArchiveProfile()
        { 
            CreateMap<CreateUserDTO, User>()
            .ForMember(dest => dest.CreatedDate,  opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LastUpdatedDate,  opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsDeleted,  opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.IsActive,  opt => opt.MapFrom(src => true));

            CreateMap<ItemDTO, Item>()
            .ForMember(dest => dest.IsApproved,  opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.CreatedDate,  opt => opt.MapFrom(src => DateTime.UtcNow));       

            CreateMap<ThirdPartyDTO, ThirdPartyAllowed>()
            .ForMember(dest => dest.RegistrationDate,  opt => opt.MapFrom(src => DateTime.UtcNow));        

        }
    }
}