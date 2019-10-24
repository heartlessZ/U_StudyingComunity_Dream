import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService } from 'services';
import { ArticleDetailDto, ArticleCategoryDto } from 'entities';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

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
      this.search.maxResultCount = 10;
      this.search.skipCount = 0;
      //默认只查询审核通过的
      this.search.releaseStatus = 2;
      this.getArticleList();
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

    
  goArticleDetail(id:number):void{
    this.router.navigate(["app/community/article-detail/"+id])
  }
  
  goArticleDetailComment(id:number):void{
    this.router.navigate(["app/community/article-detail/"+id+"#comment"])
  }
}
