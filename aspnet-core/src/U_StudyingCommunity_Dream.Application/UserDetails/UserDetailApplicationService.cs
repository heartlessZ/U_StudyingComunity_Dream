
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
using Abp.ObjectMapping;
using U_StudyingCommunity_Dream.Authorization.Users;
using Abp.Runtime.Session;

namespace U_StudyingCommunity_Dream.UserDetails
{
    /// <summary>
    /// UserDetail应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class UserDetailAppService : U_StudyingCommunity_DreamAppServiceBase, IUserDetailAppService
    {
        private readonly IRepository<UserDetail, Guid> _entityRepository;
        private readonly UserManager _userManager;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public UserDetailAppService(
        IRepository<UserDetail, Guid> entityRepository,
        UserManager userManager
        )
        {
            _entityRepository = entityRepository;
            _userManager = userManager;
        }

        /// <summary>
        /// 获取当前登录用户详情
        /// </summary>
        /// <returns></returns>
        [AbpAllowAnonymous]
        public async Task<GetCurrentUserDto> GetCurrentUserDetailAsync()
        {
            if (AbpSession.UserId == null)
            {
                return null;
            }
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                return null;
            }
            var userDetail = _entityRepository.FirstOrDefaultAsync(user.UserDetailId);
            if (userDetail != null)
            {
                return new GetCurrentUserDto()
                {
                    UserId = user.Id,
                    Name = user.Name,
                    UserDetailId = userDetail.Result.Id,
                    Surname = user.Surname,
                    HeadPortraitUrl = userDetail.Result.HeadPortraitUrl,
                    IsAdmin = userDetail.Result.IsAdmin
                };
            }
            return new GetCurrentUserDto()
            {
                UserId = user.Id,
                Name = user.Name,
                UserDetailId = Guid.Empty,
                Surname = user.Surname,
                HeadPortraitUrl = null,
                IsAdmin = true
            };
        }

        /// <summary>
        /// 获取UserDetail的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<UserDetailListDto>> GetPaged(GetUserDetailsInput input)
		{

		    var query = _entityRepository.GetAll();
			// TODO:根据传入的参数添加过滤条件
            

			var count = await query.CountAsync();

			var entityList = await query
					.OrderBy(input.Sorting).AsNoTracking()
					.PageBy(input)
					.ToListAsync();

			 var entityListDtos = ObjectMapper.Map<List<UserDetailListDto>>(entityList);
			//var entityListDtos =entityList.MapTo<List<UserDetailListDto>>();

			return new PagedResultDto<UserDetailListDto>(count,entityListDtos);
		}


		/// <summary>
		/// 通过指定id获取UserDetailListDto信息
		/// </summary>
		 
		public async Task<UserDetailListDto> GetById(EntityDto<Guid> input)
		{
			var entity = await _entityRepository.GetAsync(input.Id);

		    var result = ObjectMapper.Map<UserDetailListDto>(entity);
            var user = await UserManager.GetUserByIdAsync(result.UserId);
            result.Name = user.Name;
            return result;
		}

		/// <summary>
		/// 获取编辑 UserDetail
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<GetUserDetailForEditOutput> GetForEdit(NullableIdDto<Guid> input)
		{
			var output = new GetUserDetailForEditOutput();
UserDetailEditDto editDto;

			if (input.Id.HasValue)
			{
				var entity = await _entityRepository.GetAsync(input.Id.Value);

				editDto = entity.MapTo<UserDetailEditDto>();

				//userDetailEditDto = ObjectMapper.Map<List<userDetailEditDto>>(entity);
			}
			else
			{
				editDto = new UserDetailEditDto();
			}

			output.UserDetail = editDto;
			return output;
		}


		/// <summary>
		/// 添加或者修改UserDetail的公共方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task<bool> CreateOrUpdate(CreateOrUpdateUserDetailInput input)
		{

			if (input.UserDetail.Id.HasValue)
			{
				await Update(input.UserDetail);
			}
			else
			{
                //await Create(input.UserDetail);
                throw new UserFriendlyException("未找到该用户。");

            }
            return true;
		}


		/// <summary>
		/// 新增UserDetail
		/// </summary>
		
		protected virtual async Task<UserDetailEditDto> Create(UserDetailEditDto input)
		{
			//TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <UserDetail>(input);
            var entity=input.MapTo<UserDetail>();
			

			entity = await _entityRepository.InsertAsync(entity);
			return entity.MapTo<UserDetailEditDto>();
		}

		/// <summary>
		/// 编辑UserDetail
		/// </summary>
		
		protected virtual async Task Update(UserDetailEditDto input)
		{
			//TODO:更新前的逻辑判断，是否允许更新

			var entity = await _entityRepository.GetAsync(input.Id.Value);
			//input.MapTo(entity);

			ObjectMapper.Map(input, entity);
		    await _entityRepository.UpdateAsync(entity);
		}



		/// <summary>
		/// 删除UserDetail信息的方法
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		
		public async Task Delete(EntityDto<Guid> input)
		{
			//TODO:删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(input.Id);
		}



		/// <summary>
		/// 批量删除UserDetail的方法
		/// </summary>
		
		public async Task BatchDelete(List<Guid> input)
		{
			// TODO:批量删除前的逻辑判断，是否允许删除
			await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
		}


		/// <summary>
		/// 导出UserDetail为excel表,等待开发。
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


