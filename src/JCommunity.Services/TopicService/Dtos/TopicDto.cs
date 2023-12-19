namespace JCommunity.Services.TopicService.Dtos;

public record TopicDto(
    string id,
	string name,
	string description,
	IEnumerable<TopicTag> tags,
	int sort,
	MemberDto author)
{
    internal class TopicDtoProfile : Profile
    {
        public TopicDtoProfile()
        {
            CreateMap<Topic, TopicDto>()
               .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.tags, opt => opt.MapFrom(src => src.Tags))
               .ForMember(dest => dest.author, opt => opt.MapFrom(src => src.Author));
        }
    }
}

