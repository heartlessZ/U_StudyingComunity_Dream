
using AutoMapper;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.UserDetails.Dtos;

namespace U_StudyingCommunity_Dream.UserDetails.Mapper
{

	/// <summary>
    /// 配置UserDetail_Project的AutoMapper
    /// </summary>
	internal static class UserDetail_ProjectMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <UserDetail_Project,UserDetail_ProjectListDto>();
            configuration.CreateMap <UserDetail_ProjectListDto,UserDetail_Project>();

            configuration.CreateMap <UserDetail_ProjectEditDto,UserDetail_Project>();
            configuration.CreateMap <UserDetail_Project,UserDetail_ProjectEditDto>();

        }
	}
}
