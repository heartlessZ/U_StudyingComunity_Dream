
using AutoMapper;
using U_StudyingCommunity_Dream.Articles;
using U_StudyingCommunity_Dream.Articles.Dtos;

namespace U_StudyingCommunity_Dream.Articles.Mapper
{

	/// <summary>
    /// 配置Comment的AutoMapper
    /// </summary>
	internal static class CommentMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Comment,CommentListDto>();
            configuration.CreateMap <CommentListDto,Comment>();

            configuration.CreateMap <CommentEditDto,Comment>();
            configuration.CreateMap <Comment,CommentEditDto>();

        }
	}
}
