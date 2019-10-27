import { Component, OnInit, Injector, Input } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService } from 'services';
import { ArticleDetailDto, ArticleCategoryDto } from 'entities';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-article-and-project',
  templateUrl: './article-and-project.component.html',
  styleUrls: ['./article-and-project.component.css']
})
export class ArticleAndProjectComponent extends AppComponentBase implements OnInit {
@Input() isCurrentUser;

  initLoading = true; // bug
  loadingMore = false;
  data: ArticleDetailDto[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  search: any = { categoryId: null, maxResultCount: 10, skipCount: 0 , releaseStatus: 2};
  tabs: ArticleCategoryDto[];
  totalCount: number;
  serverBaseUrl: string;
  userDetailId:string;

  constructor(injector: Injector,
    private articleService: ArticleService,
    private actRouter: ActivatedRoute,
    private router: Router, ) {
    super(injector);

    
    this.userDetailId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.serverBaseUrl = this.articleService.baseUrl;
    this.search.maxResultCount = 5;
    this.search.skipCount = 0;
    //默认只查询审核通过的
    this.search.releaseStatus = 2;
    this.getArticleList();
  }

  //获取文章
  getArticleList(): void {
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      this.totalCount = result.totalCount;
      this.data = ArticleDetailDto.fromJSArray(result.items);
      if (result.items.length > 0) {
        this.loadingMore = false;
      }
      this.initLoading = false;
    })
  }

  refreshData(status:number){
    this.loadingMore = true;
    this.search.releaseStatus = status;
    this.getArticleList();
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


  createArticle(): void {
    this.router.navigate(["app/create-article"])
  }

  goArticleDetail(id:number):void{
    this.router.navigate(["app/community/article-detail/"+id])
  }
  goArticleDetailComment(id:number):void{
    this.router.navigate(["app/community/article-detail/"+id])
  }

}
