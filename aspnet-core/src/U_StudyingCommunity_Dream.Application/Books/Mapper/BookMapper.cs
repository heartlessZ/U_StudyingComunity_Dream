
using AutoMapper;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Books.Dtos;

namespace U_StudyingCommunity_Dream.Books.Mapper
{

	/// <summary>
    /// 配置Book的AutoMapper
    /// </summary>
	internal static class BookMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Book,BookListDto>();
            configuration.CreateMap <BookListDto,Book>();

            configuration.CreateMap <BookEditDto,Book>();
            configuration.CreateMap <Book,BookEditDto>();

        }
	}
}
