import { Component, OnInit, ViewChild, Input, Injector, Output, EventEmitter } from '@angular/core';
import { NzContextMenuService, NzDropdownMenuComponent } from 'ng-zorro-antd/dropdown';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectDto, CurrentUserDetailDto } from 'entities';
import { ProjectModalComponent } from './project-modal/project-modal.component';
import { UserDetailService, ProjectService } from 'services';
import { ProjectUserModalComponent } from './project-user-modal/project-user-modal.component';
import { AppComponentBase } from '@shared/component-base';
import { NzModalService } from 'ng-zorro-antd';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  @ViewChild('projectModal', { static: true }) projectModal: ProjectModalComponent;
  @Input() currentUser: CurrentUserDetailDto;

  id: number;
  currentUserProjectTagName:string;
  currentProjectId: number;
  currentProject:ProjectDto = new ProjectDto();
  currentprojectUserDetailId: any;
  data: ProjectDto[] = [];
  isFirst: boolean = false;
  title: any;

  isCurrentUser: boolean = false;
  isLogin: boolean = false;
  headUrl: string = "";
  isSuccsess:boolean = false;

  constructor(injector: Injector
    , private nzContextMenuService: NzContextMenuService
    , private actRouter: ActivatedRoute
    , private router: Router
    , private userDetailService: UserDetailService
    , private projectService: ProjectService
    , private modal: NzModalService) {
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
      this.isFirst =false;
      if (result.id != undefined) {
        this.data.push(result);
        this.loadTree(result);
      }
      this.afterChange();
    })
  }

  //当任务发生变化后
  afterChange(){
      //创建第一个任务按钮是否显示
      if (this.data.length <= 0) {
        this.isFirst = true;
      } else {
        this.isFirst = false;
      }

      //循环加载每个标签的进度
      let sum = 0;
      this.data.forEach(d=>{
        sum += d.progress;
      })
      console.log(sum);
      
      //判断任务是否完成
      if(sum == this.data.length*100){
        this.isSuccsess = true;
      }else{
        this.isSuccsess = false;
      }
      this.modalSave.emit(null);
  }

  loadTree(project: ProjectDto): void {
    if (project.childProject != undefined) {
      this.data.push(project.childProject);
      this.loadTree(project.childProject)
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
    this.afterChange();
  }

  editCurrentProject(): void {
    this.projectModal.showByEdit(this.currentProjectId)
  }

  saveProgress():void{
    this.projectService.createOrUpdateProject(this.currentProject).subscribe((result)=>{
      this.notify.success("保存成功")
      this.afterChange();
    })
  }

  deleteUserProject():void{
    this.modal.confirm({
      nzContent: '确定是否删除当前标签?该操作不可撤销',
      nzOnOk: () => {
        this.projectService.dropUserProjectById(this.id).subscribe((result)=>{
          this.notify.success("删除成功");
          this.afterChange();
          this.isCurrentUser = false;
          this.currentUserProjectTagName = "";
          this.currentProject = new ProjectDto();
        })
      }
    });
  }

  deleteCurrentProject():void{
    this.modal.confirm({
      nzContent: '确定是否删除当前标签?该操作不可撤销',
      nzOnOk: () => {
        this.projectService.deleteProjectById(this.currentProjectId).subscribe((result)=>{
          this.notify.success("删除成功")
          this.getProjectsTreeById()
        })
      }
    });
  }
}
