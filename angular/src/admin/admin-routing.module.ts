import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AdminComponent } from './admin.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';
import { LibraryComponent } from './library/library.component';
import { BookCategoryComponent } from './book-category/book-category.component';
import { CommunityComponent } from './community/community.component';
import { BookResourceComponent } from './library/book-resource/book-resource.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AdminComponent,
                children: [
                    { path: '', redirectTo:'user',  pathMatch:'full' },
                    { path: 'user', component: UserComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                    { path: 'library', component: LibraryComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                    { path: 'library/book-resource', component: BookResourceComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                    { path: 'book-category', component: BookCategoryComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                    { path: 'community', component: CommunityComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }
