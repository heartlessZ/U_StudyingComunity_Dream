import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '@shared/shared.module';
import { HttpClientModule } from '@angular/common/http';
import { ProjectComponent } from './project.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { ProjectUserModalComponent } from './project-detail/project-user-modal/project-user-modal.component';
import { ProjectModalComponent } from './project-detail/project-modal/project-modal.component';
import { ProjectRoutingModule } from './project-routing.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        SharedModule,
        ProjectRoutingModule
    ],
    declarations: [
        ProjectComponent,
        ProjectDetailComponent,
        ProjectUserModalComponent,
        ProjectModalComponent,
    ],
    entryComponents: [
        ProjectComponent,
        ProjectDetailComponent,
        ProjectUserModalComponent,
        ProjectModalComponent,
    ],
    // providers: [LocalizationService, MenuService],
})
export class ProjectModule { }
