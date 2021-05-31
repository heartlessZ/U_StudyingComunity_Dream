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
import { UserDetailService, CommonHttpClient, BookService, ArticleService, ProjectService, AppAuthService } from 'services';
import { ChangePasswordComponent } from './personal-center/change-password/change-password.component';
import { UserDetailEditComponent } from './personal-center/user-detail-edit/user-detail-edit.component';

import { BookDetailComponent } from './library/book-detail/book-detail.component';
import { ArticleAndProjectComponent } from './personal-center/article-and-project/article-and-project.component';
import { CreateArticleComponent } from './community/create-article/create-article.component';
import { ArticleDetailComponent } from './community/article-detail/article-detail.component';
import { ReplyModalComponent } from './community/article-detail/reply-modal/reply-modal.component';
import { RouterModule } from '@angular/router';

import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { ProjectDetailComponent } from './project/project-detail/project-detail.component';
import { ProjectModalComponent } from './project/project-detail/project-modal/project-modal.component';
import { ProjectUserModalComponent } from './project/project-detail/project-user-modal/project-user-modal.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,

      ProjectComponent,
      ProjectDetailComponent,
      ProjectModalComponent,
      ProjectUserModalComponent,

      PersonalCenterComponent,
      ChangePasswordComponent,
      UserDetailEditComponent,
      ArticleAndProjectComponent,

      LibraryComponent,
      BookDetailComponent,

      CommunityComponent,
      CreateArticleComponent,
      ArticleDetailComponent,
      ReplyModalComponent,
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
   ],
   providers: [
      UserDetailService,
      CommonHttpClient,
      BookService,
      ArticleService,
      ProjectService,
      AppAuthService,
   ],
   entryComponents: [
      AppComponent,
      HomeComponent,
   ]
})
export class AppModule {}
