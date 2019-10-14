import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";
//import { ApiResult, DocumentDto, Clause, Employee, Attachment, DocAttachment } from "../entities";
//import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { PagedResultDto } from "@shared/component-base/index"
import { CurrentUserDetailDto } from "entities";

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
  getAll(params: any): Observable<PagedResultDto> {
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


}
