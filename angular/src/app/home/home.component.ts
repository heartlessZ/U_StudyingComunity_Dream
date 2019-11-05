import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService, BookService, UserDetailService } from 'services';
import { ArticleDetailDto, ArticleCategoryDto, UserDetailDto, BookDetailDto } from 'entities';
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
  search: any = { categoryId: null, maxResultCount: 10, skipCount: 0, releaseStatus: 2 };
  tabs: ArticleCategoryDto[];

  selectUser:any;
  selectBook:any;

  userSimpleInfos: UserDetailDto[];
  bookSimpleInfos: BookDetailDto[];

  totalCount: number;
  serverBaseUrl: string;
  constructor(private articleService: ArticleService
    , private bookService: BookService
    , private userDetailService: UserDetailService
    , private msg: NzMessageService
    , private router: Router) { }

  ngOnInit(): void {
    this.serverBaseUrl = this.articleService.baseUrl;
    this.search.maxResultCount = 10;
    this.search.skipCount = 0;
    //默认只查询审核通过的
    this.search.releaseStatus = 2;
    this.getArticleList();
    this.getUserSimpleInfos();
    this.getBookSimpleInfos();
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

  getUserSimpleInfos():void{
    this.userDetailService.getUserSimpleInfo(this.selectUser).subscribe((result)=>{
      this.userSimpleInfos = result;
    })
  }

  getBookSimpleInfos():void{
    this.bookService.getBookSimpleInfo(this.selectBook).subscribe((result)=>{
      this.bookSimpleInfos = result;
    })
  }


  goArticleDetail(id: number): void {
    this.router.navigate(["app/community/article-detail/" + id])
  }

  goArticleDetailComment(id: number): void {
    this.router.navigate(["app/community/article-detail/" + id])
  }

  goUserDetail(userDetailId:any):void{
    console.log(userDetailId);
    
    this.router.navigate(["app/personal-center/"+userDetailId])
  }

  goBookDetail(bookId:any):void{
    this.router.navigate(["app/library/book-detail/"+bookId])
  }
}
