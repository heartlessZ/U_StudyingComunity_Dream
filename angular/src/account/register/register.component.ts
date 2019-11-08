import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import {
  AccountServiceProxy,
  RegisterInput,
  RegisterOutput
} from '@shared/service-proxies/service-proxies';
import { LoginService } from '../login/login.service';

@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
  animations: [accountModuleAnimation()]
})
export class RegisterComponent extends AppComponentBase implements OnInit {
  model: RegisterInput = new RegisterInput();
  saving = false;
  validateForm: FormGroup;

  constructor(
    injector: Injector,
    private _accountService: AccountServiceProxy,
    private _router: Router,
    private _loginService: LoginService,
    private fb: FormBuilder
  ) {
    super(injector);
  }

  back(): void {
    this._router.navigate(['/account/login']);
  }

  save(): void {
    this.saving = true;
    this.model.name = this.model.userName;
    this.model.surname = this.model.userName;
    this.submitForm()
    if(!this.validateForm.valid)
      return;
    this._accountService
      .register(this.model)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result: RegisterOutput) => {
        if (result.code == 200) {
          //this.notify.success('注册成功');
          this.message.success('注册成功');
          this._router.navigate(['/account/login']);
          return;
        }
        else {
          //this.notify.error(result.msg);
          this.message.error(result.msg);
        }

        // Autheticate
        // this.saving = true;
        // this._loginService.authenticateModel.userNameOrEmailAddress = this.model.userName;
        // this._loginService.authenticateModel.password = this.model.password;
        // this._loginService.authenticate(() => {
        //   this.saving = false;
        // });

        this.saving = false;
      });
  }

  submitForm(): void {
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }
  }

  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.validateForm.controls.checkPassword.updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  };

  getCaptcha(e: MouseEvent): void {
    e.preventDefault();
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
      loginname: [null, [Validators.required]],
    });
  }

  returnHome(): void {
    this._router.navigate(["app/home"])
  }
}
