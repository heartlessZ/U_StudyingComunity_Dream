import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
        path: 'app',
        //loadChildren: () => import('app/app.module').then(m => m.AppModule), // Lazy load account module
        loadChildren: 'app/app.module#AppModule',
        data: { preload: true }
    },
    {
        path: 'account',
        //loadChildren: () => import('account/account.module').then(m => m.AccountModule), // Lazy load account module
        loadChildren: 'account/account.module#AccountModule',
        data: { preload: false }
    },
    {
        path: 'admin',
        //loadChildren: () => import('admin/admin.module').then(m => m.AdminModule), // Lazy load account module
        loadChildren: 'admin/admin.module#AdminModule',
        data: { preload: false }
    },
    { path: '**', redirectTo:'app', pathMatch:'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: []
})
export class RootRoutingModule { }
