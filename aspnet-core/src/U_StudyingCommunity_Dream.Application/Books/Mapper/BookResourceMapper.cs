
using AutoMapper;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Books.Dtos;

namespace U_StudyingCommunity_Dream.Books.Mapper
{

	/// <summary>
    /// 配置BookResource的AutoMapper
    /// </summary>
	internal static class BookResourceMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BookResource,BookResourceListDto>();
            configuration.CreateMap <BookResourceListDto,BookResource>();

            configuration.CreateMap <BookResourceEditDto,BookResource>();
            configuration.CreateMap <BookResource,BookResourceEditDto>();

        }
	}
}
