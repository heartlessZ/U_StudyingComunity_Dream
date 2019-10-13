import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
 
import { LibraryComponent } from './library/library.component';
import { ProjectComponent } from './project/project.component';
import { CommunityComponent } from './community/community.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: '', redirectTo:'home',  pathMatch:'full' },
                    { path: 'home', component:HomeComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }},
                    { path: 'library', component:LibraryComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }},
                    { path: 'project', component:ProjectComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }},
                    { path: 'community', component:CommunityComponent,  canActivate: [AppRouteGuard] ,  data : { guard: 'Pages.Users' }}
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
