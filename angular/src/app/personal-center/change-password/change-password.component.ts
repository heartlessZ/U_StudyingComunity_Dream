import { Component, OnInit, EventEmitter, Output, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Observable, Observer } from 'rxjs';
import { UserDetailService } from 'services';
import { AccountServiceProxy, ChangePasswordDto, UserServiceProxy } from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';

@Component({
  selector: 'app-change-password-model',
  templateUrl: './change-password.component.html',
  styleUrls: [
    './change-password.component.css'
  ],
})
export class ChangePasswordComponent extends AppComponentBase implements OnInit {
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  validateForm: FormGroup;
  emodalVisible = false; // 模态框是否显示
  isOkLoading = false;

  pwdDto: ChangePasswordDto = new ChangePasswordDto();

  constructor(injector: Injector,
    private fb: FormBuilder,
    private router: Router,
    private appRouteGuard: AppRouteGuard,
    // private userDetailService : UserDetailService,
    // private accountServiceProxy:AccountServiceProxy,
    private userServiceProxy: UserServiceProxy) {
    super(injector);
    this.validateForm = this.fb.group({
      oldPwd: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirm: ['', [this.confirmValidator]]
    });
  }

  /**
   * 显示模态框（进入新增页）
   */
  show() {
    this.emodalVisible = true;
    // console.log("修改密码");

  }

  handleOk(): void {
    this.isOkLoading = true;
    this.submitForm(this.pwdDto);

    if (!this.validateForm.valid) {
      return;
    }
    this.userServiceProxy.changePassword(this.pwdDto).subscribe((result) => {
      if (result) {
        this.message.success('修改成功');
        this.emodalVisible = false;
        this.isOkLoading = false;

        this.router.navigate(['account/login']);
      }
    });
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
      this.validateForm.controls[key].updateValueAndValidity();
    }
  }

  validateConfirmPassword(): void {
    setTimeout(() => this.validateForm.controls.confirm.updateValueAndValidity());
  }

  confirmValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { error: true, required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  }

}

