
using AutoMapper;
using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Articles.Dtos;

namespace U_StudyingCommunity_Dream.Articles.Mapper
{

	/// <summary>
    /// 配置ArticleCategory的AutoMapper
    /// </summary>
	internal static class ArticleCategoryMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ArticleCategory,ArticleCategoryListDto>();
            configuration.CreateMap <ArticleCategoryListDto,ArticleCategory>();

            configuration.CreateMap <ArticleCategoryEditDto,ArticleCategory>();
            configuration.CreateMap <ArticleCategory,ArticleCategoryEditDto>();

        }
	}
}
