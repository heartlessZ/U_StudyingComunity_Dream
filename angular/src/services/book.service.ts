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

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private _commonhttp: CommonHttpClient;
  baseUrl: string;

  constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
      , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
      this._commonhttp = commonhttp;
      this.baseUrl = baseUrl ? baseUrl : "";
  }


  //获取分页数据
  getBookListPaged(params: any): Observable<PagedResultDto> {
    let url_ = "/api/services/app/Book/GetPaged";
    return this._commonhttp.get(url_, params).pipe(map(data => {
        const result = new PagedResultDto();
        result.items = data.items;
        result.totalCount = data.totalCount;
        return result;
    }));
}
}
