import { Component, ViewContainerRef, Injector, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts'

import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
import { UserDetailService } from 'services';
import { UserDetailDto } from 'entities';

@Component({
    templateUrl: './app.component.html',
    styleUrls: [
        '../../node_modules/ng-zorro-antd/ng-zorro-antd.less',
        './app.component.less'
    ],
})
export class AppComponent extends AppComponentBase implements OnInit//, AfterViewInit
 {

    //private viewContainerRef: ViewContainerRef;
    currentUser : UserDetailDto;
    isLogin : boolean = false;
    headurl : string = "";
    host = AppConsts.remoteServiceBaseUrl;

    constructor(
        injector: Injector,
        private router : Router,
        private userDetailService : UserDetailService,
    ) {
        super(injector);
    }

    ngOnInit(): void {

        SignalRAspNetCoreHelper.initSignalR();

        abp.event.on('abp.notifications.received', userNotification => {
            abp.notifications.showUiNotifyForUserNotification(userNotification);

            // Desktop notification
            Push.create('AbpZeroTemplate', {
                body: userNotification.notification.data.message,
                icon: abp.appPath + 'assets/app-logo-small.png',
                timeout: 6000,
                onClick: function () {
                    window.focus();
                    this.close();
                }
            });
        });

        this.getCurrentUser();
    }

    getCurrentUser():void {
        this.userDetailService.getCurrentUserSimpleInfo().subscribe((result)=>{
            console.log(result);
            
        })
    }

    // ngAfterViewInit(): void {
    //     $.AdminBSB.activateAll();
    //     $.AdminBSB.activateDemo();
    // }

    // onResize(event) {
    //     // exported from $.AdminBSB.activateAll
    //     $.AdminBSB.leftSideBar.setMenuHeight();
    //     $.AdminBSB.leftSideBar.checkStatuForResize(false);

    //     // exported from $.AdminBSB.activateDemo
    //     $.AdminBSB.demo.setSkinListHeightAndScroll();
    //     $.AdminBSB.demo.setSettingListHeightAndScroll();
    // }

    login() : void{
        this.router.navigate(["account/login"])
    }

    register() : void{
        this.router.navigate(["account/register"])
    }

    exit() : void {
        
    }
}
