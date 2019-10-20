import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap';

import { AbpModule } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';

import { SharedModule } from '@shared/shared.module';

import { AdminComponent } from './admin.component';

import { AdminRoutingModule } from './admin-routing.module';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';
import { UserDetailService, CommonHttpClient, BookService } from 'services';
import { LibraryComponent } from './library/library.component';
import { BookCategoryComponent } from './book-category/book-category.component';
import { CreateCategoryComponent } from './book-category/create-category/create-category.component';
import { BookCategoryDetailComponent } from './book-category/book-category-detail/book-category-detail.component';
import { BookDetailComponent } from './library/book-detail/book-detail.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        HttpClientJsonpModule,
        AbpModule,
        SharedModule,
        ServiceProxyModule,
        ModalModule.forRoot(),
        ReactiveFormsModule,
        AdminRoutingModule
    ],
    declarations: [
        AdminComponent,
        HomeComponent,
        UserComponent,

        LibraryComponent,
        BookDetailComponent,

        BookCategoryComponent,
        CreateCategoryComponent,
        BookCategoryDetailComponent,

    ],
    providers: [
        UserDetailService,
        CommonHttpClient,
        BookService,
    ],
    entryComponents: [
    ]
})
export class AdminModule {

}
