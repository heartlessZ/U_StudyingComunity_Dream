
using AutoMapper;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.UserDetails.Dtos;

namespace U_StudyingCommunity_Dream.UserDetails.Mapper
{

	/// <summary>
    /// 配置UserDetail的AutoMapper
    /// </summary>
	internal static class UserDetailMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <UserDetail,UserDetailListDto>();
            configuration.CreateMap <UserDetailListDto,UserDetail>();

            configuration.CreateMap <UserDetailEditDto,UserDetail>();
            configuration.CreateMap <UserDetail,UserDetailEditDto>();

        }
	}
}
