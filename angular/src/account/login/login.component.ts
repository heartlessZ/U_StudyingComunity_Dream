import { Component, Injector, OnInit } from '@angular/core';
import { AbpSessionService } from '@abp/session/abp-session.service';
import { AppComponentBase } from '@shared/component-base/app-component-base';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { Router } from '@angular/router';
import { LoginService } from './login.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  animations: [accountModuleAnimation()]
})
export class LoginComponent extends AppComponentBase implements OnInit {
  submitting = false;
  validateForm: FormGroup;


  constructor(
    injector: Injector,
    public loginService: LoginService,
    private _sessionService: AbpSessionService,
    private _router: Router,
    private fb: FormBuilder
  ) {
    super(injector);
  }

  get multiTenancySideIsTeanant(): boolean {
    return this._sessionService.tenantId > 0;
  }

  get isSelfRegistrationAllowed(): boolean {
    if (!this._sessionService.tenantId) {
      return false;
    }

    return true;
  }

  login(): void {
    this.submitting = true;
    this.submitForm();
    if(!this.validateForm.valid)
      return;
    this.loginService.authenticate(() => (this.submitting = false));
  }

  submitForm(): void {
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[i].markAsDirty();
      this.validateForm.controls[i].updateValueAndValidity();
    }
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }

  register(): void {
    this._router.navigate(['/account/register']);
  }


  returnHome(): void {
    this._router.navigate(["/app/home"])
  }
}
