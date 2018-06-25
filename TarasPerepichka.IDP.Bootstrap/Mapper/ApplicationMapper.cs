using TarasPerepichka.IDP.DataLayer.Entitties;
using TarasPerepichka.IDP.ViewModel;

namespace TarasPerepichka.IDP.Bootstrap.Mapper
{
    public class ApplicationMapper
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize((mapper) =>
            {
                mapper.CreateMap<OrderEntity, OrderVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Article, opt => opt.MapFrom(src => src.Article))
                .ReverseMap()
                .ForAllOtherMembers(o => o.Ignore());


                mapper.CreateMap<ArticleEntity, ArticleVM>(AutoMapper.MemberList.Destination)
                .ReverseMap();

            });
        }
    }
}
