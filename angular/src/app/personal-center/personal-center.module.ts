import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { PersonalCenterComponent } from './personal-center.component';
import { UserDetailEditComponent } from './user-detail-edit/user-detail-edit.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ArticleAndProjectComponent } from './article-and-project/article-and-project.component';
import { PersonalCenterRoutingModule } from './personal-center-routing.module';
import { CreateArticleComponent } from '@app/community/create-article/create-article.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        SharedModule,
        PersonalCenterRoutingModule
    ],
    declarations: [
        ArticleAndProjectComponent,
        ChangePasswordComponent,
        UserDetailEditComponent,
        PersonalCenterComponent,
        CreateArticleComponent,
    ],
    entryComponents: [
        ArticleAndProjectComponent,
        ChangePasswordComponent,
        UserDetailEditComponent,
        PersonalCenterComponent,
        CreateArticleComponent,
    ],
    // providers: [LocalizationService, MenuService],
})
export class PersonalCenterModule { }
