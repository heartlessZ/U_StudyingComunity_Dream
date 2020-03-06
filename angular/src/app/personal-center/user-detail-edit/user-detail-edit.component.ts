import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { UserDetailDto } from 'entities';
import { UserDetailService } from 'services';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UploadFile } from 'ng-zorro-antd/upload';
import { Observable, Observer } from 'rxjs';
import { AppConsts } from '@shared/AppConsts';
import { registerLocaleData } from '@angular/common';
import zh from '@angular/common/locales/zh';
registerLocaleData(zh);

@Component({
  selector: 'app-user-detail-edit-model',
  templateUrl: './user-detail-edit.component.html',
  styleUrls: [
    './user-detail-edit.component.css'
  ],
})
export class UserDetailEditComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();


  validateForm: FormGroup;
  userDetail: UserDetailDto;
  emodalUserVisible = false; // 模态框是否显示
  isOkLoading = false;
  loading = false;
  avatarUrl: string;
  fileUploadUrl: string;
  fileUrl: string; // 图片最终上传所得的地址
  birthday: Date;
  gender: any;

  constructor(injector: Injector,
    private fb: FormBuilder,
    private userDetailService: UserDetailService) {
    super(injector);

    this.validateForm = this.fb.group({
      surname: ['', [Validators.required]],
      gender: [null],
      description: [null],
      required: [false],
      birthday: [null],
    });
  }

  /**
   * 显示模态框（进入用户详情页）
   */
  showWin(id: string) {
    this.emodalUserVisible = true;
    this.userDetail = new UserDetailDto();
    this.getUserDetailById(id);
  }

  handleOk(): void {
    this.isOkLoading = true;
    if (this.fileUrl != undefined) {
      this.userDetail.headPortraitUrl = this.fileUrl;
    }
    this.userDetail.gender = this.gender;
    this.submitForm(this.userDetail);

    // console.log(this.userDetail);
    // return;
    if (!this.validateForm.valid) {
      return;
    }
    this.userDetailService.editUserDetail(this.userDetail).subscribe((result) => {
      if (result) {
        this.message.success('修改成功');
        this.emodalUserVisible = false;
        this.isOkLoading = false;

        this.modalSave.emit(null);
      }
    });
  }

  handleCancel(): void {
    this.emodalUserVisible = false;
    this.validateForm.reset();
  }

  ngOnInit() {
    this.fileUploadUrl = this.userDetailService.baseUrl + '/api/File/upload';
  }

  onChange(result: Date): void {
    this.userDetail.birthday = result;
  }

  submitForm(value: any): void {
    for (const key in this.validateForm.controls) {
      this.validateForm.controls[key].markAsDirty();
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }

  getUserDetailById(id: string): void {
    this.userDetailService.getUserDetailById(id).subscribe((result) => {
      this.userDetail = result;
      this.avatarUrl = this.userDetailService.baseUrl + result.headPortraitUrl;
      this.birthday = result.birthday;
      this.gender = result.gender.toString();
      // console.log(this.avatarUrl);
      // console.log(this.userDetail);

    });
  }

  beforeUpload = (file: File) => {
    return new Observable((observer: Observer<boolean>) => {
      const isJPG = file.type === 'image/jpeg';
      if (!isJPG) {
        this.message.error('只能上传jpg格式的图片！');
        observer.complete();
        return;
      }
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.message.error('头像大小必须小于2MB！');
        observer.complete();
        return;
      }
      // check height
      this.checkImageDimension(file).then(dimensionRes => {
        // if (!dimensionRes) {
        //   this.message.error('Image only 300x300 above');
        //   observer.complete();
        //   return;
        // }

        // observer.next(isJPG && isLt2M && dimensionRes);
        observer.next(isJPG && isLt2M);
        observer.complete();
      });
    });
  }

  handleChange(info: { file: UploadFile }): void {
    switch (info.file.status) {
      case 'uploading':
        this.loading = true;
        break;
      case 'done':
          this.fileUrl = info.file.response.result.data.url;
        // Get this url from response in real world.
        this.getBase64(info.file!.originFileObj!, (img: string) => {
          this.loading = false;
          this.avatarUrl = img;
        });
        break;
      case 'error':
        this.message.error('上传失败，请稍后重试');
        this.loading = false;
        break;
    }
  }

  private getBase64(img: File, callback: (img: string) => void): void {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result!.toString()));
    reader.readAsDataURL(img);
  }

  private checkImageDimension(file: File): Promise<boolean> {
    return new Promise(resolve => {
      const img = new Image(); // create image
      img.src = window.URL.createObjectURL(file);
      img.onload = () => {
        const width = img.naturalWidth;
        const height = img.naturalHeight;
        window.URL.revokeObjectURL(img.src!);
        resolve(width === height && width >= 300);
      };
    });
  }
}
