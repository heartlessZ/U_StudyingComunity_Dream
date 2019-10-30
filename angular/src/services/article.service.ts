import { Inject, Optional, Injectable } from "@angular/core";
import { Observer, Observable } from "rxjs";
import { CommonHttpClient } from "services/common-httpclient";
import { map } from "rxjs/operators";
import { NzTreeNode } from "ng-zorro-antd";
//import { ApiResult, DocumentDto, Clause, Employee, Attachment, DocAttachment } from "../entities";
//import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from "@shared/service-proxies/service-proxies";
import { PagedResultDto } from "@shared/component-base/index"
import { CurrentUserDetailDto, UserDetailDto, BookResourceDto, ArticleCategoryDto, SelectArticleCategoryDto, ArticleDetailDto, CommentCreate } from "entities";
import { BookCategoryDto, SelectBookCategory } from "entities/book-category";
import { BookDetailDto } from "entities";

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  private _commonhttp: CommonHttpClient;
  baseUrl: string;

  constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
    , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this._commonhttp = commonhttp;
    this.baseUrl = baseUrl ? baseUrl : "";
  }

  //获取所有标签
  getAllArticleCategories(): Observable<ArticleCategoryDto[]> {
    let url_ = "/api/services/app/ArticleCategory/GetAllTags";
    return this._commonhttp.get(url_).pipe(map(data => {
      return ArticleCategoryDto.fromJSArray(data);
    }));
  }

  createOrUpdateArticle(article:ArticleDetailDto):Observable<boolean>{
    let url_ = "/api/services/app/Article/CreateOrUpdate";
    
    var content = {Article:article};
    console.log(content);
    
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  getArticlePaged(params : any):Observable<PagedResultDto>{
    let url_ = "/api/services/app/Article/GetPaged";
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  getAllTags():Observable<ArticleCategoryDto[]>{
    let url_ = "/api/services/app/ArticleCategory/GetAllTags";
    return this._commonhttp.get(url_).pipe(map(data => {
      return ArticleCategoryDto.fromJSArray(data);
    }));
  }

  getArticleById(id : any):Observable<ArticleDetailDto>{
    let url_ = "/api/services/app/Article/GetById";
    return this._commonhttp.get(url_, {id:id}).pipe(map(data => {
      return ArticleDetailDto.fromJS(data);
    }));
  }

  getCommentsByArticleId(params : any):Observable<PagedResultDto>{
    let url_ = "/api/services/app/Comment/GetCommentsByArticleId";
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  createComment(comment:CommentCreate):Observable<boolean>{
    let url_ = "/api/services/app/Comment/CreateOrUpdate";
    
    var content = {Comment:comment};
    
    return this._commonhttp.post(url_, content).pipe(map(data => {
      return true;
    }));
  }

  updateArticleStatus(articleId:number,status:number):Observable<boolean>{
    let url_ = "/api/services/app/Article/AduitArticleStatus";
    
    return this._commonhttp.post(url_, {articleId:articleId,status:status}).pipe(map(data => {
      return data;
    }));
  }

  createVisitVolume(id:any):Observable<void>{
    let url_ = "/api/services/app/Article/CreateVisitVolume";
    
    return this._commonhttp.post(url_, {id:id}).pipe(map(data => {
      
    }));
  }

  createPraise(id:any):Observable<boolean>{
    let url_ = "/api/services/app/Article/CreatePraise";
    
    return this._commonhttp.post(url_, {id:id}).pipe(map(data => {
      return data;
    }));
  }

}
