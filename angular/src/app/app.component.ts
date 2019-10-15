import { Component, ViewContainerRef, Injector, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { Router } from '@angular/router';
import { AppConsts } from '@shared/AppConsts'

import { SignalRAspNetCoreHelper } from '@shared/helpers/SignalRAspNetCoreHelper';
import { UserDetailService } from 'services';
import { UserDetailDto, CurrentUserDetailDto } from 'entities';
import { NzIconService } from 'ng-zorro-antd/icon';

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
    currentUser : CurrentUserDetailDto;
    isLogin : boolean = false;
    headurl : string = "";
    host = AppConsts.remoteServiceBaseUrl;

    constructor(
        injector: Injector,
        private router : Router,
        private userDetailService : UserDetailService,
        private _iconService: NzIconService
    ) {
        super(injector);
        // this._iconService.fetchFromIconfont({
        //     scriptUrl: 'https://at.alicdn.com/t/font_8d5l8fzk5b87iudi.js'
        //   });
    }

    ngOnInit(): void {

        this.getCurrentUser();

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

    }

    getCurrentUser():void {
        this.userDetailService.getCurrentUserSimpleInfo().subscribe((result)=>{
            console.log(result);
            if(result.userId != undefined)
            {
                this.isLogin=true;
                this.currentUser = result;
                if(result.headPortraitUrl != null)
                    this.headurl = this.host + result.headPortraitUrl;
            }
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

    goPersonalInfo(id:string):void{
        console.log(id);
        
        if (id == undefined)
            return;
        this.router.navigate(["/app/personal-center",{id:id}]);
    }
}
