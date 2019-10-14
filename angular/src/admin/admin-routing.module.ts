import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { AdminComponent } from './admin.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AdminComponent,
                children: [
                    { path: '', redirectTo:'home',  pathMatch:'full' },
                    { path: 'home', component: HomeComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                    { path: 'user', component: UserComponent , canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Admin' } },
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }
