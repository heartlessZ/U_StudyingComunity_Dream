import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService } from 'services';
import { ArticleDetailDto, ArticleCategoryDto } from 'entities';
import { Router } from '@angular/router';

@Component({
  selector: 'app-community',
  templateUrl: './community.component.html',
  styleUrls: ['./community.component.css']
})
export class CommunityComponent implements OnInit {

  initLoading = true; // bug
  loadingMore = false;
  data: ArticleDetailDto[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  search: any = { categoryId: null, maxResultCount: 10, skipCount: 0 , releaseStatus: 2};
  tabs: ArticleCategoryDto[];
  totalCount: number;
  serverBaseUrl: string;

  constructor(private articleService: ArticleService
    , private msg: NzMessageService
    , private router: Router) { }

  ngOnInit(): void {
    this.serverBaseUrl = this.articleService.baseUrl;
    this.search.categoryId = 1;
    this.search.maxResultCount = 10;
    this.search.skipCount = 0;
    //默认只查询审核通过的
    this.search.releaseStatus = 2;
    this.getArticleList();
    this.getAllTags();
  }

  //获取所有的标签
  getAllTags(): void {
    this.articleService.getAllTags().subscribe((result) => {
      this.tabs = result;
    })
  }

  //获取文章
  getArticleList(): void {
    this.loadingMore = true;
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      this.totalCount = result.totalCount;
      this.data = ArticleDetailDto.fromJSArray(result.items);
      if (result.items.length > 0) {
        this.loadingMore = false;
      }
      this.initLoading = false;
      console.log(result);
    })
  }

  //加载更多文章
  onLoadMore(): void {
    this.loadingMore = true;
    this.search.skipCount = this.search.maxResultCount * (this.search.skipCount + 1);
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      this.totalCount = result.totalCount;
      let articles = ArticleDetailDto.fromJSArray(result.items);
      this.data.push(...articles)
      if (result.items.length > 0) {
        this.loadingMore = false;
      }
    })
  }

  //切换标签页时刷新文章列表
  refreshData(categoryId?: number) {
    this.search.categoryId = categoryId;
    this.search.skipCount = 0;
    this.getArticleList();
  }

  goToArticleDetail(id:number){
    console.log("文章详情");
    
    this.router.navigate(["app/article-detail/" + id]);
  }
}
