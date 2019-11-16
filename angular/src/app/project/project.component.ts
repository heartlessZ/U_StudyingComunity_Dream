import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { NzContextMenuService, NzDropdownMenuComponent } from 'ng-zorro-antd/dropdown';
import { Router, ActivatedRoute } from '@angular/router';
import { UserDetailService, ProjectService } from 'services';
import { CurrentUserDetailDto, ProjectDto, UserProjectDto } from 'entities';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectUserModalComponent } from './project-detail/project-user-modal/project-user-modal.component';
import { AppComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: [
    './project.component.css'
  ],
})
export class ProjectComponent extends AppComponentBase implements OnInit {
  @ViewChild('projectDetail', { static: true }) projectDetail: ProjectDetailComponent;
  @ViewChild('projectUserModal', { static: true }) projectUserModal: ProjectUserModalComponent;
  userProjectId: any;
  currentprojectUserDetailId: any;
  currentUserProjectTagName: any;

  constructor(injector: Injector
    , private router: Router
    , private userDetailService: UserDetailService
    , private actRouter: ActivatedRoute
    , private projectService: ProjectService) {

    super(injector);
    this.userProjectId = this.actRouter.snapshot.params['id'];
    this.currentprojectUserDetailId = this.actRouter.snapshot.params['userId'];
    this.currentUserProjectTagName = this.actRouter.snapshot.params['tagName'];
  }

  ngOnInit() {
    $("div#banner").removeClass('homepage-mid-read');
    $("div#banner").removeClass('homepage-mid-community');
    $("div#banner").removeClass('homepage-mid-personal');
    $("div#banner").removeClass('homepage-mid-learning');
    $("div#banner").removeClass('homepage-mid-library');
    $("div#banner").addClass('homepage-mid-learning');
    this.search = { userDetailId: null, maxResultCount: 10, skipCount: 0, isPublic: true };
    this.searchUserProject = { MaxResultCount: 5, SkipCount: 0 }

    this.getCurrentUser();
    this.getCurrentUserProjects();
    this.getUserProjectsPaged();
  }

  search: any = { userDetailId: null, maxResultCount: 10, skipCount: 0, isPublic: true };
  currentUser: CurrentUserDetailDto = new CurrentUserDetailDto();
  isLogin: boolean = false;
  headUrl: string = "";
  searchUserProject: any = { MaxResultCount: 5, SkipCount: 0 }

  isReload: boolean = false;
  loadingMore:boolean = false;

  currentProjectUserDetailId: any;

  userProjectTotalCount: number;
  pageIndex: number = 1;
  currentUserProjects: UserProjectDto[];//当前用户计划集合
  recommendUserProjects: UserProjectDto[];//推荐学习计划集合

  getCurrentUserProjects(): void {
    this.isReload = true;
    this.projectService.getCurrentUserProjectDtos(this.searchUserProject).subscribe((result) => {
      this.currentUserProjects = result.items;
      this.userProjectTotalCount = result.totalCount;
      //console.log(result);
      this.isReload = false;
    })
  }

  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      if (result.userId != undefined) {
        this.isLogin = true;
        this.currentUser = result;
        this.headUrl = this.userDetailService.baseUrl + result.headPortraitUrl;

        if (this.userProjectId != undefined) {
          this.projectDetail.currentUser = this.currentUser;
          this.projectDetail.id = this.userProjectId;
          this.projectDetail.currentprojectUserDetailId = this.currentprojectUserDetailId;
          this.projectDetail.currentUserProjectTagName = this.currentUserProjectTagName;
          this.projectDetail.getProjectsTreeById();
        }
      }
    })
  }

  pageIndexChange(): void {
    this.searchUserProject.SkipCount = (this.pageIndex - 1) * this.searchUserProject.MaxResultCount;
    this.getCurrentUserProjects();
  }

  getUserProjectsPaged(): void {
    this.projectService.getUserProjectsPaged(this.search).subscribe((result) => {
      this.recommendUserProjects = UserProjectDto.fromJSArray(result.items);
    })
  }

  //加载更多计划
  onLoadMore(): void {
    this.loadingMore = true;
    this.search.skipCount = this.search.maxResultCount * (this.search.skipCount + 1);
    this.projectService.getUserProjectsPaged(this.search).subscribe((result) => {
      this.recommendUserProjects = UserProjectDto.fromJSArray(result.items);
      
      this.loadingMore = false;
      if(result.items.length < this.search.maxResultCount){
        this.search.skipCount = -1;
      }
    })
  }

  refreshDetail(userProjectId: number, tagName: string, userDetailId: any): void {
    this.projectDetail.id = userProjectId;
    this.projectDetail.currentUserProjectTagName = tagName;
    this.projectDetail.currentprojectUserDetailId = userDetailId;
    this.projectDetail.getProjectsTreeById();
  }

  createProjectTag(): void {
    this.projectUserModal.show();
  }

  reload(): void {
    this.getCurrentUserProjects();
  }

}
