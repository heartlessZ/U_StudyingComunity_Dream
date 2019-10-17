
using AutoMapper;
using U_StudyingCommunity_Dream.Books;
using U_StudyingCommunity_Dream.Books.Dtos;

namespace U_StudyingCommunity_Dream.Books.Mapper
{

	/// <summary>
    /// 配置BookCategory的AutoMapper
    /// </summary>
	internal static class BookCategoryMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BookCategory,BookCategoryListDto>();
            configuration.CreateMap <BookCategoryListDto,BookCategory>();

            configuration.CreateMap <BookCategoryEditDto,BookCategory>();
            configuration.CreateMap <BookCategory,BookCategoryEditDto>();

        }
	}
}
