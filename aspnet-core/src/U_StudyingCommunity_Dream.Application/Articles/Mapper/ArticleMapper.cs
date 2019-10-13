
using AutoMapper;
using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Articles.Dtos;

namespace U_StudyingCommunity_Dream.Articles.Mapper
{

	/// <summary>
    /// 配置Article的AutoMapper
    /// </summary>
	internal static class ArticleMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Article,ArticleListDto>();
            configuration.CreateMap <ArticleListDto,Article>();

            configuration.CreateMap <ArticleEditDto,Article>();
            configuration.CreateMap <Article,ArticleEditDto>();

        }
	}
}
