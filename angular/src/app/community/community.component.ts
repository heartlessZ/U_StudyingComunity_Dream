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
  search: any = { categoryId: null, maxResultCount: 10, skipCount: 0, releaseStatus: 2 };
  tabs: ArticleCategoryDto[];
  totalCount: number;
  serverBaseUrl: string;

  constructor(private articleService: ArticleService
    , private msg: NzMessageService
    , private router: Router) { }

  ngOnInit(): void {
    $("div#banner").removeClass('homepage-mid-read');
    $("div#banner").removeClass('homepage-mid-community');
    $("div#banner").removeClass('homepage-mid-personal');
    $("div#banner").removeClass('homepage-mid-learning');
    $("div#banner").removeClass('homepage-mid-library');
    $("div#banner").addClass('homepage-mid-community');
    this.serverBaseUrl = this.articleService.baseUrl;
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
    this.initLoading = true;
    this.loadingMore = true;
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      this.totalCount = result.totalCount;
      this.data = ArticleDetailDto.fromJSArray(result.items);
      if (result.items.length > 0) {
        this.loadingMore = false;
      }
      this.initLoading = false;
      //console.log(result);
    })
  }

  //加载更多文章
  onLoadMore(): void {
    this.initLoading = true;
    this.loadingMore = true;
    this.search.skipCount = this.search.maxResultCount * (this.search.skipCount + 1);
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      if (result.items.length > 0) {
        let articles = ArticleDetailDto.fromJSArray(result.items);
        this.data.push(...articles)
        this.initLoading = false;

        if (result.items.length >= this.search.maxResultCount) {
          this.loadingMore = false;
        } else {
          this.loadingMore = true;
        }
      }
    })
  }

  //切换标签页时刷新文章列表
  refreshData(categoryId?: number) {
    this.loadingMore = false;
    this.search.categoryId = categoryId;
    this.search.skipCount = 0;
    this.getArticleList();
  }

  goToArticleDetail(id: number) {
    this.router.navigate(["app/community/article-detail/" + id]);
  }
}
