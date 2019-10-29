
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


using U_StudyingCommunity_Dream.UserDetails;
using U_StudyingCommunity_Dream.UserDetails.Dtos;




namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// UserDetail_Project应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class UserDetail_ProjectAppService : U_StudyingCommunity_DreamAppServiceBase, IUserDetail_ProjectAppService
    {
        private readonly IRepository<UserDetail_Project, long> _entityRepository;

        

        /// <summary>
        /// 构造函数 
        ///</summary>
        public UserDetail_ProjectAppService(
        IRepository<UserDetail_Project, long> entityRepository
        
        )
        {
            _entityRepository = entityRepository; 
            
        }


        /// <summary>
        /// 获取UserDetail_Project的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
		 
        public async Task<PagedResultDto<UserDetail_ProjectListDto>> GetPaged(GetUserDetail_ProjectsInput input)
		{
            var user = await GetCurrentUserAsync();
		    var query = _entityRepository.GetAll()
                .Where(i=>i.UserId!=user.UserDetailId)
                .WhereIf(input.UserDetailId.HasValue,u=>u.UserId == input.UserDetailId.Value)
                .WhereIf(input.IsPublic.HasValue,u=>u.IsPublic==input.IsPublic.Value);
			// TODO:根据传入的参数添加过滤条件
            
			var count = await query.CountAsync();

			var entityList = await query
					.OrderByDescending(u=>u.Praise).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			var entityListDtos = ObjectMapper.Map<List<UserDetail_ProjectListDto>>(entityList);
			//var entityListDtos =entityList.MapTo<List<UserDetail_ProjectListDto>>();

			return new PagedResultDto<UserDetail_ProjectListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取UserDetail_ProjectListDto信息
		/// </summary>
		 
		public async Task<UserDetail_ProjectListDto> GetById(EntityDto<long> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    return entity.MapTo<UserDetail_ProjectListDto>();
		}

		/// <summary>
		/// 获取编辑 UserDetail_Project
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetUserDetail_ProjectForEditOutput> GetForEdit(NullableIdDto<long> input)
		{
			var output = new GetUserDetail_ProjectForEditOutput();
UserDetail_ProjectEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<UserDetail_ProjectEditDto>();

				//userDetail_ProjectEditDto = ObjectMapper.Map<List<userDetail_ProjectEditDto>>(entity);
			}
			else
			{
				editDto = new UserDetail_ProjectEditDto();
			}

			output.UserDetail_Project = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改UserDetail_Project的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task CreateOrUpdate(CreateOrUpdateUserDetail_ProjectInput input)
		{

			if (input.UserDetail_Project.Id.HasValue)
			{
				await Update(input.UserDetail_Project);
			}
			else
			{
				await Create(input.UserDetail_Project);
			}
		}

        public async Task<bool> EditUserProjectProId(EditUserProjectProIdDto input)
        {
            var userProject = await _entityRepository.GetAsync(input.UserProjectId);
            userProject.ProjectId = input.ProjectId;
            await CurrentUnitOfWork.SaveChangesAsync();
            return true;
        }


		/// <summary>
		/// 新增UserDetail_Project
		/// </summary>
		
		protected virtual async Task<UserDetail_ProjectEditDto> Create(UserDetail_ProjectEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            var entity = ObjectMapper.Map <UserDetail_Project>(input);
            //var entity=input.MapTo<UserDetail_Project>();
			
			entity = await _entityRepository.InsertAsync(entity);
			return input;
		}

		/// <summary>
		/// 编辑UserDetail_Project
		/// </summary>
		
		protected virtual async Task Update(UserDetail_ProjectEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			//input.MapTo(entity);

		    ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除UserDetail_Project信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<long> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除UserDetail_Project的方法
		/// </summary>
		
		public async Task BatchDelete(List<long> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}

        public async Task<List<UserDetail_ProjectListDto>> GetCurrentUserProjects()
        {
            var result = new List<UserDetail_ProjectListDto>();
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var userProjects = _entityRepository.GetAll().Where(p => p.UserId == user.UserDetailId);
                return ObjectMapper.Map<List<UserDetail_ProjectListDto>>(userProjects);
            }
            return null;
        }

        /// <summary>
        /// 导出UserDetail_Project为excel表,等待开发。
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


