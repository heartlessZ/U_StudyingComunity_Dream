import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { PersonalCenterComponent } from './personal-center.component';
import { CreateArticleComponent } from '@app/community/create-article/create-article.component';

const routes: Routes = [
    {
        path: '',
        component: PersonalCenterComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'create-article',
        component: CreateArticleComponent,
        canActivate: [AppRouteGuard],
    },
    {
        path: 'create-article/:id',
        component: CreateArticleComponent,
        canActivate: [AppRouteGuard],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PersonalCenterRoutingModule { }
