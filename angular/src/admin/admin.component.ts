import { Component, ViewContainerRef, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
    templateUrl: './account.component.html',
    styleUrls: [
        './account.component.less',
        '../../node_modules/ng-zorro-antd/ng-zorro-antd.less'
    ],
    encapsulation: ViewEncapsulation.None
})
export class AccountComponent extends AppComponentBase implements OnInit {

    versionText: string;
    currentYear: number;

    private viewContainerRef: ViewContainerRef;

    public constructor(
        injector: Injector,
    ) {
        super(injector);

        this.currentYear = new Date().getFullYear();
        this.versionText = this.appSession.application.version + ' [' + this.appSession.application.releaseDate.format('YYYYDDMM') + ']';
    }

    showTenantChange(): boolean {
        //return abp.multiTenancy.isEnabled;
        return false;
    }

    ngOnInit(): void {
        $('body').addClass('login-page');
    }
}
