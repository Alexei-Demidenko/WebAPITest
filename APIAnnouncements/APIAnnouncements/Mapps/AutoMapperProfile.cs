using AutoMapper;
using BusinessLayer.DataTransferObject.AnnoncDTO;
using BusinessLayer.DataTransferObject.UserDTO;
using DataAccessLayer.Models;

namespace APIAnnouncements.Mapps
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRequestDto, User>()
                .ForMember(x => x.Id, e => e.Ignore())
                .ForMember(x => x.CreationDate, e => e.Ignore());

            CreateMap<User, UserResponseDto>();

            CreateMap<AnnoncCreateRequestDto, Announcing>()
                .ForMember(x => x.Id, e => e.Ignore())
                .ForMember("UserId", userId => userId.MapFrom(src => src.UserId));

            CreateMap<AnnoncUpdateRequestDto, Announcing>()
                .ForMember(x => x.Id, e => e.Ignore())
                .ForMember(x => x.CreationDate, e => e.Ignore())
                .ForMember(x => x.UserId, e => e.Ignore());

            CreateMap<Announcing, AnnoncResponseDto>();
        }
    }
}
