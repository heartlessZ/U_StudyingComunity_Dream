import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AbpModule } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

import {HomeComponent} from './home/home.component';

import { LibraryComponent } from './library/library.component';
import { ProjectComponent } from './project/project.component';
import { CommunityComponent } from './community/community.component';
import { PersonalCenterComponent } from './personal-center/personal-center.component';
import { UserDetailService, CommonHttpClient } from 'services';
import { ChangePasswordComponent } from './personal-center/change-password/change-password.component';
import { UserDetailEditComponent } from './personal-center/user-detail-edit/user-detail-edit.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      LibraryComponent,
      ProjectComponent,
      CommunityComponent,
      PersonalCenterComponent,
      ChangePasswordComponent,
      UserDetailEditComponent
   ],
   imports: [
      CommonModule,
      FormsModule,
      ReactiveFormsModule,
      HttpClientModule,
      HttpClientJsonpModule,
      ModalModule.forRoot(),
      AbpModule,
      AppRoutingModule,
      ServiceProxyModule,
      SharedModule,
      NgxPaginationModule,
      ReactiveFormsModule
   ],
   providers: [
      UserDetailService,
      CommonHttpClient
   ],
   entryComponents: []
})
export class AppModule {}
