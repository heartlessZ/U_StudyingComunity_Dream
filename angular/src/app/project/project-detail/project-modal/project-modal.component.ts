import { Component, OnInit, EventEmitter, Output, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Observable, Observer } from 'rxjs';
import { UserDetailService, ProjectService } from 'services';
import { AccountServiceProxy, ChangePasswordDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { ProjectDto } from 'entities';

@Component({
  selector: 'app-project-modal',
  templateUrl: './project-modal.component.html',
  styleUrls: ['./project-modal.component.css']
})
export class ProjectModalComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  @Input() isFirst:boolean;
  @Input() userProjectId:number;
  @Input() currentProjectId:number;

  validateForm: FormGroup;
  title:string;
  emodalVisible = false;//模态框是否显示
  isOkLoading = false;

  project:ProjectDto = new ProjectDto();
  

  constructor(injector: Injector,
    private fb: FormBuilder,
    private router : Router,
    private appRouteGuard : AppRouteGuard,
    private projectService:ProjectService) {
      super(injector);
    this.validateForm = this.fb.group({
      name: ['', [Validators.required]],
      expirationTime: [null, [Validators.required]],
      remark: ['']
    });
  }

  /**
   * 显示模态框（进入新增页）
   */
  showByCreate(id?:number) {
    this.emodalVisible = true;
    this.title = "新增计划"
    this.project = new ProjectDto();
    this.project.expirationTime=new Date();
    this.project.parent = this.currentProjectId;
    //console.log(this.project);
    
    if (id!=undefined){
      this.project.parent = id;
    }
    if(this.isFirst){
      this.project.parent = 0;
    }
  }

  /**
   * 显示模态框（进入编辑页）
   */
  showByEdit(id:number) {
    this.emodalVisible = true;
    this.title = "编辑计划";
    this.getProjectById(id);
  }

  getProjectById(id:number):void{
    this.projectService.getProjectById(id).subscribe((result)=>{
      this.project = result;
    })
  }

  onChange(result: Date): void {
    //this.project.expirationTime = result;
  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(this.project);
    
    //console.log(this.project);
    //return;
      if(!this.validateForm.valid)
      return;
    
    this.projectService.createOrUpdateProject(this.project).subscribe((result)=>{
      this.notify.success("成功");
        this.emodalVisible = false;
        this.isOkLoading = false;
        this.validateForm.reset();
        if(this.isFirst && result != 0){
          this.projectService.editUserProjectProId(this.userProjectId,result).subscribe((res)=>{
            this.modalSave.emit(null);
          })
        }
        this.modalSave.emit(null);
    })
  }

  handleCancel(): void {
    this.emodalVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {
    
  }

  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }

  resetForm(e: MouseEvent): void {
    e.preventDefault();
    this.validateForm.reset();
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsPristine();
      //this.validateForm.controls[key].updateValueAndValidity();
    }
  }

}
