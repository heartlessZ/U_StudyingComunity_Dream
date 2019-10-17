import { Component, ViewContainerRef, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/component-base/app-component-base';

@Component({
    templateUrl: 'admin.component.html',
    styleUrls: [
        'admin.component.less'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AdminComponent extends AppComponentBase implements OnInit {

    versionText: string;
    currentYear: number;
    isCollapsed = false;

    private viewContainerRef: ViewContainerRef;

    public constructor(
        injector: Injector,
    ) {
        super(injector);

        this.currentYear = new Date().getFullYear();
        this.versionText = this.appSession.application.version + ' [' + this.appSession.application.releaseDate.format('YYYYDDMM') + ']';
    }

    ngOnInit(): void {
        
    }
}
