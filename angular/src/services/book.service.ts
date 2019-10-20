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
import { BookCategoryDto, SelectBookCategory } from "entities/book-category";
import { BookDetailDto } from "entities/book-detail";

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

  //获取所有树节点
  getBookCategoryList(): Observable<BookCategoryDto[]> {
    let url_ = "/api/services/app/BookCategory/GetNodes";
    return this._commonhttp.get(url_).pipe(map(data => {
      return BookCategoryDto.fromJSArray(data);
    }));
  }

  getBookCategoriesSelect(): Observable<SelectBookCategory[]> {
    let url_ = "/api/services/app/BookCategory/GetNodes";
    return this._commonhttp.get(url_).pipe(map(data => {
      return SelectBookCategory.fromJSArray(data);
    }));
  }

  //创建或更新书籍类别
  createOrUpdateCategory(name:string,parent:number,id?:number): Observable<boolean> {
    let url_ = "/api/services/app/BookCategory/CreateOrUpdate";
    
    var content = {bookCategory:{id:id,name:name,parent:parent}};
    console.log(content);
    
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  //通过Id获取书籍详情
  getBookDetailById(id: any): Observable<BookDetailDto> {
    let url_ = "/api/services/app/Book/GetById";
        return this._commonhttp.get(url_,{id:id}).pipe(map(data => {
            return BookDetailDto.fromJS(data);
        }));
  }

  //创建或更新书籍
  createOrUpdateBook(book:BookDetailDto): Observable<boolean> {
    let url_ = "/api/services/app/Book/CreateOrUpdate";
    
    var content = {book:book};
    console.log(content);
    //return new Observable();
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  //通过资源Id删除资源
  deleteBookResourceById(id:any):Observable<boolean>{
    let url_ = "/api/services/app/BookResource/Delete";
    return this._commonhttp.delete(url_, {id:id}).pipe(map(data => {
      return data;
    }));
  }
}
