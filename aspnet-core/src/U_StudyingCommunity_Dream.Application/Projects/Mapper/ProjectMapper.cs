
using AutoMapper;
using U_StudyingCommunity_Dream.Projects;
using U_StudyingCommunity_Dream.Projects.Dtos;

namespace U_StudyingCommunity_Dream.Projects.Mapper
{

	/// <summary>
    /// 配置Project的AutoMapper
    /// </summary>
	internal static class ProjectMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Project,ProjectListDto>();
            configuration.CreateMap <ProjectListDto,Project>();

            configuration.CreateMap <ProjectEditDto,Project>();
            configuration.CreateMap <Project,ProjectEditDto>();

        }
	}
}
