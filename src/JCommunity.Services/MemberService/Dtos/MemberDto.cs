namespace JCommunity.Services.MemberService.Dtos;

public record MemberDto(string id, string name, string email, string nickName)
{
    internal class MemberDtoProfile : Profile
    {
        public MemberDtoProfile()
        {
            CreateMap<Member, MemberDto>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.nickName, opt => opt.MapFrom(src => src.NickName));
        }
    }
}
