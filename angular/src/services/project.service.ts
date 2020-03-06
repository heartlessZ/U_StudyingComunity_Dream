import { Inject, Optional, Injectable } from '@angular/core';
import { Observer, Observable } from 'rxjs';
import { CommonHttpClient } from 'services/common-httpclient';
import { map } from 'rxjs/operators';
import { NzTreeNode } from 'ng-zorro-antd';
// import { ApiResult, DocumentDto, Clause, Employee, Attachment, DocAttachment } from "../entities";
// import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';
import { PagedResultDto } from '@shared/component-base/index';
import { CurrentUserDetailDto, UserDetailDto, BookResourceDto, ArticleCategoryDto, SelectArticleCategoryDto, ArticleDetailDto, CommentCreate, ProjectDto, UserProjectDto } from 'entities';
import { BookCategoryDto, SelectBookCategory } from 'entities/book-category';
import { BookDetailDto } from 'entities';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  baseUrl: string;
  private _commonhttp: CommonHttpClient;

  constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
    , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this._commonhttp = commonhttp;
    this.baseUrl = baseUrl ? baseUrl : '';
  }


  // 修改或更新计划
  createOrUpdateProject(project: ProjectDto): Observable<number> {
    const url_ = '/api/services/app/Project/CreateOrUpdate';

    const content = {Project: project};
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  // 获取当前登录用户的学习计划标签
  getProjectTreeById(id: any): Observable<ProjectDto> {
    const url_ = '/api/services/app/Project/GetProjectTreeById';
    return this._commonhttp.get(url_, {id: id}).pipe(map(data => {
      return ProjectDto.fromJS(data);
    }));
  }

  // 修改或更新计划标签
  createOrUpdateUserProject(project: UserProjectDto): Observable<boolean> {
    const url_ = '/api/services/app/UserDetail_Project/CreateOrUpdate';

    const content = {UserDetail_Project: project};
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }


  // 获取当前登录用户的学习计划
  getCurrentUserProjectDtos(params: any): Observable<PagedResultDto> {
    const url_ = '/api/services/app/UserDetail_Project/GetCurrentUserProjects';
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  getUserProjectById(id: any): Observable<UserProjectDto> {
    const url_ = '/api/services/app/UserDetail_Project/GetById';
    return this._commonhttp.get(url_, {id: id}).pipe(map(data => {
      return UserProjectDto.fromJS(data);
    }));
  }

  editUserProjectProId(userProjectId: number, projectId: number): Observable<boolean> {
    const url_ = '/api/services/app/UserDetail_Project/EditUserProjectProId';

    const content = {UserProjectId: userProjectId, ProjectId: projectId};
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  getProjectById(id: any): Observable<ProjectDto> {
    const url_ = '/api/services/app/Project/GetById';
    return this._commonhttp.get(url_, {id: id}).pipe(map(data => {
      return ProjectDto.fromJS(data);
    }));
  }

  getUserProjectsPaged(params: any): Observable<PagedResultDto> {
    const url_ = '/api/services/app/UserDetail_Project/GetPaged';
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  dropUserProjectById(id: any): Observable<boolean> {
    const url_ = '/api/services/app/UserDetail_Project/DropUserProjectById';

    return this._commonhttp.post(url_, {id: id}).pipe(map(data => {
      return data;
    }));
  }

  deleteProjectById(id: any): Observable<boolean> {
    const url_ = '/api/services/app/Project/Delete';

    return this._commonhttp.delete(url_, {id: id}).pipe(map(data => {
      return data;
    }));
  }
}
