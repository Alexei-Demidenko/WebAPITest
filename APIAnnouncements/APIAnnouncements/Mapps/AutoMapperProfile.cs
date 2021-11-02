using APIAnnouncements.dbo;
using APIAnnouncements.Models;
using AutoMapper;

namespace APIAnnouncements.Mapps
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<UserRequest, User>()
				.ForMember(x => x.Id, e => e.Ignore())
				.ForMember(x=>x.CreationDate, e=> e.Ignore());

			CreateMap<User, UserResponse>();

			CreateMap<AnnoncRequest, Announcing>()
				.ForMember(x => x.Id, e => e.Ignore());

			CreateMap<Announcing, AnnoncResponse>();
		}
	}
}
