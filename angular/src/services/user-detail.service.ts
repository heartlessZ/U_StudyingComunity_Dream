import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";
//import { ApiResult, DocumentDto, Clause, Employee, Attachment, DocAttachment } from "../entities";
//import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { PagedResultDto } from "@shared/component-base/index"
import { CurrentUserDetailDto, UserDetailDto } from "entities";

@Injectable()
export class UserDetailService {

    private _commonhttp: CommonHttpClient;
    baseUrl: string;

    constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
        , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this._commonhttp = commonhttp;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    //获取分页数据
    getUserListPaged(params: any): Observable<PagedResultDto> {
        let url_ = "/api/services/app/UserDetail/GetPaged";
        return this._commonhttp.get(url_, params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //获取当前登录用户基本信息
    getCurrentUserSimpleInfo(): Observable<CurrentUserDetailDto> {
        let url_ = "/api/services/app/UserDetail/GetCurrentUserDetailAsync";
        return this._commonhttp.get(url_).pipe(map(data => {
            return CurrentUserDetailDto.fromJS(data);
        }));
    }

    //获取当前登录用户基本信息
    getUserDetailById(id:any): Observable<UserDetailDto> {
        let url_ = "/api/services/app/UserDetail/GetById";
        return this._commonhttp.get(url_,{id:id}).pipe(map(data => {
            return UserDetailDto.fromJS(data);
        }));
    }

    //修改密码
    changePassword(pwdDto:any):Observable<boolean>{
        let url_ = "/api/services/app/User/ChangePassword";
        return this._commonhttp.post(url_,{pwdDto:pwdDto}).pipe(map(data => {
            return data;
        }));
    }

    editUserDetail(userDetail:UserDetailDto):Observable<boolean>{
        let url_ = "/api/services/app/UserDetail/CreateOrUpdate";
        let content=JSON.stringify(userDetail);
        return this._commonhttp.post(url_,{userDetail:userDetail}).pipe(map(data => {
            return data;
        }));
    }

    updateUserStatus(id:any):Observable<boolean>{
        let url_ = "/api/services/app/UserDetail/GetUpdateUserStatus";
        return this._commonhttp.get(url_,{id:id}).pipe(map(data => {
            return data;
        }));
    }

    getUserSimpleInfo(keyword:any):Observable<UserDetailDto[]>{
        let url_ = "/api/services/app/UserDetail/GetUserSimpleInfos";
        return this._commonhttp.get(url_,{keyword}).pipe(map(data => {
            return UserDetailDto.fromJSArray(data);
        }));
    }


    ///关注，粉丝
    //获取当前用户是否已经关注当前浏览用户
    getIsAttentionUser(userId:any,fansId:any):Observable<boolean>{
        let url_ = "/api/services/app/UserDetail/GetIsAttentionUser";
        return this._commonhttp.get(url_,{userId:userId,fansId:fansId}).pipe(map(data => {
            return data;
        }));
    }

    //获取关注列表
    getAttentionList(params:any):Observable<PagedResultDto>{
        let url_ = "/api/services/app/UserDetail/GetAttentionList";
        return this._commonhttp.get(url_,params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //获取粉丝列表
    getFansList(params:any):Observable<PagedResultDto>{
        let url_ = "/api/services/app/UserDetail/GetFansList";
        return this._commonhttp.get(url_,params).pipe(map(data => {
            const result = new PagedResultDto();
            result.items = data.items;
            result.totalCount = data.totalCount;
            return result;
        }));
    }

    //取消关注
    deleteAttentionRecord(userId:any,fansId:any):Observable<boolean>{
        let url_ = "/api/services/app/UserDetail/CancelAttentionRecord";
        return this._commonhttp.post(url_,{userId:userId,fansId:fansId}).pipe(map(data => {
            return data;
        }));
    }

    //添加关注
    createAttentionRecord(userId:any,fansId:any):Observable<boolean>{
        let url_ = "/api/services/app/UserDetail/CreateAttentionRecord";
        return this._commonhttp.post(url_,{userId:userId,fansId:fansId}).pipe(map(data => {
            return data;
        }));
    }
}
