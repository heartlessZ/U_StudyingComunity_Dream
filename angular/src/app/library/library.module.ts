import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { LibraryComponent } from './library.component';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { LibraryRoutingModule } from './library-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        SharedModule,
        LibraryRoutingModule
    ],
    declarations: [
        LibraryComponent,
        BookDetailComponent,
    ],
    entryComponents: [
        LibraryComponent,
        BookDetailComponent,
    ],
    // providers: [LocalizationService, MenuService],
})
export class LibraryModule { }
