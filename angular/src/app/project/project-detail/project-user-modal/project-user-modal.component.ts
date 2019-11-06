import { Component, OnInit, EventEmitter, Output, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { BookService, ProjectService } from 'services';
import { BookCategoryDto, ProjectDto, UserProjectDto, CurrentUserDetailDto } from 'entities';

@Component({
  selector: 'app-project-user-modal',
  templateUrl: './project-user-modal.component.html',
  styleUrls: ['./project-user-modal.component.css']
})
export class ProjectUserModalComponent extends AppComponentBase implements OnInit  {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  @Input() currentUser:CurrentUserDetailDto;

  validateForm: FormGroup;
  emodalVisible = false;//模态框是否显示
  isOkLoading = false;
  loading = false;
  name: string;
  title:string;
  userProject: UserProjectDto = new UserProjectDto();

  constructor(injector: Injector,
    private fb: FormBuilder,
    private projectService: ProjectService) {
    super(injector);

    this.validateForm = this.fb.group({
      tagName: ['', [Validators.required]],
      isPublic:['']
    });
  }

  show(id? : number) {
    this.emodalVisible = true;
    this.userProject  = new UserProjectDto();
    this.userProject.userId = this.currentUser.userDetailId;
    if(id != undefined && id != 0){
      this.getUserProjectById(id);
      this.title="编辑标签";
    }else{
      this.title = "新建标签"
    }
  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(name);
    if (!this.validateForm.valid)
      return;
    let parent = 0;
    this.projectService.createOrUpdateUserProject(this.userProject).subscribe((result) => {
      if (result) {
        this.message.success("成功");
        this.emodalVisible = false;
        this.isOkLoading = false;
        this.modalSave.emit(null);
      }
    })
  }

  getUserProjectById(id:number):void{
    this.projectService.getUserProjectById(id).subscribe((result)=>{
      this.userProject = result;
    })
  }

  handleCancel(): void {
    this.emodalVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {
    this.userProject.isPublic = false;
  }

  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }
}
