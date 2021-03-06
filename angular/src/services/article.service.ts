import { Inject, Optional, Injectable } from '@angular/core';
import { Observer, Observable } from 'rxjs';
import { CommonHttpClient } from 'services/common-httpclient';
import { map } from 'rxjs/operators';
import { NzTreeNode } from 'ng-zorro-antd';
// import { ApiResult, DocumentDto, Clause, Employee, Attachment, DocAttachment } from "../entities";
// import { PagedResultDto } from "@shared/component-base";
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';
import { PagedResultDto } from '@shared/component-base/index';
import { CurrentUserDetailDto, UserDetailDto, BookResourceDto, ArticleCategoryDto, SelectArticleCategoryDto, ArticleDetailDto, CommentCreate } from 'entities';
import { BookCategoryDto, SelectBookCategory } from 'entities/book-category';
import { BookDetailDto } from 'entities';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  baseUrl: string;

  private _commonhttp: CommonHttpClient;

  constructor(@Inject(CommonHttpClient) commonhttp: CommonHttpClient
    , @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this._commonhttp = commonhttp;
    this.baseUrl = baseUrl ? baseUrl : '';
  }

  // 获取所有标签
  getAllArticleCategories(): Observable<ArticleCategoryDto[]> {
    const url_ = '/api/services/app/ArticleCategory/GetAllTags';
    return this._commonhttp.get(url_).pipe(map(data => {
      return ArticleCategoryDto.fromJSArray(data);
    }));
  }

  createOrUpdateArticle(article: ArticleDetailDto): Observable<boolean> {
    const url_ = '/api/services/app/Article/CreateOrUpdate';

    const content = {Article: article};
    // console.log(content);

    return this._commonhttp.post(url_, content).pipe(map(data => {
      return data;
    }));
  }

  getArticlePaged(params: any): Observable<PagedResultDto> {
    const url_ = '/api/services/app/Article/GetPaged';
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  getAllTags(): Observable<ArticleCategoryDto[]> {
    const url_ = '/api/services/app/ArticleCategory/GetAllTags';
    return this._commonhttp.get(url_).pipe(map(data => {
      return ArticleCategoryDto.fromJSArray(data);
    }));
  }

  getArticleById(id: any): Observable<ArticleDetailDto> {
    const url_ = '/api/services/app/Article/GetById';
    return this._commonhttp.get(url_, {id: id}).pipe(map(data => {
      return ArticleDetailDto.fromJS(data);
    }));
  }

  getCommentsByArticleId(params: any): Observable<PagedResultDto> {
    const url_ = '/api/services/app/Comment/GetCommentsByArticleId';
    return this._commonhttp.get(url_, params).pipe(map(data => {
      const result = new PagedResultDto();
      result.items = data.items;
      result.totalCount = data.totalCount;
      return result;
    }));
  }

  createComment(comment: CommentCreate): Observable<boolean> {
    const url_ = '/api/services/app/Comment/CreateOrUpdate';

    const content = {Comment: comment};

    return this._commonhttp.post(url_, content).pipe(map(data => {
      return true;
    }));
  }

  deleteComment(id: any): Observable<boolean> {
    const url_ = '/api/services/app/Comment/Delete';

    return this._commonhttp.delete(url_, {id: id}).pipe(map(data => {
      return true;
    }));
  }

  updateArticleStatus(articleId: number, status: number): Observable<boolean> {
    const url_ = '/api/services/app/Article/AduitArticleStatus';

    return this._commonhttp.post(url_, {articleId: articleId, status: status}).pipe(map(data => {
      return data;
    }));
  }

  createVisitVolume(id: any): Observable<void> {
    const url_ = '/api/services/app/Article/CreateVisitVolume';

    return this._commonhttp.post(url_, {id: id}).pipe(map(data => {

    }));
  }

  createPraise(id: any): Observable<boolean> {
    const url_ = '/api/services/app/Article/CreatePraise';

    return this._commonhttp.post(url_, {id: id}).pipe(map(data => {
      return data;
    }));
  }

}
