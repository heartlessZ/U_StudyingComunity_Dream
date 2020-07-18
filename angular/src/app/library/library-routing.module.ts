import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { LibraryComponent } from './library.component';
import { BookDetailComponent } from './book-detail/book-detail.component';

const routes: Routes = [
    {
        path: '',
        component: LibraryComponent,
    },
    {
        path: 'book-detail/:id',
        component: BookDetailComponent,
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LibraryRoutingModule { }
