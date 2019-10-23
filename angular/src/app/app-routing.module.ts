import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
 
import { LibraryComponent } from './library/library.component';
import { ProjectComponent } from './project/project.component';
import { CommunityComponent } from './community/community.component';
import { PersonalCenterComponent } from './personal-center/personal-center.component';
import { BookDetailComponent } from './library/book-detail/book-detail.component';
import { CreateArticleComponent } from './community/create-article/create-article.component';
import { ArticleDetailComponent } from './community/article-detail/article-detail.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: '', redirectTo:'home', pathMatch:'full' },
                    { path: 'home', component:HomeComponent},
                    { path: 'library', component:LibraryComponent},
                    { path: 'book-detail/:id', component:BookDetailComponent},
                    { path: 'project', component:ProjectComponent},
                    { path: 'community/article-detail/:id', component:ArticleDetailComponent},
                    { path: 'community', component:CommunityComponent},
                    { path: 'personal-center', component:PersonalCenterComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }},
                    { path: 'create-article', component:CreateArticleComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }}
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
