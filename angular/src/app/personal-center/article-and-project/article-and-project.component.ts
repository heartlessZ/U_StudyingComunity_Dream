import { Component, OnInit, Injector, Input, Output, EventEmitter } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd';
import { ArticleService, ProjectService, UserDetailService } from 'services';
import { ArticleDetailDto, ArticleCategoryDto, UserProjectDto, UserDetailDto } from 'entities';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-article-and-project',
  templateUrl: './article-and-project.component.html',
  styleUrls: ['./article-and-project.component.css']
})
export class ArticleAndProjectComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  @Input() isCurrentUser: boolean;
  @Input() userDetail: UserDetailDto;

  initLoading = true; // bug
  loadingMore = false;
  data: ArticleDetailDto[] = [];
  list: Array<{ loading: boolean; name: any }> = [];
  search: any = { categoryId: null, maxResultCount: 10, skipCount: 0, releaseStatus: 2, userDetailId: null };

  tabs: ArticleCategoryDto[];
  totalCount: number;
  serverBaseUrl: string;
  userDetailId: string;

  baseUrl: string;

  constructor(injector: Injector,
    private articleService: ArticleService,
    private actRouter: ActivatedRoute,
    private userDetailService: UserDetailService,
    private router: Router,
    private projectService: ProjectService) {
    super(injector);

    this.userDetailId = this.actRouter.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.baseUrl = this.userDetailService.baseUrl;
    this.serverBaseUrl = this.articleService.baseUrl;
    this.search.maxResultCount = 5;
    this.search.skipCount = 0;
    //默认只查询审核通过的
    this.search.releaseStatus = 2;
    this.search.userDetailId = this.userDetailId;

    this.searchUserProject = { MaxResultCount: 5, SkipCount: 0 };
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

  refreshData(status: number) {
    this.loadingMore = true;
    this.search.releaseStatus = status;
    this.search.skipCount = 0;
    this.getArticleList();
  }

  //加载更多文章
  onLoadMore(): void {
    this.initLoading = true;
    this.loadingMore = true;
    this.search.skipCount = this.search.maxResultCount * (this.search.skipCount + 1);
    this.articleService.getArticlePaged(this.search).subscribe((result) => {
      this.totalCount = result.totalCount;
      let articles = ArticleDetailDto.fromJSArray(result.items);
      this.data.push(...articles)
      this.initLoading = false;
      if (result.items.length >= this.search.maxResultCount) {
        this.loadingMore = false;
      } else {
        this.loadingMore = true;
      }
    })
  }


  currentUserProjects: UserProjectDto[];//当前用户计划集合

  userProjectTotalCount: number;
  pageIndex: number = 1;
  searchUserProject: any = { MaxResultCount: 5, SkipCount: 0 }

  getCurrentUserProjects(): void {
    this.projectService.getCurrentUserProjectDtos(this.searchUserProject).subscribe((result) => {
      this.currentUserProjects = result.items;
      this.userProjectTotalCount = result.totalCount;
    })
  }

  searchUserProjectNotCurrentUser: any = { MaxResultCount: 5, SkipCount: 0, UserDetailId: null, IsPublic: true };
  pageIndexOfNotCurrentUser: number = 1;
  userProjectNotCurrentTotalCount: number;
  getUserProjectsByUserDetailId(): void {
    this.searchUserProjectNotCurrentUser.userDetailId = this.userDetail.id;
    this.projectService.getUserProjectsPaged(this.searchUserProjectNotCurrentUser).subscribe((result) => {
      this.currentUserProjects = result.items;
      this.userProjectNotCurrentTotalCount = result.totalCount;
    })
  }

  pageIndexChangeNotCurrentUser(): void {
    this.searchUserProjectNotCurrentUser.SkipCount = (this.pageIndex - 1) * this.searchUserProject.MaxResultCount;
    this.getUserProjectsByUserDetailId();
  }

  pageIndexChange(): void {
    this.searchUserProject.SkipCount = (this.pageIndex - 1) * this.searchUserProject.MaxResultCount;
    this.getCurrentUserProjects();
  }

  goProject(userProject: UserProjectDto): void {
    this.router.navigate(["app/project", { id: userProject.id, userId: userProject.userId, tagName: userProject.tagName }])
  }

  createArticle(): void {
    this.router.navigate(["app/personal-center/create-article"])
  }

  editArticle(id:any): void {
    this.router.navigate(["app/personal-center/create-article/"+id])
  }

  goArticleDetail(id: number): void {
    this.router.navigate(["app/community/article-detail/" + id])
  }
  goArticleDetailComment(id: number): void {
    this.router.navigate(["app/community/article-detail/" + id])
  }

  goUserDetail(userDetailId: any): void {
    this.router.navigate(["app/personal-center/" + userDetailId ])
    //console.log("哈哈哈哈");

    //this.modalSave.emit(null)
  }


  searchFans: any = { UserDetailId: 0, SkipCount: 0, MaxResultCount: 30 }
  searchAttention: any = { UserDetailId: 0, SkipCount: 0, MaxResultCount: 30 }
  userSimpleList: UserDetailDto[];
  pageIndexOfFansList: number = 1;
  fansListTotalCount: number;
  pageIndexOfAttentionList: number = 1;
  attentionListTotalCount: number;
  //获取粉丝列表
  getCurrentUserFans(): void {
    this.searchFans.UserDetailId = this.userDetail.id;
    this.userDetailService.getFansList(this.searchFans).subscribe((result) => {
      //console.log(result);
      this.userSimpleList = UserDetailDto.fromJSArray(result.items);
      this.fansListTotalCount = result.totalCount;
    })
  }

  pageIndexChangeOfFansList(): void {
    this.searchFans.SkipCount = (this.pageIndexOfFansList - 1) * this.searchFans.MaxResultCount;
    this.getCurrentUserFans();
  }

  //获取关注列表
  getCurrentAttentions(): void {
    this.searchAttention.UserDetailId = this.userDetail.id;
    this.userDetailService.getAttentionList(this.searchAttention).subscribe((result) => {
      //console.log(result);
      this.userSimpleList = UserDetailDto.fromJSArray(result.items);
      this.attentionListTotalCount = result.totalCount;
    })
  }

  
  pageIndexChangeOfAttentions(): void {
    this.searchAttention.SkipCount = (this.pageIndexOfAttentionList - 1) * this.searchAttention.MaxResultCount;
    this.getCurrentAttentions();
  }

}
