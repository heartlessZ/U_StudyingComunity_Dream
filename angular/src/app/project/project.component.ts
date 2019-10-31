import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { NzContextMenuService, NzDropdownMenuComponent } from 'ng-zorro-antd/dropdown';
import { Router } from '@angular/router';
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

  constructor(injector: Injector
    , private router: Router
    , private userDetailService: UserDetailService
    , private projectService: ProjectService) { 
      
    super(injector);
    }

  ngOnInit() {
    this.search = { userDetailId: null, maxResultCount: 10, skipCount: 0, isPublic:true };
    this.searchUserProject = {MaxResultCount: 5, SkipCount: 0}

    this.getCurrentUser();
    this.getCurrentUserProjects();
    this.getUserProjectsPaged();
  }

  search: any = { userDetailId: null, maxResultCount: 10, skipCount: 0, isPublic:true };
  currentUser: CurrentUserDetailDto = new CurrentUserDetailDto();
  isLogin: boolean = false;
  headUrl: string = "";
  searchUserProject:any = {MaxResultCount: 5, SkipCount: 0}
  userProjectTotalCount:number;
  pageIndex:number = 1;

  isReload:boolean=false;
  
  //currentProjectUserDetailId : any;

  currentUserProjects: UserProjectDto[];//当前用户计划集合
  recommendUserProjects:UserProjectDto[];//推荐学习计划集合

  getCurrentUserProjects(): void {
    this.isReload = true;
    this.projectService.getCurrentUserProjectDtos(this.searchUserProject).subscribe((result) => {
      this.currentUserProjects = result.items;
      this.userProjectTotalCount = result.totalCount;
      console.log(result);
      this.isReload = false;
    })
  }

  getCurrentUser(): void {
    this.userDetailService.getCurrentUserSimpleInfo().subscribe((result) => {
      if (result.userId != undefined) {
        this.isLogin = true;
        this.currentUser = result;
        this.headUrl = this.userDetailService.baseUrl + result.headPortraitUrl;
      }
    })
  }

  pageIndexChange():void{
    this.searchUserProject.SkipCount = (this.pageIndex-1)*this.searchUserProject.MaxResultCount;
    this.getCurrentUserProjects();
  }

  getUserProjectsPaged():void{
    this.projectService.getUserProjectsPaged(this.search).subscribe((result)=>{
      this.recommendUserProjects = UserProjectDto.fromJSArray(result.items);
    })
  }

  //加载更多文章
  onLoadMore(): void {
    //this.loadingMore = true;
    this.search.skipCount = this.search.maxResultCount * (this.search.skipCount + 1);
    this.projectService.getUserProjectsPaged(this.search).subscribe((result) => {
      this.recommendUserProjects = UserProjectDto.fromJSArray(result.items);
    })
  }

  refreshDetail(userProjectId: number,tagName:string,userDetailId:any): void {
    this.projectDetail.id = userProjectId;
    this.projectDetail.currentUserProjectTagName = tagName;
    this.projectDetail.currentprojectUserDetailId = userDetailId;
    this.projectDetail.getProjectsTreeById();
  }

  createProjectTag(): void {
    this.projectUserModal.show();
  }

  reload():void{
    this.getCurrentUserProjects();
  }

}
