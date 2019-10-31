import { Component, OnInit, ViewChild, Input, Injector } from '@angular/core';
import { NzContextMenuService, NzDropdownMenuComponent } from 'ng-zorro-antd/dropdown';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectDto, CurrentUserDetailDto } from 'entities';
import { ProjectModalComponent } from './project-modal/project-modal.component';
import { UserDetailService, ProjectService } from 'services';
import { ProjectUserModalComponent } from './project-user-modal/project-user-modal.component';
import { AppComponentBase } from '@shared/component-base';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent extends AppComponentBase implements OnInit {
  @ViewChild('projectModal', { static: true }) projectModal: ProjectModalComponent;
  @Input() currentUser: CurrentUserDetailDto;

  id: number;
  currentUserProjectTagName:string;
  currentProjectId: number;
  currentProject:ProjectDto = new ProjectDto();
  currentprojectUserDetailId: any;
  data: ProjectDto[];
  isFirst: boolean = false;
  title: any;

  isCurrentUser: boolean = false;
  isLogin: boolean = false;
  headUrl: string = "";

  constructor(injector: Injector
    , private nzContextMenuService: NzContextMenuService
    , private actRouter: ActivatedRoute
    , private router: Router
    , private userDetailService: UserDetailService
    , private projectService: ProjectService) {
      super(injector);
  }

  ngOnInit() {
    
  }

  getProjectsTreeById(): void {
    
    if(this.currentUser.userDetailId == this.currentprojectUserDetailId){
      this.isCurrentUser = true;
    }else{
      this.isCurrentUser = false;
    }

    this.data = [];
    this.projectService.getProjectTreeById(this.id).subscribe((result) => {
      console.log(result);
      
      if (result.id != undefined) {
        this.data.push(result);
        this.loadTree(result);
      }else{
        this.isFirst = true;
      }
    })
  }

  loadTree(project: ProjectDto): void {
    if (project.childProject != undefined) {
      this.data.push(project.childProject);
      this.loadTree(project.childProject)
    }
    if (this.data.length <= 0) {
      this.isFirst = true;
    } else {
      this.isFirst = false;
    }
  }

  contextMenu($event: MouseEvent, menu: NzDropdownMenuComponent, project: any): void {
    this.nzContextMenuService.create($event, menu);
    //console.log(menu);
    this.currentProject = project;
    this.currentProjectId = project.id;
  }

  closeMenu(): void {
    this.nzContextMenuService.close();
  }

  createNextProject(): void {
    this.projectModal.showByCreate()
  }

  editCurrentProject(): void {
    this.projectModal.showByEdit(this.currentProjectId)
  }

  saveProgress():void{
    this.projectService.createOrUpdateProject(this.currentProject).subscribe((result)=>{
      this.notify.success("保存成功")
    })
  }
}
