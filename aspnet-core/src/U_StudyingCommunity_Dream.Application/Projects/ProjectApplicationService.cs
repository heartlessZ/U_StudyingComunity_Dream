
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Extensions;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;


using U_StudyingCommunity_Dream.Projects;
using U_StudyingCommunity_Dream.Projects.Dtos;
using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.Dtos;

namespace U_StudyingCommunity_Dream.Projects
{
    /// <summary>
    /// Project应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class ProjectAppService : U_StudyingCommunity_DreamAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Project, long> _entityRepository;
        private readonly IRepository<UserDetail_Project, long> _userProjectRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public ProjectAppService(
        IRepository<Project, long> entityRepository
        , IRepository<UserDetail_Project, long> userProjectRepository
        )
        {
            _entityRepository = entityRepository;
            _userProjectRepository = userProjectRepository;
        }


        /// <summary>
        /// 获取Project的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<ProjectListDto>> GetPaged(GetProjectsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			// var entityListDtos = ObjectMapper.Map<List<ProjectListDto>>(entityList);
			var entityListDtos =entityList.MapTo<List<ProjectListDto>>();

			return new PagedResultDto<ProjectListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取ProjectListDto信息
		/// </summary>
		 
		public async Task<ProjectListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<ProjectListDto>();
		}

		/// <summary>
		/// 获取编辑 Project
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetProjectForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetProjectForEditOutput();
ProjectEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<ProjectEditDto>();

				//projectEditDto = ObjectMapper.Map<List<projectEditDto>>(entity);
			}
			else
			{
				editDto = new ProjectEditDto();
			}

			output.Project = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改Project的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<long> CreateOrUpdate(CreateOrUpdateProjectInput input)
		{
            long result = 0;
			if (input.Project.Id.HasValue)
			{
				await Update(input.Project);
			}
			else
			{
				result = await Create(input.Project);
			}
            return result;
		}

        


		/// <summary>
		/// 新增Project
		/// </summary>
		
		protected virtual async Task<long> Create(ProjectEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map <Project>(input);
            var project = await _entityRepository.FirstOrDefaultAsync(p => p.Parent == input.Parent);
            var id = await _entityRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (input.Parent != 0)
            {
                if (project != null)
                {
                    project.Parent = id;
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            return id;
		}

		/// <summary>
		/// 编辑Project
		/// </summary>
		
		protected virtual async Task Update(ProjectEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
		}



		/// <summary>
		/// 删除Project信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除Project的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<ProjectListDto>> GetProjectListDtos()
        {
            var result = new List<ProjectListDto>();
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var userProjects = _userProjectRepository.GetAll().Where(p => p.UserId == user.UserDetailId);
                foreach (var item in userProjects)
                {
                    var dto = new ProjectListDto();
                    var project = await _entityRepository.GetAsync(item.ProjectId);
                    dto.isPublic = item.IsPublic;
                    dto.TagName = item.TagName;
                    dto.UserDetailId = item.UserId;
                    result.Add(ObjectMapper.Map(project, dto));
                }
                return result;
            }
            return null;
        }

        public async Task<ProjectTreeDto> GetProjectTreeById(EntityDto<long> input)
        {
            var result = new ProjectTreeDto();
            var userProject = await _userProjectRepository.GetAsync(input.Id);
            var project = await _entityRepository.FirstOrDefaultAsync(userProject.ProjectId);
            if (userProject.ProjectId == 0 || project == null)
            {
                return null;
            }
            result.Id = project.Id;
            result.ExpirationTime = project.ExpirationTime;
            result.Name = project.Name;
            result.Progress = project.Progress;
            result.Parent = project.Parent;
            result.Remark = project.Remark;
            result.ChildProject = await GetChildProject(result.Id);

            return result;
        }

        public async Task<ProjectTreeDto> GetChildProject(long parent)
        {
            var result = new ProjectTreeDto();
            var project = await _entityRepository.GetAll().FirstOrDefaultAsync(p => p.Parent == parent);
            if (project == null)
            {
                return null;
            }
            result.Id = project.Id;
            result.ExpirationTime = project.ExpirationTime;
            result.Name = project.Name;
            result.Progress = project.Progress;
            result.Parent = project.Parent;
            result.Remark = project.Remark;
            result.ChildProject = await GetChildProject(result.Id);
            return result;
        }
        
        /// <summary>
        /// 导出Project为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


