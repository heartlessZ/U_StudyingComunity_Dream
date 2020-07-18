import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { CommunityComponent } from './community.component';
import { ArticleDetailComponent } from './article-detail/article-detail.component';

const routes: Routes = [
    {
        path: '',
        component: CommunityComponent,
    },
    {
        path: 'article-detail/:id',
        component: ArticleDetailComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CommunityRoutingModule { }
