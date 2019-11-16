import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { CommunityRoutingModule } from './community-routing.module';
import { CommunityComponent } from './community.component';
import { CreateArticleComponent } from './create-article/create-article.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';
import { ReplyModalComponent } from './article-detail/reply-modal/reply-modal.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        SharedModule,
        CommunityRoutingModule
    ],
    declarations: [
        CommunityComponent,
        //CreateArticleComponent,
        ArticleDetailComponent,
        ReplyModalComponent,
    ],
    entryComponents: [
        CommunityComponent,
        //CreateArticleComponent,
        ArticleDetailComponent,
        ReplyModalComponent,
    ],
    // providers: [LocalizationService, MenuService],
})
export class CommunityModule { }
